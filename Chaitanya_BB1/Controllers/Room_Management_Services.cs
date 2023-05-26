using Chaitanya_BB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/Room_Management_Services")]
[ApiController]
public class Room_Management_Services : ControllerBase
{
	private readonly IRoomsRepository _roomRepository;

	public Room_Management_Services(IRoomsRepository roomRepository)
	{
		_roomRepository = roomRepository;
	}

	[HttpGet("Get_Rooms")]
	public IEnumerable<Room> GetRooms()
	{
		return _roomRepository.GetRooms();
	}

	
	[HttpGet("Get_Rooms_By_ID")]
	public ActionResult<Room> GetRoom(int id)
	{
		var room = _roomRepository.GetRoomById(id);
		if (room == null)
		{
			return NotFound();
		}
		return room;
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

	//Authorization needed to "Add room"
	[Authorize]
	[HttpPost("Add_A_Room")]
	public ActionResult<Room> AddRoom(Room room)
	{
		_roomRepository.AddRoom(room);
		return CreatedAtAction(nameof(GetRoom), new { id = room.Rid }, room);
	}

	[HttpPut("Update_Room_By_ID")]
	public IActionResult UpdateRoom(int id, Room room)
	{
		if (id != room.Rid)
		{
			return BadRequest();
		}

		var existingRoom = _roomRepository.GetRoomById(id);
		if (existingRoom == null)
		{
			return NotFound();
		}

		existingRoom.Available = room.Available;
		existingRoom.Hid = room.Hid;

		_roomRepository.UpdateRoom(existingRoom);

		return NoContent();
	}

	//Authorization needed to "Delete a room"
	[Authorize]
	[HttpDelete("Delete_Room_By_ID")]
	public IActionResult DeleteRoom(int id)
	{
		var room = _roomRepository.GetRoomById(id);
		if (room == null)
		{
			return NotFound();
		}

		_roomRepository.DeleteRoom(id);

		return NoContent();
	}
}
