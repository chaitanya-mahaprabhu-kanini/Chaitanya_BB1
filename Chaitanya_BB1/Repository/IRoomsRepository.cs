using Chaitanya_BB1.Models;
using System.Collections.Generic;

public interface IRoomsRepository
{
	IEnumerable<Room> GetRooms();
	Room GetRoomById(int id);
	void AddRoom(Room room);
	void UpdateRoom(Room room);
	void DeleteRoom(int id);
}
