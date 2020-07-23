namespace EntityFramework.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Task> AssignedTasks { get; set; }

        internal static void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>();
            entity.ToTable("User");

            entity.HasKey(e => e.Id).HasName("PK_UserId");
            entity.HasIndex(e => e.Email).IsUnique().HasName("UX_User_Email");

            entity.Property(e => e.Id).IsRequired().HasColumnName("UserId").ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.FirstName).IsRequired();
            entity.Property(e => e.LastName).IsRequired();
        }
    }
}