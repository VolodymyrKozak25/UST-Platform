using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public partial class UstdbContext : IdentityDbContext<User>
{
    public UstdbContext()
    {
    }

    public UstdbContext(DbContextOptions<UstdbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Models.Group> Groups { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Studentresult> Studentresults { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=USTDB;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("answers_pkey");

            entity.ToTable("answers");

            entity.Property(e => e.AnswerId).HasColumnName("answer_id");
            entity.Property(e => e.AnswerText).HasColumnName("answer_text");
            entity.Property(e => e.IsCorrect)
                .HasDefaultValueSql("false")
                .HasColumnName("is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("question_answer");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.HasIndex(e => e.CourseKey, "courses_course_key_key").IsUnique();

            entity.HasIndex(e => e.Name, "courses_name_key").IsUnique();

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseKey).HasColumnName("course_key");
            entity.Property(e => e.MaxTeachers)
                .HasDefaultValueSql("1")
                .HasColumnName("max_teachers");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Models.Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("groups_pkey");

            entity.ToTable("groups");

            entity.HasIndex(e => e.Name, "groups_name_key").IsUnique();

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(24)
                .HasColumnName("name");

            entity.HasMany(d => d.Courses).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "Enrollment",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("enrollment_course_id_fkey"),
                    l => l.HasOne<Models.Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .HasConstraintName("enrollment_group_id_fkey"),
                    j =>
                    {
                        j.HasKey("GroupId", "CourseId").HasName("enrollment_id");
                        j.ToTable("enrollment");
                        j.IndexerProperty<int>("GroupId").HasColumnName("group_id");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                    });
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("questions_pkey");

            entity.ToTable("questions");

            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.QuestionText).HasColumnName("question_text");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.TestId).HasColumnName("test_id");

            entity.HasOne(d => d.Test).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("test_question");
        });

        modelBuilder.Entity<Studentresult>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.TestId, e.AnswerId }).HasName("sresult_id");

            entity.ToTable("studentresults");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.AnswerId).HasColumnName("answer_id");

            entity.HasOne(d => d.Answer).WithMany(p => p.Studentresults)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("studentresults_answer_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Studentresults)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("studentresults_student_id_fkey");

            entity.HasOne(d => d.Test).WithMany(p => p.Studentresults)
                .HasForeignKey(d => d.TestId)
                .HasConstraintName("studentresults_test_id_fkey");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("tests_pkey");

            entity.ToTable("tests");

            entity.HasIndex(e => e.Name, "tests_name_key").IsUnique();

            entity.Property(e => e.TestId).HasColumnName("test_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.FinalDate).HasColumnName("final_date");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.TimeLimit).HasColumnName("time_limit");

            entity.HasOne(d => d.Course).WithMany(p => p.Tests)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("course_test");

            entity.HasMany(d => d.Groups).WithMany(p => p.Tests)
                .UsingEntity<Dictionary<string, object>>(
                    "TestAvailability",
                    r => r.HasOne<Models.Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .HasConstraintName("testAvailability_group_id_fkey"),
                    l => l.HasOne<Test>().WithMany()
                        .HasForeignKey("TestId")
                        .HasConstraintName("testAvailability_test_id_fkey"),
                    j =>
                    {
                        j.HasKey("TestId", "GroupId").HasName("test_availability_id");
                        j.ToTable("testAvailability");
                        j.IndexerProperty<int>("TestId").HasColumnName("test_id");
                        j.IndexerProperty<int>("GroupId").HasColumnName("group_id");
                    });
        });
    }
}
