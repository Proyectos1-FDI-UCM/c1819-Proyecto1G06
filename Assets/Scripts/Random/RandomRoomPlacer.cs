using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomPlacer : MonoBehaviour
{
    public GameObject startingRoom;
    public GameObject[] possibleRooms;
    public GameObject roomBoss;
    public Sprite itemPositionSprite;

    RandomItemPlacer randomItemPlacer;

    enum Door { North, South, East, West}

    private void Awake()
    {
        randomItemPlacer = GetComponent<RandomItemPlacer>();
    }

    public void PlaceRooms(List<Room> map, Vector2Int offset, List<Vector2Int> items, List<Vector2Int> health)
    {
        RoomManager[] rooms = new RoomManager[map.Count];

        rooms[0] = PlaceRoom(map[0], startingRoom, offset, items, health);
        for(int i = 1; i < rooms.Length - 1; i++)
        {
            rooms[i] = PlaceRoom(map[i], possibleRooms[Random.Range(0, possibleRooms.Length)], offset, items, health);
        }
        rooms[rooms.Length - 1] = PlaceRoom(map[map.Count - 1], roomBoss, offset, items, health);

        foreach(Vector2Int itemPos in items)
        {
            int i = 0;
            while(i < rooms.Length && rooms[i].pos != itemPos + offset)
            {
                i++;
            }
            if (i < rooms.Length)
            {
                print(i);
                rooms[i].itemPos.AddComponent<SpriteRenderer>().sprite = itemPositionSprite;
                randomItemPlacer.PlaceItem(rooms[i].itemPos.transform);
            }
        }

        foreach(Vector2Int healthPos in health)
        {
            int i = 0;
            while (i < rooms.Length && rooms[i].pos != healthPos + offset)
            {
                i++;
            }
            if (i < rooms.Length)
            {
                print(i);
                randomItemPlacer.PlaceHealth(rooms[i].itemPos.transform);
            }
        }
    }

    RoomManager PlaceRoom(Room room, GameObject go, Vector2Int offset, List<Vector2Int> items, List<Vector2Int> health)
    {
        RoomManager instance = Instantiate<GameObject>(go, (Vector2)room.position * 30f, Quaternion.identity).GetComponent<RoomManager>();
        Transform door;
        if (!room.upDoor && (door = GetDoor(Door.North, instance.doors.transform))) door.parent = instance.transform;
        if (!room.downDoor && (door = GetDoor(Door.South, instance.doors.transform))) door.parent = instance.transform;
        if (!room.leftDoor && (door = GetDoor(Door.West, instance.doors.transform))) door.parent = instance.transform;
        if (!room.rightDoor && (door = GetDoor(Door.East, instance.doors.transform))) door.parent = instance.transform;
        instance.pos = room.position + offset;

        return instance;
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
