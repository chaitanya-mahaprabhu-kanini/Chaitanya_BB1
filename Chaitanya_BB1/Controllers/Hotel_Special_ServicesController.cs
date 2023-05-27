using Chaitanya_BB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/Hotel_Special_Services")]
[ApiController]
public class Hotel_Special_Services : ControllerBase
{
	private readonly IHotelsRepository _hotelRepository;

	public Hotel_Special_Services(IHotelsRepository hotelRepository)
	{
		_hotelRepository = hotelRepository;
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
	[HttpGet("L_Available_Rooms_Count")]
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
