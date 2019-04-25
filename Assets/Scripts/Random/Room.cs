using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public bool leftDoor, rightDoor, upDoor, downDoor;  // Si las puertas conectan con algo: true
    public Vector2Int position;
    public int distanceFromStart
    {
        get
        {
            return Mathf.Abs(position.x) + Mathf.Abs(position.y);
        }
    }

    public Room(Vector2Int pos)
    {
        leftDoor = rightDoor = upDoor = downDoor = false;
        position = pos;
    }

    public Room(int x, int y)
    {
        leftDoor = rightDoor = upDoor = downDoor = false;
        position = new Vector2Int(x, y);
    }

    /// <summary>
    /// Dos habitaciones son iguales si están en la misma posición
    /// </summary>
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Room room = obj as Room;
        if (room == null) return false;
        else return Equals(room);
    }

    public bool Equals(Room room)
    {
        if (room == null) return false;
        else return room.position.x == position.x && room.position.y == position.y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(Room room1, Room room2)
    {
        if (ReferenceEquals(room1, null) && ReferenceEquals(room2, null))
        {
            return true;
        }
        if (ReferenceEquals(room1, null) || ReferenceEquals(room2, null))
        {
            return false;
        }
        return ReferenceEquals(room1, room2) || (room1.position.x == room2.position.x && room1.position.y == room2.position.y);
    }

    public static bool operator !=(Room room1, Room room2)
    {
        return !(room1 == room2);
    }

    public override string ToString()
    {
        return "Position: " + position.ToString() + " Doors: u" + upDoor + " d" + downDoor + " l" + leftDoor + " r" + rightDoor;
    }
}
