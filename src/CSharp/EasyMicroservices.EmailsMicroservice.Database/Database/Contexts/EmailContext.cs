using EasyMicroservices.EmailsMicroservice.Database.Entities;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.EmailsMicroservice.Database.Contexts
{
    public class EmailContext : RelationalCoreContext
    {
        IDatabaseBuilder _builder;
        public EmailContext(IDatabaseBuilder builder)
        {
            _builder = builder;
        }
        public DbSet<EmailEntity> Emails { get; set; }
        public DbSet<ServerEntity> EmailServers { get; set; }
        public DbSet<QueueEntity> QueueEmails { get; set; }
        public DbSet<SendEmailEntity> SendEmails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_builder != null)
                _builder.OnConfiguring(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmailEntity>(model =>
            {
                model.HasKey(x => x.Id);
            });

            modelBuilder.Entity<ServerEntity>(model =>
            {
                model.HasKey(x => x.Id);
            });

            modelBuilder.Entity<QueueEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.Server)
                .WithMany(x => x.Queues)
                .HasForeignKey(x => x.ServerId);

                model.HasOne(x => x.FromEmail)
                .WithMany(x => x.Queues)
                .HasForeignKey(x => x.FromEmailId);
            });
            modelBuilder.Entity<SendEmailEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.Queue)
                .WithMany(x => x.SendEmails)
                .HasForeignKey(x => x.QueueId);

            });
        }
    }
}