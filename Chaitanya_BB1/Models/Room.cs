using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chaitanya_BB1.Models
{
	public class Room
	{
		[Key]
		public int Rid { get; set; }
		public Boolean Available { get; set; }
		public int Hid { get; set; }
		[ForeignKey("Hid")]
		public Hotel Hotel { get; set; }
	}
}
