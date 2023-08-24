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
        public DbSet<EmailServerEntity> EmailServers { get; set; }
        public DbSet<QueueEmailEntity> QueueEmails { get; set; }
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

            modelBuilder.Entity<EmailServerEntity>(model =>
            {
                model.HasKey(x => x.Id);
            });

            modelBuilder.Entity<QueueEmailEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.EmailServers)
                .WithMany(x => x.QueueEmails)
                .HasForeignKey(x => x.EmailServerId);

                model.HasOne(x => x.Emails)
                .WithMany(x => x.QueueEmails)
                .HasForeignKey(x => x.FromEmailId);
            });
            modelBuilder.Entity<SendEmailEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.QueueEmails)
                .WithMany(x => x.SendEmails)
                .HasForeignKey(x => x.QueueEmailId);

            });
        }
    }
}