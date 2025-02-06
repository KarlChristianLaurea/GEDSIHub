﻿// <auto-generated />
using System;
using GADApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GADApplication.Migrations.ApplicationIdentityDb
{
    [DbContext(typeof(ApplicationIdentityDbContext))]
    [Migration("20241018125816_addedIdentityEntity")]
    partial class addedIdentityEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GADApplication.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DesignationRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastMessagePreview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ResponsibleUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UnitOfficeCampus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GADApplication.Models.ResponsibleUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ResponsibleUnits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Offices",
                            Name = "OP"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Offices",
                            Name = "SPPO"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Offices",
                            Name = "OIA"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Offices",
                            Name = "IAO"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Offices",
                            Name = "CMO"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Offices",
                            Name = "ULCO"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Offices",
                            Name = "OUBS"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Departments",
                            Name = "OVPRED"
                        },
                        new
                        {
                            Id = 9,
                            Category = "Departments",
                            Name = "RIHSD"
                        },
                        new
                        {
                            Id = 10,
                            Category = "Departments",
                            Name = "RISFI"
                        },
                        new
                        {
                            Id = 11,
                            Category = "Departments",
                            Name = "GADO"
                        },
                        new
                        {
                            Id = 12,
                            Category = "Departments",
                            Name = "RIST"
                        },
                        new
                        {
                            Id = 13,
                            Category = "Departments",
                            Name = "RICL"
                        },
                        new
                        {
                            Id = 14,
                            Category = "Departments",
                            Name = "RPO"
                        },
                        new
                        {
                            Id = 15,
                            Category = "Departments",
                            Name = "IQMSO"
                        },
                        new
                        {
                            Id = 16,
                            Category = "Departments",
                            Name = "IPMO"
                        },
                        new
                        {
                            Id = 17,
                            Category = "Departments",
                            Name = "EMO"
                        },
                        new
                        {
                            Id = 18,
                            Category = "Departments",
                            Name = "RMO"
                        },
                        new
                        {
                            Id = 19,
                            Category = "Campuses",
                            Name = "Unisan, Quezon Campus"
                        },
                        new
                        {
                            Id = 20,
                            Category = "Campuses",
                            Name = "Sto. Tomas, Batangas Campus"
                        },
                        new
                        {
                            Id = 21,
                            Category = "Campuses",
                            Name = "Sta. Rosa, Laguna Campus"
                        },
                        new
                        {
                            Id = 22,
                            Category = "Campuses",
                            Name = "San Pedro, Laguna Campus"
                        },
                        new
                        {
                            Id = 23,
                            Category = "Campuses",
                            Name = "Sablayan, Occidental Mindoro Campus"
                        },
                        new
                        {
                            Id = 24,
                            Category = "Campuses",
                            Name = "Ragay, Camarines Sur Campus"
                        },
                        new
                        {
                            Id = 25,
                            Category = "Campuses",
                            Name = "Gen. Luna, Quezon Campus"
                        },
                        new
                        {
                            Id = 26,
                            Category = "Campuses",
                            Name = "Mulanay, Quezon Campus"
                        },
                        new
                        {
                            Id = 27,
                            Category = "Campuses",
                            Name = "Alfonso, Cavite Campus"
                        },
                        new
                        {
                            Id = 28,
                            Category = "Campuses",
                            Name = "Maragondon, Cavite Campus"
                        },
                        new
                        {
                            Id = 29,
                            Category = "Campuses",
                            Name = "Lopez, Quezon Campus"
                        },
                        new
                        {
                            Id = 30,
                            Category = "Campuses",
                            Name = "Calauan, Laguna Campus"
                        },
                        new
                        {
                            Id = 31,
                            Category = "Campuses",
                            Name = "Biñan, Laguna Campus"
                        },
                        new
                        {
                            Id = 32,
                            Category = "Campuses",
                            Name = "Bansud, Oriental Mindoro Campus"
                        },
                        new
                        {
                            Id = 33,
                            Category = "Campuses",
                            Name = "Sta. Maria, Bulacan Campus"
                        },
                        new
                        {
                            Id = 34,
                            Category = "Campuses",
                            Name = "Pulilan, Bulacan Campus"
                        },
                        new
                        {
                            Id = 35,
                            Category = "Campuses",
                            Name = "Mariveles, Bataan Campus"
                        },
                        new
                        {
                            Id = 36,
                            Category = "Campuses",
                            Name = "Cabiao, Nueva Ecija Campus"
                        },
                        new
                        {
                            Id = 37,
                            Category = "Campuses",
                            Name = "Taguig City Campus"
                        },
                        new
                        {
                            Id = 38,
                            Category = "Campuses",
                            Name = "San Juan City Campus"
                        },
                        new
                        {
                            Id = 39,
                            Category = "Campuses",
                            Name = "Quezon City Campus"
                        },
                        new
                        {
                            Id = 40,
                            Category = "Campuses",
                            Name = "Parañaque City Campus"
                        },
                        new
                        {
                            Id = 41,
                            Category = "Departments",
                            Name = "OVPC"
                        },
                        new
                        {
                            Id = 42,
                            Category = "Departments",
                            Name = "OVPA"
                        },
                        new
                        {
                            Id = 43,
                            Category = "Departments",
                            Name = "CRO"
                        },
                        new
                        {
                            Id = 44,
                            Category = "Departments",
                            Name = "PPDO"
                        },
                        new
                        {
                            Id = 45,
                            Category = "Departments",
                            Name = "MHDPC"
                        },
                        new
                        {
                            Id = 46,
                            Category = "Departments",
                            Name = "GSO"
                        },
                        new
                        {
                            Id = 47,
                            Category = "Departments",
                            Name = "DRI"
                        },
                        new
                        {
                            Id = 48,
                            Category = "Departments",
                            Name = "PSMO"
                        },
                        new
                        {
                            Id = 49,
                            Category = "Departments",
                            Name = "PMO"
                        },
                        new
                        {
                            Id = 50,
                            Category = "Departments",
                            Name = "USSO"
                        },
                        new
                        {
                            Id = 51,
                            Category = "Departments",
                            Name = "MSD"
                        },
                        new
                        {
                            Id = 52,
                            Category = "Departments",
                            Name = "HRM"
                        },
                        new
                        {
                            Id = 53,
                            Category = "Departments",
                            Name = "OVPSAS"
                        },
                        new
                        {
                            Id = 54,
                            Category = "Departments",
                            Name = "UCCA"
                        },
                        new
                        {
                            Id = 55,
                            Category = "Departments",
                            Name = "SDPO"
                        },
                        new
                        {
                            Id = 56,
                            Category = "Departments",
                            Name = "ARCDO"
                        },
                        new
                        {
                            Id = 57,
                            Category = "Departments",
                            Name = "OCPS"
                        },
                        new
                        {
                            Id = 58,
                            Category = "Departments",
                            Name = "OSFA"
                        },
                        new
                        {
                            Id = 59,
                            Category = "Departments",
                            Name = "OSS"
                        },
                        new
                        {
                            Id = 60,
                            Category = "Departments",
                            Name = "OUR"
                        },
                        new
                        {
                            Id = 61,
                            Category = "Departments",
                            Name = "PFQ"
                        },
                        new
                        {
                            Id = 62,
                            Category = "Departments",
                            Name = "RGO"
                        },
                        new
                        {
                            Id = 63,
                            Category = "Departments",
                            Name = "IPO"
                        },
                        new
                        {
                            Id = 64,
                            Category = "Departments",
                            Name = "FMO"
                        },
                        new
                        {
                            Id = 65,
                            Category = "Departments",
                            Name = "BSO"
                        },
                        new
                        {
                            Id = 66,
                            Category = "Departments",
                            Name = "AD"
                        },
                        new
                        {
                            Id = 67,
                            Category = "Departments",
                            Name = "NALLRC"
                        },
                        new
                        {
                            Id = 68,
                            Category = "Departments",
                            Name = "NSTP"
                        },
                        new
                        {
                            Id = 69,
                            Category = "Departments",
                            Name = "COED"
                        },
                        new
                        {
                            Id = 70,
                            Category = "Departments",
                            Name = "ITECH"
                        },
                        new
                        {
                            Id = 71,
                            Category = "Departments",
                            Name = "CTHTM"
                        },
                        new
                        {
                            Id = 72,
                            Category = "Departments",
                            Name = "CSSD"
                        },
                        new
                        {
                            Id = 73,
                            Category = "Departments",
                            Name = "COS"
                        },
                        new
                        {
                            Id = 74,
                            Category = "Departments",
                            Name = "CPSPA"
                        },
                        new
                        {
                            Id = 75,
                            Category = "Departments",
                            Name = "CHK"
                        },
                        new
                        {
                            Id = 76,
                            Category = "Departments",
                            Name = "CE"
                        },
                        new
                        {
                            Id = 77,
                            Category = "Departments",
                            Name = "CCIS"
                        },
                        new
                        {
                            Id = 78,
                            Category = "Departments",
                            Name = "COC"
                        },
                        new
                        {
                            Id = 79,
                            Category = "Departments",
                            Name = "CBA"
                        },
                        new
                        {
                            Id = 80,
                            Category = "Departments",
                            Name = "CAL"
                        },
                        new
                        {
                            Id = 81,
                            Category = "Departments",
                            Name = "CAF"
                        },
                        new
                        {
                            Id = 82,
                            Category = "Departments",
                            Name = "COL"
                        },
                        new
                        {
                            Id = 83,
                            Category = "Departments",
                            Name = "GS"
                        },
                        new
                        {
                            Id = 84,
                            Category = "Departments",
                            Name = "OUS"
                        },
                        new
                        {
                            Id = 85,
                            Category = "Departments",
                            Name = "UPPO"
                        },
                        new
                        {
                            Id = 86,
                            Category = "Departments",
                            Name = "UTLDO"
                        },
                        new
                        {
                            Id = 87,
                            Category = "Departments",
                            Name = "FEO"
                        },
                        new
                        {
                            Id = 88,
                            Category = "Departments",
                            Name = "QAO"
                        },
                        new
                        {
                            Id = 89,
                            Category = "Departments",
                            Name = "OEVP"
                        },
                        new
                        {
                            Id = 90,
                            Category = "Departments",
                            Name = "ICT"
                        },
                        new
                        {
                            Id = 91,
                            Category = "Departments",
                            Name = "IMO"
                        },
                        new
                        {
                            Id = 92,
                            Category = "Departments",
                            Name = "IDSA"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GADApplication.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GADApplication.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GADApplication.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GADApplication.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
