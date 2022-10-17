using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notetaking.Models.DomainModels;

namespace Notetaking.Data
{
    public class NotesDbContext : DbContext
    {   //Creating a database code first
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {
        }
        public DbSet<Note> Notes => Set<Note>();

        public DbSet<User> Users => Set<User>();
    }
}
