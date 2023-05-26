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

	[HttpGet]
	public IEnumerable<Room> GetRooms()
	{
		return _roomRepository.GetRooms();
	}

	
	[HttpGet("{id}")]
	public ActionResult<Room> GetRoom(int id)
	{
		var room = _roomRepository.GetRoomById(id);
		if (room == null)
		{
			return NotFound();
		}
		return room;
	}

	//Authorization needed to "Add room"
	[Authorize]
	[HttpPost]
	public ActionResult<Room> AddRoom(Room room)
	{
		_roomRepository.AddRoom(room);
		return CreatedAtAction(nameof(GetRoom), new { id = room.Rid }, room);
	}

	[HttpPut("{id}")]
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
	[HttpDelete("{id}")]
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

	//Linq for getting count of rooms under a hotel.
	[HttpGet("countByHotel/{hotelId}")]
	public ActionResult<int> CountRoomsByHotel(int hotelId)
	{
		int roomCount = _roomRepository.CountRoomsByHotel(hotelId);
		return roomCount;
	}
}
