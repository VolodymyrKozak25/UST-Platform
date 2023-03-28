﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(UstdbContext))]
    [Migration("20230325140015_InitializeDB")]
    partial class InitializeDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("answer_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnswerId"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("answer_text");

                    b.Property<bool?>("IsCorrect")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasColumnName("is_correct")
                        .HasDefaultValueSql("false");

                    b.Property<int>("QuestionId")
                        .HasColumnType("integer")
                        .HasColumnName("question_id");

                    b.HasKey("AnswerId")
                        .HasName("answers_pkey");

                    b.HasIndex("QuestionId");

                    b.ToTable("answers", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseId"));

                    b.Property<int>("CourseKey")
                        .HasColumnType("integer")
                        .HasColumnName("course_key");

                    b.Property<int?>("MaxTeachers")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("max_teachers")
                        .HasDefaultValueSql("1");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.HasKey("CourseId")
                        .HasName("courses_pkey");

                    b.HasIndex(new[] { "CourseKey" }, "courses_course_key_key")
                        .IsUnique();

                    b.HasIndex(new[] { "Name" }, "courses_name_key")
                        .IsUnique();

                    b.ToTable("courses", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("GroupId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("name");

                    b.HasKey("GroupId")
                        .HasName("groups_pkey");

                    b.HasIndex(new[] { "Name" }, "groups_name_key")
                        .IsUnique();

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("question_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("QuestionId"));

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("question_text");

                    b.Property<int>("Score")
                        .HasColumnType("integer")
                        .HasColumnName("score");

                    b.Property<int>("TestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    b.HasKey("QuestionId")
                        .HasName("questions_pkey");

                    b.HasIndex("TestId");

                    b.ToTable("questions", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Student", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.HasKey("UserId")
                        .HasName("students_pkey");

                    b.HasIndex("GroupId");

                    b.ToTable("students", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Studentresult", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("integer")
                        .HasColumnName("student_id");

                    b.Property<int>("TestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    b.Property<int>("AnswerId")
                        .HasColumnType("integer")
                        .HasColumnName("answer_id");

                    b.HasKey("StudentId", "TestId", "AnswerId")
                        .HasName("sresult_id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("TestId");

                    b.ToTable("studentresults", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Teacher", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("UserId")
                        .HasName("teachers_pkey");

                    b.ToTable("teachers", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TestId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<DateTime>("FinalDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("final_date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("name");

                    b.Property<TimeSpan>("TimeLimit")
                        .HasColumnType("interval")
                        .HasColumnName("time_limit");

                    b.HasKey("TestId")
                        .HasName("tests_pkey");

                    b.HasIndex("CourseId");

                    b.HasIndex(new[] { "Name" }, "tests_name_key")
                        .IsUnique();

                    b.ToTable("tests", (string)null);
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("middle_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_type");

                    b.HasKey("UserId")
                        .HasName("users_pkey");

                    b.HasIndex(new[] { "Email" }, "users_email_key")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Enrollment", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.HasKey("GroupId", "CourseId")
                        .HasName("enrollment_id");

                    b.HasIndex("CourseId");

                    b.ToTable("enrollment", (string)null);
                });

            modelBuilder.Entity("Result", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("integer")
                        .HasColumnName("student_id");

                    b.Property<int>("TestId")
                        .HasColumnType("integer")
                        .HasColumnName("test_id");

                    b.HasKey("StudentId", "TestId")
                        .HasName("results_id");

                    b.HasIndex("TestId");

                    b.ToTable("results", (string)null);
                });

            modelBuilder.Entity("Teachercourse", b =>
                {
                    b.Property<int>("TeacherId")
                        .HasColumnType("integer")
                        .HasColumnName("teacher_id");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.HasKey("TeacherId", "CourseId")
                        .HasName("tcourse_id");

                    b.HasIndex("CourseId");

                    b.ToTable("teachercourses", (string)null);
                });

            modelBuilder.Entity("DAL.Models.Answer", b =>
                {
                    b.HasOne("DAL.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("question_answer");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("DAL.Models.Question", b =>
                {
                    b.HasOne("DAL.Models.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("test_question");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("DAL.Models.Student", b =>
                {
                    b.HasOne("DAL.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("group_student");

                    b.HasOne("DAL.Models.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("DAL.Models.Student", "UserId")
                        .IsRequired()
                        .HasConstraintName("students_user_id_fkey");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Studentresult", b =>
                {
                    b.HasOne("DAL.Models.Answer", "Answer")
                        .WithMany("Studentresults")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("studentresults_answer_id_fkey");

                    b.HasOne("DAL.Models.Student", "Student")
                        .WithMany("Studentresults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("studentresults_student_id_fkey");

                    b.HasOne("DAL.Models.Test", "Test")
                        .WithMany("Studentresults")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("studentresults_test_id_fkey");

                    b.Navigation("Answer");

                    b.Navigation("Student");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("DAL.Models.Teacher", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithOne("Teacher")
                        .HasForeignKey("DAL.Models.Teacher", "UserId")
                        .IsRequired()
                        .HasConstraintName("teachers_user_id_fkey");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.Test", b =>
                {
                    b.HasOne("DAL.Models.Course", "Course")
                        .WithMany("Tests")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("course_test");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Enrollment", b =>
                {
                    b.HasOne("DAL.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("enrollment_course_id_fkey");

                    b.HasOne("DAL.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("enrollment_group_id_fkey");
                });

            modelBuilder.Entity("Result", b =>
                {
                    b.HasOne("DAL.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("results_student_id_fkey");

                    b.HasOne("DAL.Models.Test", null)
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("results_test_id_fkey");
                });

            modelBuilder.Entity("Teachercourse", b =>
                {
                    b.HasOne("DAL.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("teachercourses_course_id_fkey");

                    b.HasOne("DAL.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("teachercourses_teacher_id_fkey");
                });

            modelBuilder.Entity("DAL.Models.Answer", b =>
                {
                    b.Navigation("Studentresults");
                });

            modelBuilder.Entity("DAL.Models.Course", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("DAL.Models.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("DAL.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("DAL.Models.Student", b =>
                {
                    b.Navigation("Studentresults");
                });

            modelBuilder.Entity("DAL.Models.Test", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("Studentresults");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
