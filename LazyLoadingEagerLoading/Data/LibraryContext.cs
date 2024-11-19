using LazyLoadingEagerLoading.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LazyLoadingEagerLoading.Data
{
    public class LibraryContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
    }
}
