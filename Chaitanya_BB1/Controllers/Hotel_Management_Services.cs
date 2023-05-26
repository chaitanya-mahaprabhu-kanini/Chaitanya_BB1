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

	
	[HttpGet("Get_Hotel_Details")]
	public IEnumerable<Hotel> GetHotels()
	{
		return _hotelRepository.GetHotels();
	}

	
	[HttpGet("Get_Hotel_Details_ID_Based")]
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
	[HttpPost("Add_New_Hotel")]
	[Authorize]
	public ActionResult<Hotel> AddHotel(Hotel hotel)
	{
		_hotelRepository.AddHotel(hotel);
		return CreatedAtAction(nameof(GetHotel), new { id = hotel.Hid }, hotel);
	}

	[HttpPut("Update_Hotel_Details_ID_Based")]
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
	[HttpDelete("Delete_Hotel_Details_ID_Based")]
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

	[HttpGet("Filter_By_Price_Range")]
	public IEnumerable<Hotel> FilterHotelsByPriceRange(int minPrice, int maxPrice)
	{
		return _hotelRepository.FilterHotelsByPriceRange(minPrice, maxPrice);
	}

	[HttpGet("Filter_By_Location")]
	public IEnumerable<Hotel> FilterHotelsByLocation(string location)
	{
		return _hotelRepository.FilterHotelsByLocation(location);
	}

	[HttpGet("Filter_By_Amenity")]
	public IEnumerable<Hotel> FilterHotelsByAmenity(string amenity)
	{
		return _hotelRepository.FilterHotelsByAmenity(amenity);
	}


	//LINQ implementation to count number of available rooms.
	[HttpGet("Available_Rooms_Count")]
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

	//Filters data based on location and price range. Does the search dynamically. 
	//Does not need authentication
	[HttpGet("Filter_Based_On_Location_PriceRange")]
	public ActionResult<IEnumerable<Hotel>> FilterHotels(string location, int minPrice, int maxPrice)
	{
		var filteredHotels = _hotelRepository.FilterHotels(location, minPrice, maxPrice);
		return Ok(filteredHotels);
	}
}
