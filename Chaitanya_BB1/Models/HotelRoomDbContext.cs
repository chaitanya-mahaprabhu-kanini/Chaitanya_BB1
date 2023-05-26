using Microsoft.EntityFrameworkCore;

namespace Chaitanya_BB1.Models
{
	public class HotelRoomDbContext: DbContext
	{
		public HotelRoomDbContext(DbContextOptions options) : base(options) { }
		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<Room> Rooms { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Hotel>()
				.HasMany(u => u.Rooms)
				.WithOne(p => p.Hotel)
				.HasForeignKey(p => p.Hotel);
		}
	}
}
