using Microsoft.EntityFrameworkCore;
using MigrationMean.Models;
namespace MigrationMean.data;

public class JobContext : DbContext
{
    
    public DbSet<Job> Jobs { get; set; }
}
