using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notetaking.Models.DomainModels;

namespace Notetaking.Data
{
    public class UserDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}