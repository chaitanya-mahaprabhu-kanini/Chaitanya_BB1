﻿using Chaitanya_BB1.Models;
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
}