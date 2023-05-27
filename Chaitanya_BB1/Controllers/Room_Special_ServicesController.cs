using Chaitanya_BB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/Room_Management_Services")]
[ApiController]
public class Room_Special_Services : ControllerBase
{
	private readonly IRoomsRepository _roomRepository;

	public Room_Special_Services(IRoomsRepository roomRepository)
	{
		_roomRepository = roomRepository;
	}

	//LINQ for getting count of rooms under a hotel.
	[HttpGet("L_Count_Rooms_In_Hotel")]
	public ActionResult<int> CountRoomsByHotel(int hotelId)
	{
		int roomCount = _roomRepository.CountRoomsByHotel(hotelId);
		return roomCount;
	}

	//Linq implementation to get the availability of room based on its Rid.
	[HttpGet("L_Availability_By_Room_ID")]
	public String GetRoomAvailability(int roomId)
	{
		bool roomAvailability = _roomRepository.GetRoomAvailability(roomId);
		if (roomAvailability == false)
		{
			return "Room not available!";
		}

		return "Room available!";
	}
}
