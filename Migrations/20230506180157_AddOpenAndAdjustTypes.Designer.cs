﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EnrollmentAggregator.Migrations
{
    [DbContext(typeof(EnrollmentHistoryContext))]
    [Migration("20230506180157_AddOpenAndAdjustTypes")]
    partial class AddOpenAndAdjustTypes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CourseSection", b =>
                {
                    b.Property<uint>("ClassNumber")
                        .HasColumnType("int unsigned");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CourseCode")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<ushort>("NumCapacity")
                        .HasColumnType("smallint unsigned");

                    b.Property<ushort>("NumEnrolled")
                        .HasColumnType("smallint unsigned");

                    b.Property<ushort?>("NumOpen")
                        .HasColumnType("smallint unsigned");

                    b.Property<ushort>("SectionNumber")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("SectionType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SnapshotId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TermCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ushort>("WaitCapacity")
                        .HasColumnType("smallint unsigned");

                    b.Property<ushort>("WaitTotal")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("ClassNumber", "Time");

                    b.HasIndex("CourseCode");

                    b.ToTable("SnapshotsCompressed", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
