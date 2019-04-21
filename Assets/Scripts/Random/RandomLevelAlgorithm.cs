using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelAlgorithm : MonoBehaviour
{
    class Room
    {
        public bool leftDoor, rightDoor, upDoor, downDoor;  // Si las puertas conectan con algo: true
        public Vector2Int position;
        public int distanceFromStart
        {
            get
            {
                return position.x + position.y;
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

        public static bool operator==(Room room1, Room room2)
        {
            if(ReferenceEquals(room1, null) || ReferenceEquals(room2, null))
            {
                return false;
            }
            return ReferenceEquals(room1, room2) || (room1.position.x == room2.position.x && room1.position.y == room2.position.y);
        }

        public static bool operator!=(Room room1, Room room2)
        {
            if (ReferenceEquals(room1, null) && ReferenceEquals(room2, null))
            {
                return false;
            }
            return !(room1 == room2);
        }

        public override string ToString()
        {
            return "Position: " + position.ToString() + " Doors: u" + upDoor + " d" + downDoor + " l" + leftDoor + " r" + rightDoor;
        }
    }

    public int numberOfRooms = 0;
    public float baseChanceToClose = 0.15f;
    public float distanceModifierToClose = 0.10f;

    List<Room> rooms;

    List<Vector2Int> availablePositions;

    private void Start()
    {
        CreateMap();
    }

    /// <summary>
    /// Crea un mapa con las propiedades de la instancia de la clase
    /// </summary>
    public void CreateMap()
    {
        availablePositions = new List<Vector2Int>();
        InsertBaseRooms();
        int i = 0;
        while(i < numberOfRooms)
        {
            if (InsertRoom()) i++;
        }
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
    /// Crea una habitación en la primera posición libre de availablePositions si puede
    /// </summary>
    /// <returns>true si la crea, false si no</returns>
    bool InsertRoom()
    {
        bool insertedRoom = false;
        Vector2Int pos = availablePositions[0];

        // Solo cierra un camino si el número de posibilidades para avanzar es mayor de 1
        if (availablePositions.Count < 2 || !ClosePath(pos))
        {
            Room room = new Room(pos);
            AddAdjacentPositions(pos);
            ConnectRoomToAdjacent(ref room);
            rooms.Add(room);
            insertedRoom = true;
        }

        availablePositions.RemoveAt(0); // La posición se ha comprobado ya, se elimina
        return insertedRoom;
    }
}
