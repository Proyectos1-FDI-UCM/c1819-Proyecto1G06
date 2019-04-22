using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelAlgorithm : MonoBehaviour
{
    [Range(2, 200)]public int numberOfRooms = 0;
    [Range(0f, 1f)]public float baseChanceToClose = 0.15f;
    [Range(0f, 1f)]public float distanceModifierToClose = 0.10f;

    List<Room> rooms;

    List<Vector2Int> availablePositions;

    private void Awake()
    {
        CreateMap();
        Vector2Int mapSize = GetMapSize();
        Minimap.instance.InitializeMap(mapSize);
        Vector2Int minimapOffset = MinimapOffset();
        foreach(Room room in rooms)
        {
            // Hay que hacer un offset para que las habitaciones quepan en el minimapa
            Minimap.instance.StoreRoom(room.position + minimapOffset, false);
        }
        Minimap.instance.UpdateMapUI();
        GetComponent<RandomRoomPlacer>().PlaceRooms(rooms, minimapOffset);
    }

    /// <summary>
    /// Crea un mapa con las propiedades de la instancia de la clase
    /// </summary>
    public void CreateMap()
    {
        availablePositions = new List<Vector2Int>();
        InsertBaseRooms();
        int i = 2;
        while(i < numberOfRooms)
        {
            if (InsertRoom()) i++;
        }
    }

    /// <summary>
    /// Devuelve el tamaño del mapa creado
    /// </summary>
    public Vector2Int GetMapSize()
    {
        if (rooms.Count == 0) return new Vector2Int(-1, -1);
        int maxX = 0, maxY = 0, minX = 0, minY = 0;
        foreach (Room room in rooms)
        {
            if (room.position.x > maxX) maxX = room.position.x;
            else if (room.position.x < minX) minX = room.position.x;
            if (room.position.y > maxY) maxY = room.position.y;
            else if (room.position.y < minY) minY = room.position.y;
        }

        return new Vector2Int(Mathf.Abs(minX) + maxX + 1, Mathf.Abs(minY) + maxY + 1);
    }

    /// <summary>
    /// Offset para que en el minimapa no hayan posiciones negativas
    /// </summary>
    Vector2Int MinimapOffset()
    {
        if (rooms.Count == 0) return new Vector2Int(0, 0);
        int minX = 0, minY = 0;

        foreach (Room room in rooms)
        {
            if (room.position.x < minX) minX = room.position.x;
            if (room.position.y < minY) minY = room.position.y;
        }

        return new Vector2Int(Mathf.Abs(minX), Mathf.Abs(minY));
    }

    /// <summary>
    /// Coloca la sala inicial del nivel y la siguiente, a su derecha
    /// </summary>
    void InsertBaseRooms()
    {
        rooms = new List<Room>();
        Room startingRoom = new Room(0, 0);
        Room nextRoom = new Room(1, 0);
        ConnectRooms(ref startingRoom, ref nextRoom);
        rooms.Add(startingRoom);
        rooms.Add(nextRoom);
        AddAdjacentPositions(nextRoom.position);
    }

    /// <summary>
    /// Conecta dos habitaciones entre sí si son adyacentes
    /// </summary>
    /// <returns>true si ha podido, false si no</returns>
    bool ConnectRooms(ref Room room1, ref Room room2)
    {
        if (room1 == null || room2 == null) return false;

        if (room1.position.x + 1 == room2.position.x && room1.position.y == room2.position.y)   //room2 a la derecha de room1
        {
            room1.rightDoor = true;
            room2.leftDoor = true;
        } else if(room1.position.x - 1 == room2.position.x && room1.position.y == room2.position.y) //room2 a la izquierda de room1
        {
            room1.leftDoor = true;
            room2.rightDoor = true;
        } else if(room1.position.x == room2.position.x && room1.position.y + 1 == room2.position.y) //room2 encima de room1
        {
            room1.upDoor = true;
            room2.downDoor = true;
        } else if(room1.position.x == room2.position.x && room1.position.y - 1 == room2.position.y) //room2 debajo de room1
        {
            room1.downDoor = true;
            room2.upDoor = true;
        } else  // No adyacentes
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Conecta la habitación room con todas sus adyacentes de rooms
    /// </summary>
    void ConnectRoomToAdjacent(ref Room room)
    {
        Vector2Int roomPos = room.position;
        Room adjacent = rooms.Find(x => x.position.x - 1 == roomPos.x && x.position.y == roomPos.y);
        if (adjacent != null)
        {
            ConnectRooms(ref adjacent, ref room);
        }
        adjacent = rooms.Find(x => x.position.x + 1 == roomPos.x && x.position.y == roomPos.y);
        if (adjacent != null)
        {
            ConnectRooms(ref adjacent, ref room);
        }
        adjacent = rooms.Find(x => x.position.x == roomPos.x && x.position.y - 1 == roomPos.y);
        if (adjacent != null)
        {
            ConnectRooms(ref adjacent, ref room);
        }
        adjacent = rooms.Find(x => x.position.x == roomPos.x && x.position.y + 1 == roomPos.y);
        if (adjacent != null)
        {
            ConnectRooms(ref adjacent, ref room);
        }
    }

    /// <summary>
    /// Comprueba si la posición pos es válida (no tiene más de una habitación adyacente o es una habitación ya)
    /// </summary>
    bool CheckIfValidPosition(Vector2Int pos)
    {
        if(rooms.Contains(new Room(pos.x, pos.y))) return false;

        int adjacentRooms = 0;
        if(rooms.Contains(new Room(pos.x + 1, pos.y)))
        {
            adjacentRooms++;
        }
        if(rooms.Contains(new Room(pos.x - 1, pos.y)))
        {
            adjacentRooms++;
        }
        if(rooms.Contains(new Room(pos.x, pos.y + 1)))
        {
            adjacentRooms++;
        }
        if(rooms.Contains(new Room(pos.x, pos.y - 1)))
        {
            adjacentRooms++;
        }

        return adjacentRooms <= 1;
    }

    /// <summary>
    /// Añade las posiciones adyacentes a from si son válidas, a availablePositions
    /// </summary>
    void AddAdjacentPositions(Vector2Int from)
    {
        Vector2Int adjacent = new Vector2Int(from.x + 1, from.y);
        if (CheckIfValidPosition(adjacent)) availablePositions.Add(adjacent);
        adjacent = new Vector2Int(from.x - 1, from.y);
        if(CheckIfValidPosition(adjacent)) availablePositions.Add(adjacent);
        adjacent = new Vector2Int(from.x, from.y + 1);
        if(CheckIfValidPosition(adjacent)) availablePositions.Add(adjacent);
        adjacent = new Vector2Int(from.x, from.y - 1);
        if(CheckIfValidPosition(adjacent)) availablePositions.Add(adjacent);
    }

    /// <summary>
    /// Cierra el camino pos según la probabilidad de qeuse cierre
    /// </summary>
    /// <returns>true si se cierra, false si no</returns>
    bool ClosePath(Vector2Int pos)
    {
        float rng = Random.Range(0f, 1f);
        return rng < baseChanceToClose + (pos.x + pos.y) * distanceModifierToClose;
    }

    /// <summary>
    /// Abre los caminos adyacentes de una habitación de rooms
    /// </summary>
    /// <returns>true si ha abierto al menos 1, false si no</returns>
    bool OpenRandomPath()
    {
        Vector2Int pos = rooms[Random.Range(0, rooms.Count)].position;
        int openPaths = availablePositions.Count;
        AddAdjacentPositions(pos);
        return availablePositions.Count > openPaths;
    }

    /// <summary>
    /// Crea una habitación en la primera posición libre de availablePositions si puede
    /// </summary>
    /// <returns>true si la crea, false si no</returns>
    bool InsertRoom()
    {
        bool insertedRoom = false;
        Vector2Int pos;

        if (availablePositions.Count <= 0)
        {
            bool pathOpened = false;
            do
            {
                pathOpened = OpenRandomPath();
            } while (!pathOpened);
        }

        pos = availablePositions[0];
        // Solo cierra un camino si el número de posibilidades para avanzar es mayor de 1
        if (CheckIfValidPosition(pos) && (availablePositions.Count < 2 || !ClosePath(pos)))
        {
            Room room = new Room(pos);
            ConnectRoomToAdjacent(ref room);
            rooms.Add(room);
            AddAdjacentPositions(pos);
            insertedRoom = true;
        }

        availablePositions.RemoveAt(0); // La posición se ha comprobado ya, se elimina
        return insertedRoom;
    }
}
