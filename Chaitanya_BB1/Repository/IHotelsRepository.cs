using Chaitanya_BB1.Models;
using System.Collections.Generic;

public interface IHotelsRepository
{
	IEnumerable<Hotel> GetHotels();
	Hotel GetHotelById(int id);
	void AddHotel(Hotel hotel);
	void UpdateHotel(Hotel hotel);
	void DeleteHotel(int id);
}
