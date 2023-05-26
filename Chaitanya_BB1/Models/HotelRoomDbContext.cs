using Microsoft.EntityFrameworkCore;

namespace Chaitanya_BB1.Models
{
	public class HotelRoomDbContext: DbContext
	{
		public HotelRoomDbContext(DbContextOptions options) : base(options) { }
		
		//DbSets act like the table for the methods.
		//Changes made here are reflecte using SaveChanges().
		public DbSet<Hotel> Hotels { get; set; }
		public DbSet<Room> Rooms { get; set; }
	}
}
