using Microsoft.EntityFrameworkCore;

namespace genderapi.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<Gender> Gender { get; set; } = null!;

}