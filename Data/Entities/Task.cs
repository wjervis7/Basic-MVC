namespace EntityFramework.Entities
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class Task
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Assignee { get; set; }
        public DateTimeOffset? DueDate { get;set; }
        public DateTimeOffset Created { get;set; }
        public DateTimeOffset Modified { get;set; }

        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Task>();
            entity.ToTable("Task");

            entity.HasKey(e => e.Id).HasName("PK_TaskId");
            entity
                .HasOne(e => e.Assignee)
                .WithMany(e => e.AssignedTasks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey("FK_User_isassigned_Tasks");

            entity.Property(e => e.Id).IsRequired().HasColumnName("TaskId").ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.DueDate).IsRequired(false);
            entity.Property(e => e.Created).IsRequired().ValueGeneratedOnAdd();
            entity.Property(e => e.Modified).IsRequired().ValueGeneratedOnAddOrUpdate();
        }
    }
}