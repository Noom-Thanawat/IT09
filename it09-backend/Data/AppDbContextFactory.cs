using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace it09_backend.Data
{
	public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

			optionsBuilder.UseNpgsql(
				"Host=localhost;Port=5432;Database=it09db;Username=postgres;Password=postgres");

			return new AppDbContext(optionsBuilder.Options);
		}
	}
}
