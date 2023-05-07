using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentAggregator.Migrations
{
    /// <inheritdoc />
    public partial class AddOpenAndAdjustTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ushort>(
                name: "WaitTotal",
                table: "SnapshotsCompressed",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<ushort>(
                name: "WaitCapacity",
                table: "SnapshotsCompressed",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<ushort>(
                name: "SectionNumber",
                table: "SnapshotsCompressed",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<ushort>(
                name: "NumEnrolled",
                table: "SnapshotsCompressed",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<ushort>(
                name: "NumCapacity",
                table: "SnapshotsCompressed",
                type: "smallint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<uint>(
                name: "ClassNumber",
                table: "SnapshotsCompressed",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<ushort>(
                name: "NumOpen",
                table: "SnapshotsCompressed",
                type: "smallint unsigned",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOpen",
                table: "SnapshotsCompressed");

            migrationBuilder.AlterColumn<int>(
                name: "WaitTotal",
                table: "SnapshotsCompressed",
                type: "int",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "WaitCapacity",
                table: "SnapshotsCompressed",
                type: "int",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AlterColumn<short>(
                name: "SectionNumber",
                table: "SnapshotsCompressed",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "NumEnrolled",
                table: "SnapshotsCompressed",
                type: "int",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "NumCapacity",
                table: "SnapshotsCompressed",
                type: "int",
                nullable: false,
                oldClrType: typeof(ushort),
                oldType: "smallint unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "ClassNumber",
                table: "SnapshotsCompressed",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");
        }
    }
}
