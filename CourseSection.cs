using System.ComponentModel.DataAnnotations;

public class CourseSection
{

    public DateTime Time { get; init; }
    /// Example: "2410"
    public string TermCode { get; set; } = "";
    /// Example: "EECS 281"
    public string CourseCode { get; set; } = "";
    public short SectionNumber { get; init; }
    public string SectionType { get; set; } = "";

    public int ClassNumber { get; init; }

    public enum EnrollmentStatus
    {
        Open, Waitlist, Closed
    }
    public EnrollmentStatus Status { get; init; }
    public int NumEnrolled { get; init; }
    public int NumCapacity { get; init; }
    public int WaitTotal { get; init; }
    public int WaitCapacity { get; init; }
}