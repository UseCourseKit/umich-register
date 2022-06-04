using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("EnrollmentRecords")]
readonly struct CourseSection
{

    public DateTime Time { get; init; }

    /// Example: "EECS 281"
    [DynamoDBRangeKey]
    public string CourseCode { get; init; }
    [DynamoDBHashKey]
    public short SectionNumber { get; init; }
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