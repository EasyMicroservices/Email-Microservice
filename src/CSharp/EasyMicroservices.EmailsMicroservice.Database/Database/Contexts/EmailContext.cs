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

                model.HasOne(x => x.SendEmails)
                .WithMany(x => x.ToEmails)
                .HasForeignKey(x => x.SendEmailId);
            });

            modelBuilder.Entity<EmailServerEntity>(model =>
            {
                model.HasKey(x => x.Id);
            });

            modelBuilder.Entity<SendEmailEntity>(model =>
            {
                model.HasKey(x => x.Id);

                model.HasOne(x => x.EmailServers)
                .WithMany(x => x.SendEmails)
                .HasForeignKey(x => x.EmailServerId);
            });
        }
    }
}