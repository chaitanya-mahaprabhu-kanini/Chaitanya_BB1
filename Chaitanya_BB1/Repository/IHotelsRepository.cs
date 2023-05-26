using Chaitanya_BB1.Models;
using System.Collections.Generic;

//Interface contains the necessary functionalities that mandatorily have to be implemented.
//Repository acts like a middle-man to provide more security and modularity.
public interface IHotelsRepository
{
	IEnumerable<Hotel> GetHotels();
	Hotel GetHotelById(int id);
	void AddHotel(Hotel hotel);
	void UpdateHotel(Hotel hotel);
	void DeleteHotel(int id);
	public IEnumerable<Hotel> FilterHotelsByPriceRange(int minPrice, int maxPrice);
	public IEnumerable<Hotel> FilterHotelsByLocation(string location);
	public IEnumerable<Hotel> FilterHotelsByAmenity(string amenity);
	int CountAvailableRooms(int hotelId);
	IEnumerable<Hotel> FilterHotels(string location, int minPrice, int maxPrice);

}
