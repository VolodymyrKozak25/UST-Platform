using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context;

public partial class UstdbContext : DbContext
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

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Studentresult> Studentresults { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=USTDB;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("groups_pkey");

            entity.ToTable("groups");

            entity.HasIndex(e => e.Name, "groups_name_key").IsUnique();

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(12)
                .HasColumnName("name");

            entity.HasMany(d => d.Courses).WithMany(p => p.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    "Enrollment",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("enrollment_course_id_fkey"),
                    l => l.HasOne<Group>().WithMany()
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

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.HasOne(d => d.Group).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("group_student");

            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("students_user_id_fkey");

            entity.HasMany(d => d.Tests).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "Result",
                    r => r.HasOne<Test>().WithMany()
                        .HasForeignKey("TestId")
                        .HasConstraintName("results_test_id_fkey"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("results_student_id_fkey"),
                    j =>
                    {
                        j.HasKey("StudentId", "TestId").HasName("results_id");
                        j.ToTable("results");
                        j.IndexerProperty<int>("StudentId").HasColumnName("student_id");
                        j.IndexerProperty<int>("TestId").HasColumnName("test_id");
                    });
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

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("teachers_pkey");

            entity.ToTable("teachers");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teachers_user_id_fkey");

            entity.HasMany(d => d.Courses).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "Teachercourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .HasConstraintName("teachercourses_course_id_fkey"),
                    l => l.HasOne<Teacher>().WithMany()
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("teachercourses_teacher_id_fkey"),
                    j =>
                    {
                        j.HasKey("TeacherId", "CourseId").HasName("tcourse_id");
                        j.ToTable("teachercourses");
                        j.IndexerProperty<int>("TeacherId").HasColumnName("teacher_id");
                        j.IndexerProperty<int>("CourseId").HasColumnName("course_id");
                    });
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
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(16)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(16)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(16)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.UserType).HasColumnName("user_type");
        });
    }
}
