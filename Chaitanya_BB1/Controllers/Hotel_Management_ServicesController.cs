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

	//Getting details of all hotels.
	[HttpGet("Get_Hotel_Details")]
	public IEnumerable<Hotel> GetHotels()
	{
		return _hotelRepository.GetHotels();
	}

	//(LINQ) Getting details of hotel by its name.
	[HttpGet("L_Get_Hotel_By_Name")]
	public ActionResult<Hotel> GetHotelByName(string name)
	{
		var hotel = _hotelRepository.GetHotelByName(name);
		if (hotel == null)
		{
			return NotFound();
		}

		return Ok(hotel);
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

	//Authentication required to Add a new hotel.
	[Authorize]
	[HttpPost("Auth_Add_New_Hotel")]
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
	[HttpDelete("Auth_Delete_Hotel_Details_ID_Based")]
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
}
