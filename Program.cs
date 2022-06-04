var client = new APIClient();
var sections = await client.ReadSections("2410", "EECS", 281);
foreach (var section in sections)
{
    Console.WriteLine($"{section.SectionNumber} -> {section.NumEnrolled}/{section.NumCapacity} {section.WaitTotal}/{section.WaitCapacity}");
}