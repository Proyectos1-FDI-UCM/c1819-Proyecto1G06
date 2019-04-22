using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomPlacer : MonoBehaviour
{
    public GameObject startingRoom;
    public GameObject[] possibleRooms;

    enum Door { North, South, East, West}

    public void PlaceRooms(List<Room> map, Vector2Int offset)
    {
        PlaceRoom(map[0], startingRoom, offset);
        for(int i = 1; i < map.Count; i++)
        {
            PlaceRoom(map[i], possibleRooms[Random.Range(0, possibleRooms.Length)], offset);
        }
    }

    void PlaceRoom(Room room, GameObject go, Vector2Int offset)
    {
        RoomManager instance = Instantiate<GameObject>(go, (Vector2)room.position * 30f, Quaternion.identity).GetComponent<RoomManager>();
        if (!room.upDoor) GetDoor(Door.North, instance.doors.transform).parent = instance.transform;
        if (!room.downDoor) GetDoor(Door.South, instance.doors.transform).parent = instance.transform;
        if (!room.leftDoor) GetDoor(Door.West, instance.doors.transform).parent = instance.transform;
        if (!room.rightDoor) GetDoor(Door.East, instance.doors.transform).parent = instance.transform;
        instance.pos = room.position + offset;
    }

    Transform GetDoor(Door door, Transform doors)
    {
        int i = 0;
        switch (door)
        {
            case Door.North:
                while (i < doors.childCount && doors.GetChild(i).position.y <= doors.position.y)
                {
                    i++;
                }
                return doors.GetChild(i);
            case Door.South:
                while (i < doors.childCount && doors.GetChild(i).position.y >= doors.position.y)
                {
                    i++;
                }
                return doors.GetChild(i);
            case Door.West:
                while (i < doors.childCount && doors.GetChild(i).position.x >= doors.position.x)
                {
                    i++;
                }
                return doors.GetChild(i);
            case Door.East:
                while (i < doors.childCount && doors.GetChild(i).position.x <= doors.position.x)
                {
                    i++;
                }
                return doors.GetChild(i);
            default:
                return null;
        }
    }
}
