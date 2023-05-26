using Chaitanya_BB1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class HotelRepository : IHotelsRepository
{
	private readonly HotelRoomDbContext _context;

	public HotelRepository(HotelRoomDbContext context)
	{
		_context = context;
	}

	public IEnumerable<Hotel> GetHotels()
	{
		return _context.Hotels.ToList();
	}

	public Hotel GetHotelById(int id)
	{
		return _context.Hotels.Find(id);
	}

	public void AddHotel(Hotel hotel)
	{
		_context.Hotels.Add(hotel);
		_context.SaveChanges();
	}

	public void UpdateHotel(Hotel hotel)
	{
		_context.Hotels.Update(hotel);
		_context.SaveChanges();
	}

	public void DeleteHotel(int id)
	{
		var hotel = _context.Hotels.Find(id);
		if (hotel != null)
		{
			_context.Hotels.Remove(hotel);
			_context.SaveChanges();
		}
	}

	public IEnumerable<Hotel> FilterHotelsByPriceRange(int minPrice, int maxPrice)
	{
		return _context.Hotels.Where(h => h.Price >= minPrice && h.Price <= maxPrice).ToList();
	}

	public IEnumerable<Hotel> FilterHotelsByLocation(string location)
	{
		return _context.Hotels.Where(h => h.Location.ToLower().Contains(location.ToLower())).ToList();
	}

	public IEnumerable<Hotel> FilterHotelsByAmenity(string amenity)
	{
		return _context.Hotels.Where(h => h.Amenity.ToLower().Contains(amenity.ToLower())).ToList();
	}
	public int CountAvailableRooms(int hotelId)
	{
		try
		{
			var hotel = _context.Hotels.Include(h => h.Rooms).FirstOrDefault(h => h.Hid == hotelId);
			if (hotel == null)
			{
				throw new ArgumentException("Invalid hotelId");
			}

			return hotel.Rooms.Count(room => room.Available != 1);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error Encountered - " + ex);
			throw; // Rethrow the exception
		}
	}

	//Filters data based on location and price range. Does the search dynamically.
	public IEnumerable<Hotel> FilterHotels(string location, int minPrice, int maxPrice)
	{
		var query = _context.Hotels.AsQueryable();

		if (!string.IsNullOrEmpty(location))
		{
			query = query.Where(h => h.Location == location);
		}

		if (minPrice > 0)
		{
			query = query.Where(h => h.Price >= minPrice);
		}

		if (maxPrice > 0)
		{
			query = query.Where(h => h.Price <= maxPrice);
		}

		return query.ToList();
	}

	//Linq implementation to get hotel details by name.
	public Hotel GetHotelByName(string name)
	{
		return _context.Hotels.FirstOrDefault(h => h.Hname == name);
	}
}
