using System.ComponentModel.DataAnnotations;

namespace Chaitanya_BB1.Models
{
	public class Room
	{
		[Key]
		public int Rid { get; set; }
		public Boolean Available { get; set; }
		public Hotel Hotel { get; set; }
	}
}
