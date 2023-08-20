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

        }
    }
}