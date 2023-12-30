﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vaccinatedapi.data;

#nullable disable

namespace Vaccinatedapi.Migrations
{
    [DbContext(typeof(dbdatacontexts))]
    [Migration("20230505175602_1st")]
    partial class _1st
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Vaccinatedapi.models.User", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Vaccinatedapi.models.advices", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<string>("desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("advices");
                });

            modelBuilder.Entity("Vaccinatedapi.models.cities", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<string>("decs")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("Vaccinatedapi.models.dose", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("doses");
                });

            modelBuilder.Entity("Vaccinatedapi.models.hospital", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("city_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("phone_number")
                        .HasColumnType("float");

                    b.Property<int>("type_id")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("city_id");

                    b.HasIndex("type_id");

                    b.ToTable("hospitals");
                });

            modelBuilder.Entity("Vaccinatedapi.models.hospital_type", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<string>("desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("hospital_Types");
                });

            modelBuilder.Entity("Vaccinatedapi.models.kid_vaccine", b =>
                {
                    b.Property<int>("kids_Id")
                        .HasColumnType("int");

                    b.Property<int>("vaccines_Id")
                        .HasColumnType("int");

                    b.Property<bool>("taken")
                        .HasColumnType("bit");

                    b.HasKey("kids_Id", "vaccines_Id");

                    b.HasIndex("vaccines_Id");

                    b.ToTable("kid_Vaccines");
                });

            modelBuilder.Entity("Vaccinatedapi.models.kids", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("blood")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("father_id")
                        .HasColumnType("int");

                    b.Property<int>("host_id")
                        .HasColumnType("int");

                    b.Property<int?>("motherID")
                        .HasColumnType("int");

                    b.Property<int?>("mother_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("pirth_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("pirth_place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("father_id");

                    b.HasIndex("host_id");

                    b.HasIndex("motherID");

                    b.ToTable("kids");
                });

            modelBuilder.Entity("Vaccinatedapi.models.parents", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("gender")
                        .HasColumnType("int");

                    b.Property<int>("id_card_number")
                        .HasColumnType("int");

                    b.Property<string>("image_path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("marital_status")
                        .HasColumnType("int");

                    b.Property<int?>("mobile_number")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("phone_number")
                        .HasColumnType("int");

                    b.Property<DateTime?>("pirth_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("pirth_place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("parents");
                });

            modelBuilder.Entity("Vaccinatedapi.models.parents_kids", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<int>("kids_id")
                        .HasColumnType("int");

                    b.Property<int>("parents_id")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("parents_Kids");
                });

            modelBuilder.Entity("Vaccinatedapi.models.vaccine", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"));

                    b.Property<int>("days_to_take")
                        .HasColumnType("int");

                    b.Property<string>("desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("dose_id")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("dose_id");

                    b.ToTable("vaccine");
                });

            modelBuilder.Entity("Vaccinatedapi.models.hospital", b =>
                {
                    b.HasOne("Vaccinatedapi.models.cities", "city")
                        .WithMany("hospitals")
                        .HasForeignKey("city_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vaccinatedapi.models.hospital_type", "hospital_Type")
                        .WithMany("hospitals")
                        .HasForeignKey("type_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("city");

                    b.Navigation("hospital_Type");
                });

            modelBuilder.Entity("Vaccinatedapi.models.kid_vaccine", b =>
                {
                    b.HasOne("Vaccinatedapi.models.kids", null)
                        .WithMany()
                        .HasForeignKey("kids_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vaccinatedapi.models.vaccine", null)
                        .WithMany()
                        .HasForeignKey("vaccines_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vaccinatedapi.models.kids", b =>
                {
                    b.HasOne("Vaccinatedapi.models.parents", "father")
                        .WithMany("kids")
                        .HasForeignKey("father_id");

                    b.HasOne("Vaccinatedapi.models.hospital", "hospital")
                        .WithMany("kids")
                        .HasForeignKey("host_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Vaccinatedapi.models.parents", "mother")
                        .WithMany()
                        .HasForeignKey("motherID");

                    b.Navigation("father");

                    b.Navigation("hospital");

                    b.Navigation("mother");
                });

            modelBuilder.Entity("Vaccinatedapi.models.vaccine", b =>
                {
                    b.HasOne("Vaccinatedapi.models.dose", "dose")
                        .WithMany("vaccines")
                        .HasForeignKey("dose_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dose");
                });

            modelBuilder.Entity("Vaccinatedapi.models.cities", b =>
                {
                    b.Navigation("hospitals");
                });

            modelBuilder.Entity("Vaccinatedapi.models.dose", b =>
                {
                    b.Navigation("vaccines");
                });

            modelBuilder.Entity("Vaccinatedapi.models.hospital", b =>
                {
                    b.Navigation("kids");
                });

            modelBuilder.Entity("Vaccinatedapi.models.hospital_type", b =>
                {
                    b.Navigation("hospitals");
                });

            modelBuilder.Entity("Vaccinatedapi.models.parents", b =>
                {
                    b.Navigation("kids");
                });
#pragma warning restore 612, 618
        }
    }
}
