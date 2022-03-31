import { cookie } from './secrets.js';

async function fetchClassStandings(courseCode) {
  const res = await fetch(`https://atlas.ai.umich.edu/course/${encodeURI(courseCode)}/student-level.json`, {
    headers: {
      Cookie: cookie
    }
  })
  try {
    const { total_enrollment, enrollment_by_acad_standing: enroll } = await res.json()
    const enrollByClass = Object.fromEntries(enroll.map(it => [it.key, it.y]))
    return [enrollByClass.Freshman / total_enrollment, enrollByClass.Sophomore / total_enrollment,
    enrollByClass.Junior / total_enrollment, enrollByClass.Senior / total_enrollment]
  } catch (e) {
    console.log(courseCode)
    return []
  }
}

// header order: available_seats, class_num, course_code, course_id, enrollment_capacity, enrollment_status, section_number, wait_total
async function fetchCourseState(courseCode) {
  const res = await fetch(`https://atlas.ai.umich.edu/api/course-sections/${courseCode.replace(' ', '')}/2410/`, {
    headers: {
      Cookie: cookie
    }
  })
  const { combined } = await res.json()
  return combined.map(sect => [sect.available_seats, sect.class_num, sect.course_code,
  sect.course_id, sect.enrollment_capacity, sect.enrollment_status,
  sect.section_number, sect.wait_total]);
}

const requests = {
  zcbauer: ['EECS 373'],
  mipeng: ['EECS 370', 'ROB 101', 'STATS 412', 'MATH 214', 'ALA 223', 'UC 214', 'ENTR 390'/* 012 */],
  eecs: [388, 427, 441, 442, 445, 449, 467, 470, 471, 473, 477, 478, 481, 482, 483, 484, 485, 490, 491, 492, 493, 494, 495, 497].map(num => `EECS ${num}`),
}

function wait(ms) {
  return new Promise(res => setTimeout(res, ms));
}

async function fetchAllRequestedCourses() {
  // rate limited to respect Atlas
  let rows = [];
  for (const [req, courses] of Object.entries(requests)) {
    for (const course of courses) {
      rows = rows.concat((await fetchCourseState(course)).map(row => [new Date().toISOString(), req, ...row]));
      console.info('fetched state for ' + course + ' for ' + req);
      await wait(3000);
    }
  }
  return rows;
}

async function periodicUpdate() {
  try {
    const rows = await fetchAllRequestedCourses();
    const text = rows.map(row => row.join(',')).join('\n') + '\n';
    await Deno.writeTextFile('history.csv', text, { append: true });
  } catch (e) {
    console.error(`periodicUpdate failed`);
    console.error(e);
  }
}

setInterval(periodicUpdate, 1000 * 60 * 30);