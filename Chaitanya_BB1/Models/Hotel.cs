﻿using System.ComponentModel.DataAnnotations;

namespace Chaitanya_BB1.Models
{
	public class Hotel
	{
		[Key]
		public int Hid { get; set; }
		public string Hname { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;	
		public int Price { get; set; }
		public string Amenity { get; set; } = string.Empty;
		public ICollection<Room> Rooms { get; set; }
	}
}
