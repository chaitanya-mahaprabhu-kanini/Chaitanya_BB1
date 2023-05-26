using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chaitanya_BB1.Models
{
	public class Room
	{
		//Room Id acts like the primary key.
		[Key]
		public int Rid { get; set; }
		public int Available { get; set; }

		//Foreign Key.
		public int Hid { get; set; }
		[ForeignKey("Hid")]

		//One room has one hotel.
		public Hotel Hotel { get; set; }
	}
}
