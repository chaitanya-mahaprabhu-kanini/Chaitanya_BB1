using Chaitanya_BB1.Models;
using System.Collections.Generic;
using System.Linq;

public class RoomRepository : IRoomsRepository
{
	private readonly HotelRoomDbContext _context;

	public RoomRepository(HotelRoomDbContext context)
	{
		_context = context;
	}

	public IEnumerable<Room> GetRooms()
	{
		return _context.Rooms.ToList();
	}

	public Room GetRoomById(int id)
	{
		return _context.Rooms.Find(id);
	}

	public void AddRoom(Room room)
	{
		_context.Rooms.Add(room);
		_context.SaveChanges();
	}

	public void UpdateRoom(Room room)
	{
		_context.Rooms.Update(room);
		_context.SaveChanges();
	}

	public void DeleteRoom(int id)
	{
		var room = _context.Rooms.Find(id);
		if (room != null)
		{
			_context.Rooms.Remove(room);
			_context.SaveChanges();
		}
	}

	//Linq implementation to count of rooms under a hotel.
	public int CountRoomsByHotel(int hotelId)
	{
		return _context.Rooms.Count(room => room.Hid == hotelId);
	}

	//Linq implementation to get the availability of room based on its Rid.
	public bool GetRoomAvailability(int roomId)
	{
		var room = _context.Rooms.FirstOrDefault(r => r.Rid == roomId);
		if (room != null)
		{
			return room.Available != 1;
		}

		return false;
	}
}
