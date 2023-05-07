public class CourseSection
{
    public DateTime Time { get; init; }
    /// Example: "2410"
    public string TermCode { get; set; } = "";
    /// Example: "EECS 281"
    public string CourseCode { get; set; } = "";
    public ushort SectionNumber { get; init; }
    public string SectionType { get; set; } = "";

    public uint ClassNumber { get; init; }

    public enum EnrollmentStatus
    {
        Open, Waitlist, Closed
    }
    public EnrollmentStatus Status { get; init; }
    public ushort NumEnrolled { get; init; }
    public ushort? NumOpen { get; init; }
    public ushort NumCapacity { get; init; }
    public ushort WaitTotal { get; init; }
    public ushort WaitCapacity { get; init; }
    public int SnapshotId { get; init; }
}