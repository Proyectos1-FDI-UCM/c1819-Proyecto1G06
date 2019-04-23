using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomPlacer : MonoBehaviour
{
    public GameObject startingRoom;
    public GameObject[] possibleRooms;
    public GameObject roomBoss;

    enum Door { North, South, East, West}

    public void PlaceRooms(List<Room> map, Vector2Int offset)
    {
        PlaceRoom(map[0], startingRoom, offset);
        for(int i = 1; i < map.Count - 1; i++)
        {
            PlaceRoom(map[i], possibleRooms[Random.Range(0, possibleRooms.Length)], offset);
        }
        PlaceRoom(map[map.Count - 1], roomBoss, offset);
    }

    void PlaceRoom(Room room, GameObject go, Vector2Int offset)
    {
        RoomManager instance = Instantiate<GameObject>(go, (Vector2)room.position * 30f, Quaternion.identity).GetComponent<RoomManager>();
        Transform door;
        if (!room.upDoor && (door = GetDoor(Door.North, instance.doors.transform))) door.parent = instance.transform;
        if (!room.downDoor && (door = GetDoor(Door.South, instance.doors.transform))) door.parent = instance.transform;
        if (!room.leftDoor && (door = GetDoor(Door.West, instance.doors.transform))) door.parent = instance.transform;
        if (!room.rightDoor && (door = GetDoor(Door.East, instance.doors.transform))) door.parent = instance.transform;
        instance.pos = room.position + offset;
    }

    Transform GetDoor(Door door, Transform doors)
    {
        int i = 0;
        switch (door)
        {
            case Door.North:
                while (i < doors.childCount && doors.GetChild(i).position.y <= doors.position.y) i++;
                if (i < doors.childCount)
                    return doors.GetChild(i);
                else return null;
            case Door.South:
                while (i < doors.childCount && doors.GetChild(i).position.y >= doors.position.y) i++;
                if (i < doors.childCount)
                    return doors.GetChild(i);
                else return null;
            case Door.West:
                while (i < doors.childCount && doors.GetChild(i).position.x >= doors.position.x) i++;
                if (i < doors.childCount)
                    return doors.GetChild(i);
                else return null;
            case Door.East:
                while (i < doors.childCount && doors.GetChild(i).position.x <= doors.position.x) i++;
                if (i < doors.childCount)
                    return doors.GetChild(i);
                else return null;
            default:
                return null;
        }
    }
}
