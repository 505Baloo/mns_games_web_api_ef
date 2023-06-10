using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MNSGamesWebAPI.Models
{
    public partial class MNS_Games_DBContext : DbContext
    {
        public MNS_Games_DBContext()
        {
        }

        public MNS_Games_DBContext(DbContextOptions<MNS_Games_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<Badge> Badges { get; set; } = null!;
        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<Obtain> Obtains { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<Quiz> Quizzes { get; set; } = null!;
        public virtual DbSet<Registrate> Registrates { get; set; } = null!;
        public virtual DbSet<Theme> Themes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LabelAnswer)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Answer__Question__339FAB6E");
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AppUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoginNickname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoginPassword)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.StreetName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.StreetNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Badge>(entity =>
            {
                entity.ToTable("Badge");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descript)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ObtainingConditions)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Game__AppUserID__37703C52");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Game__QuizID__367C1819");
            });

            modelBuilder.Entity<Obtain>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.AnswerId, e.GameId })
                    .HasName("PK__Obtain__8CA2F2190CD631C4");

                entity.ToTable("Obtain");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.GameId).HasColumnName("GameID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Obtains)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Obtain__AnswerID__46B27FE2");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Obtains)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Obtain__GameID__47A6A41B");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Obtains)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Obtain__Question__45BE5BA9");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LabelQuestion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Question__QuizID__30C33EC3");

                entity.HasMany(d => d.AnswersNavigation)
                    .WithMany(p => p.Questions)
                    .UsingEntity<Dictionary<string, object>>(
                        "Correspond",
                        l => l.HasOne<Answer>().WithMany().HasForeignKey("AnswerId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Correspon__Answe__3F115E1A"),
                        r => r.HasOne<Question>().WithMany().HasForeignKey("QuestionId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Correspon__Quest__3E1D39E1"),
                        j =>
                        {
                            j.HasKey("QuestionId", "AnswerId").HasName("PK__Correspo__50884A8EEFD5AC3A");

                            j.ToTable("Correspond");

                            j.IndexerProperty<int>("QuestionId").HasColumnName("QuestionID");

                            j.IndexerProperty<int>("AnswerId").HasColumnName("AnswerID");
                        });
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quiz");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.Property(e => e.QuizName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ThemeId).HasColumnName("ThemeID");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quiz__AppUserID__2DE6D218");

                entity.HasOne(d => d.Theme)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Quiz__ThemeID__2CF2ADDF");

                entity.HasMany(d => d.Badges)
                    .WithMany(p => p.Quizzes)
                    .UsingEntity<Dictionary<string, object>>(
                        "Attribute",
                        l => l.HasOne<Badge>().WithMany().HasForeignKey("BadgeId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Attribute__Badge__3B40CD36"),
                        r => r.HasOne<Quiz>().WithMany().HasForeignKey("QuizId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Attribute__QuizI__3A4CA8FD"),
                        j =>
                        {
                            j.HasKey("QuizId", "BadgeId").HasName("PK__Attribut__5AD32C59DDC7188A");

                            j.ToTable("Attribute");

                            j.IndexerProperty<int>("QuizId").HasColumnName("QuizID");

                            j.IndexerProperty<int>("BadgeId").HasColumnName("BadgeID");
                        });
            });

            modelBuilder.Entity<Registrate>(entity =>
            {
                entity.HasKey(e => new { e.QuizId, e.AppUserId })
                    .HasName("PK__Registra__9484F21B2F819AC1");

                entity.ToTable("Registrate");

                entity.Property(e => e.QuizId).HasColumnName("QuizID");

                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Registrates)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registrat__AppUs__42E1EEFE");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Registrates)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Registrat__QuizI__41EDCAC5");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.ToTable("Theme");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
