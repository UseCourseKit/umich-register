using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentAggregator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SnapshotsCompressed",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ClassNumber = table.Column<int>(type: "int", nullable: false),
                    TermCode = table.Column<string>(type: "longtext", nullable: false),
                    CourseCode = table.Column<string>(type: "varchar(20)", nullable: false),
                    SectionNumber = table.Column<short>(type: "smallint", nullable: false),
                    SectionType = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NumEnrolled = table.Column<int>(type: "int", nullable: false),
                    NumCapacity = table.Column<int>(type: "int", nullable: false),
                    WaitTotal = table.Column<int>(type: "int", nullable: false),
                    WaitCapacity = table.Column<int>(type: "int", nullable: false),
                    SnapshotId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnapshotsCompressed", x => new { x.ClassNumber, x.Time });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SnapshotsCompressed_CourseCode",
                table: "SnapshotsCompressed",
                column: "CourseCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SnapshotsCompressed");
        }
    }
}
