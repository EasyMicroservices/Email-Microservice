using Microsoft.EntityFrameworkCore;

namespace EasyMicroservices.EmailsMicroservice.Database
{
    public interface IDatabaseBuilder
    {
        void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
    }
}
