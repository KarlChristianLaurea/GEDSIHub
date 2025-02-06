﻿// <auto-generated />
using System;
using GADApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GADApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241025142559_storeUserGPB")]
    partial class storeUserGPB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GADApplication.Models.AccomplishmentReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GPBId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GPBId");

                    b.ToTable("AccomplishmentReports");
                });

            modelBuilder.Entity("GADApplication.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivityType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Budget")
                        .HasColumnType("real");

                    b.Property<string>("Cause")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("GPBId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Objective")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PerformanceIndicators")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceBudget")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GPBId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("GADApplication.Models.ActivityAccomplishment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("AccomplishmentFileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("AccomplishmentFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AccomplishmentReportId")
                        .HasColumnType("int");

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<float>("ActualCost")
                        .HasColumnType("real");

                    b.Property<string>("ActualResult")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPdf")
                        .HasColumnType("bit");

                    b.Property<string>("NatureOfEvent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("OrganizingTeamMembers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccomplishmentReportId");

                    b.HasIndex("ActivityId");

                    b.ToTable("ActivityAccomplishments");
                });

            modelBuilder.Entity("GADApplication.Models.AvailableMandates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescriptionAvailable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepublicActAvailable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AvailableMandates");
                });

            modelBuilder.Entity("GADApplication.Models.FileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<double?>("ActualCost")
                        .HasColumnType("float");

                    b.Property<string>("ActualResult")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<bool?>("IsPdf")
                        .HasColumnType("bit");

                    b.Property<string>("NatureOfEvent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("OrganizingTeamMembers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("FileEntities");
                });

            modelBuilder.Entity("GADApplication.Models.GAR", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GPBId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GPBId");

                    b.ToTable("GARs");
                });

            modelBuilder.Entity("GADApplication.Models.GARActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<double>("ActualCost")
                        .HasColumnType("float");

                    b.Property<string>("ActualResult")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentTypeBudgetReport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentTypeEvaluationAttendance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentTypePictures")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContentTypeSpecialOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FileDataBudgetReport")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("FileDataEvaluationAttendance")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("FileDataPictures")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("FileDataSpecialOrder")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileNameBudgetReport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileNameEvaluationAttendance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileNamePictures")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileNameSpecialOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeBudgetReport")
                        .HasColumnType("bigint");

                    b.Property<long>("FileSizeEvaluationAttendance")
                        .HasColumnType("bigint");

                    b.Property<long>("FileSizePictures")
                        .HasColumnType("bigint");

                    b.Property<long>("FileSizeSpecialOrder")
                        .HasColumnType("bigint");

                    b.Property<int>("GARId")
                        .HasColumnType("int");

                    b.Property<string>("NatureOfEvent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfParticipants")
                        .HasColumnType("int");

                    b.Property<string>("OrganizingTeamMembers")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDateBudgetReport")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UploadDateEvaluationAttendance")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UploadDatePictures")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UploadDateSpecialOrder")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("GARId");

                    b.ToTable("GARActivities");
                });

            modelBuilder.Entity("GADApplication.Models.GARSubmissionSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("GARSubmissionSettings");
                });

            modelBuilder.Entity("GADApplication.Models.GPB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminComments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApprovalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MFOPAPOrPPA")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResponsibleUnit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalGAAOrBudget")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GPBs");
                });

            modelBuilder.Entity("GADApplication.Models.Mandate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ActivityId")
                        .HasColumnType("int");

                    b.Property<string>("ChedMemo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepublicAct")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("Mandates");
                });

            modelBuilder.Entity("GADApplication.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Receiver")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("GADApplication.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("GADApplication.Models.SubmissionSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SubmissionSettings");
                });

            modelBuilder.Entity("GADApplication.Models.AccomplishmentReport", b =>
                {
                    b.HasOne("GADApplication.Models.GPB", "GPB")
                        .WithMany("AccomplishmentReports")
                        .HasForeignKey("GPBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GPB");
                });

            modelBuilder.Entity("GADApplication.Models.Activity", b =>
                {
                    b.HasOne("GADApplication.Models.GPB", "GPB")
                        .WithMany("Activities")
                        .HasForeignKey("GPBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GPB");
                });

            modelBuilder.Entity("GADApplication.Models.ActivityAccomplishment", b =>
                {
                    b.HasOne("GADApplication.Models.AccomplishmentReport", null)
                        .WithMany("ActivityAccomplishments")
                        .HasForeignKey("AccomplishmentReportId");

                    b.HasOne("GADApplication.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("GADApplication.Models.FileEntity", b =>
                {
                    b.HasOne("GADApplication.Models.Activity", "Activity")
                        .WithMany("Files")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("GADApplication.Models.GAR", b =>
                {
                    b.HasOne("GADApplication.Models.GPB", "GPB")
                        .WithMany("GARs")
                        .HasForeignKey("GPBId");

                    b.Navigation("GPB");
                });

            modelBuilder.Entity("GADApplication.Models.GARActivity", b =>
                {
                    b.HasOne("GADApplication.Models.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GADApplication.Models.GAR", "GAR")
                        .WithMany("GARActivities")
                        .HasForeignKey("GARId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("GAR");
                });

            modelBuilder.Entity("GADApplication.Models.Mandate", b =>
                {
                    b.HasOne("GADApplication.Models.Activity", "Activity")
                        .WithMany("Mandates")
                        .HasForeignKey("ActivityId");

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("GADApplication.Models.AccomplishmentReport", b =>
                {
                    b.Navigation("ActivityAccomplishments");
                });

            modelBuilder.Entity("GADApplication.Models.Activity", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Mandates");
                });

            modelBuilder.Entity("GADApplication.Models.GAR", b =>
                {
                    b.Navigation("GARActivities");
                });

            modelBuilder.Entity("GADApplication.Models.GPB", b =>
                {
                    b.Navigation("AccomplishmentReports");

                    b.Navigation("Activities");

                    b.Navigation("GARs");
                });
#pragma warning restore 612, 618
        }
    }
}
