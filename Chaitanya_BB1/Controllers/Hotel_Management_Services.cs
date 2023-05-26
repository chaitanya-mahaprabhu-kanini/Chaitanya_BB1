using Chaitanya_BB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/Hotel_Management_Services")]
[ApiController]
public class Hotel_Management_Services : ControllerBase
{
	private readonly IHotelsRepository _hotelRepository;

	public Hotel_Management_Services(IHotelsRepository hotelRepository)
	{
		_hotelRepository = hotelRepository;
	}

	
	[HttpGet]
	public IEnumerable<Hotel> GetHotels()
	{
		return _hotelRepository.GetHotels();
	}

	
	[HttpGet("{id}")]
	public ActionResult<Hotel> GetHotel(int id)
	{
		var hotel = _hotelRepository.GetHotelById(id);
		if (hotel == null)
		{
			return NotFound();
		}
		return hotel;
	}

	//Authorization needed to Add a new hotel.
	[HttpPost]
	[Authorize]
	public ActionResult<Hotel> AddHotel(Hotel hotel)
	{
		_hotelRepository.AddHotel(hotel);
		return CreatedAtAction(nameof(GetHotel), new { id = hotel.Hid }, hotel);
	}

	[HttpPut("{id}")]
	public IActionResult UpdateHotel(int id, Hotel hotel)
	{
		if (id != hotel.Hid)
		{
			return BadRequest();
		}

		var existingHotel = _hotelRepository.GetHotelById(id);
		if (existingHotel == null)
		{
			return NotFound();
		}

		existingHotel.Hname = hotel.Hname;
		existingHotel.Location = hotel.Location;
		existingHotel.Price = hotel.Price;
		existingHotel.Amenity = hotel.Amenity;

		_hotelRepository.UpdateHotel(existingHotel);

		return NoContent();
	}

	//Authorization required to "Delete a Hotel"
	[Authorize]
	[HttpDelete("{id}")]
	public IActionResult DeleteHotel(int id)
	{
		var hotel = _hotelRepository.GetHotelById(id);
		if (hotel == null)
		{
			return NotFound();
		}

		_hotelRepository.DeleteHotel(id);

		return NoContent();
	}

	[HttpGet("filterByPriceRange")]
	public IEnumerable<Hotel> FilterHotelsByPriceRange(int minPrice, int maxPrice)
	{
		return _hotelRepository.FilterHotelsByPriceRange(minPrice, maxPrice);
	}

	[HttpGet("filterByLocation")]
	public IEnumerable<Hotel> FilterHotelsByLocation(string location)
	{
		return _hotelRepository.FilterHotelsByLocation(location);
	}

	[HttpGet("filterByAmenity")]
	public IEnumerable<Hotel> FilterHotelsByAmenity(string amenity)
	{
		return _hotelRepository.FilterHotelsByAmenity(amenity);
	}

	[HttpGet("{id}/availableRoomsCount")]
	public ActionResult<int> CountAvailableRooms(int id)
	{
		try
		{
			var hotel = _hotelRepository.GetHotelById(id);
			if (hotel == null)
			{
				return NotFound();
			}

			int availableRoomsCount = _hotelRepository.CountAvailableRooms(id);
			return availableRoomsCount;
		}
		catch (Exception ex)
		{
			return StatusCode(500, "Sorry! :( .An error occurred while retrieving the available rooms count.");
		}
	}
}
