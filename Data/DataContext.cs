namespace EntityFramework
{
    using System;
    using System.Linq;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);

            User.OnModelCreating(modelBuilder);
            Task.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<Task>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entity in entities)
            {
                var now = DateTimeOffset.UtcNow;
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.Created = now;
                        entity.Entity.Modified = now;
                        break;
                    case EntityState.Modified:
                        entity.Entity.Modified = now;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
