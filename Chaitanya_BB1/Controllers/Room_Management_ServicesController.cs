using Chaitanya_BB1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/Room_Management_Services")]
[ApiController]
public class Room_Management_ServicesController : ControllerBase
{
	private readonly IRoomsRepository _roomRepository;

	public Room_Management_ServicesController(IRoomsRepository roomRepository)
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

	//Authorization needed to "Add room"
	[Authorize]
	[HttpPost("Auth_Add_A_Room")]
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
