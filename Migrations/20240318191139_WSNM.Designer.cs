﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManager.Data;

#nullable disable

namespace ProjectManager.Migrations
{
    [DbContext(typeof(ProjectManager_v00_DbContext))]
    [Migration("20240318191139_WSNM")]
    partial class WSNM
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectManager.Models.CardFormat", b =>
                {
                    b.Property<int>("CardFormatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CardFormatID"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.HasKey("CardFormatID");

                    b.ToTable("CardFormat");
                });

            modelBuilder.Entity("ProjectManager.Models.Field", b =>
                {
                    b.Property<int>("FieldID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FieldID"));

                    b.Property<int?>("CardFormatID")
                        .HasColumnType("int");

                    b.Property<string>("FieldName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FieldID");

                    b.HasIndex("CardFormatID");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("ProjectManager.Models.Job", b =>
                {
                    b.Property<int>("JobID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobID"));

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<double>("TimeEstimation")
                        .HasColumnType("float");

                    b.HasKey("JobID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("ProjectManager.Models.JobComponent", b =>
                {
                    b.Property<int>("JobComponentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobComponentID"));

                    b.Property<int>("ComponentID")
                        .HasColumnType("int");

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.HasKey("JobComponentID");

                    b.HasIndex("ComponentID");

                    b.HasIndex("JobID");

                    b.ToTable("JobComponent");
                });

            modelBuilder.Entity("ProjectManager.Models.JobPerson", b =>
                {
                    b.Property<int>("JobPersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobPersonID"));

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("JobPersonID");

                    b.HasIndex("JobID");

                    b.HasIndex("PersonID");

                    b.ToTable("JobPerson");
                });

            modelBuilder.Entity("ProjectManager.Models.JobRequirement", b =>
                {
                    b.Property<int>("JobRequirementID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobRequirementID"));

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.Property<int>("RequirementID")
                        .HasColumnType("int");

                    b.HasKey("JobRequirementID");

                    b.HasIndex("JobID");

                    b.HasIndex("RequirementID");

                    b.ToTable("JobRequirement");
                });

            modelBuilder.Entity("ProjectManager.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<string>("FamilyNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GivenNames")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("ProjectManager.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectID"));

                    b.Property<int?>("FieldID")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectID");

                    b.HasIndex("FieldID");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("ProjectManager.Models.WorkSection", b =>
                {
                    b.Property<int>("WorkSectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WorkSectionID"));

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<int>("JobID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("WorkSectionID");

                    b.HasIndex("JobID");

                    b.HasIndex("PersonID");

                    b.ToTable("WorkSection");
                });

            modelBuilder.Entity("ProjectManager.Models.Field", b =>
                {
                    b.HasOne("ProjectManager.Models.CardFormat", "CardFormat")
                        .WithMany()
                        .HasForeignKey("CardFormatID");

                    b.Navigation("CardFormat");
                });

            modelBuilder.Entity("ProjectManager.Models.Job", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany("Jobs")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManager.Models.JobComponent", b =>
                {
                    b.HasOne("ProjectManager.Models.Job", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.Job", "Job")
                        .WithMany("JobComponents")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Component");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("ProjectManager.Models.JobPerson", b =>
                {
                    b.HasOne("ProjectManager.Models.Job", "Job")
                        .WithMany("JobPeople")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.Person", "Person")
                        .WithMany("JobPeople")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ProjectManager.Models.JobRequirement", b =>
                {
                    b.HasOne("ProjectManager.Models.Job", "Job")
                        .WithMany("JobRequirements")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.Job", "Requirement")
                        .WithMany()
                        .HasForeignKey("RequirementID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Requirement");
                });

            modelBuilder.Entity("ProjectManager.Models.Project", b =>
                {
                    b.HasOne("ProjectManager.Models.Field", "Field")
                        .WithMany("Projects")
                        .HasForeignKey("FieldID");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("ProjectManager.Models.WorkSection", b =>
                {
                    b.HasOne("ProjectManager.Models.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.Person", "Person")
                        .WithMany("WorkSections")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("ProjectManager.Models.Field", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProjectManager.Models.Job", b =>
                {
                    b.Navigation("JobComponents");

                    b.Navigation("JobPeople");

                    b.Navigation("JobRequirements");
                });

            modelBuilder.Entity("ProjectManager.Models.Person", b =>
                {
                    b.Navigation("JobPeople");

                    b.Navigation("WorkSections");
                });

            modelBuilder.Entity("ProjectManager.Models.Project", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
