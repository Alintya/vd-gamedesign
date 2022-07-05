using System;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


[Flags]
public enum RoomOpenings
{
    // 00000001
    Left = 1,
    // 00000010
    Right = 2,
    // 00000100
    Bottom = 4,
    // 00001000
    Top = 8,
}

public class ProceduralGenerator : MonoBehaviour
{
    public int RoomSize;
    public Room StartRoom;
    public NavMeshSurface Surface;
    public int MapSize;
    
    public GameObject[] LeftRooms;
    public GameObject[] RightRooms;
    public GameObject[] TopRooms;
    public GameObject[] BottomRooms;

    // BOSS SPAWNER ------------------------------------------------------------------------------------
    public List<GameObject> Rooms;

    public float WaitTime;
    public GameObject Boss;
    public GameObject NextFloor;
    private bool spawnedBoss;
    // -------------------------------------------------------------------------------------------------
    private Room[,] _map;

    // Start is called before the first frame update
    private void Start()
    {
        _map = new Room[MapSize, MapSize];
        _map[0, 0] = StartRoom;
        var generateRooms = GenerateRooms(StartRoom);
        
        for (var index = 0; index < generateRooms.Count; index++)
        {
            var room = generateRooms[index];
            generateRooms.AddRange(GenerateRooms(room.GetComponent<Room>()));
        }

        Surface.BuildNavMesh();
    }

    private IList<GameObject> GenerateRooms(Room room)
    {
        List<GameObject> spawnedRooms = new List<GameObject>();
        GameObject spawnedRoom = null;
        // Compare room flags
        // 00000001 (left)
        // &
        // 00001111 (Everything)
        // 00000001 -> Has left opening 
        if (room.openings.HasFlag(RoomOpenings.Left))
        {
            spawnedRoom = GenerateRoom(room, RoomOpenings.Left);
            spawnedRooms.Add(spawnedRoom);
        }
        if (room.openings.HasFlag(RoomOpenings.Right))
        {
            spawnedRoom = GenerateRoom(room, RoomOpenings.Right);
            spawnedRooms.Add(spawnedRoom);
        }
        if (room.openings.HasFlag(RoomOpenings.Top))
        {
            spawnedRoom = GenerateRoom(room, RoomOpenings.Top);
            spawnedRooms.Add(spawnedRoom);
        }
        if (room.openings.HasFlag(RoomOpenings.Bottom))
        {
            spawnedRoom = GenerateRoom(room, RoomOpenings.Bottom);
            spawnedRooms.Add(spawnedRoom);
        }

        return spawnedRooms.Where(e => e is not null).ToList();
    }

    private GameObject GenerateRoom(Room room, RoomOpenings opening)
    {
        //
        //  [0 0 0 1]
        //  [0 1 0 1]
        //
        //
        GameObject roomToSpawn;
        var position = room.transform.position;
        Vector3 spawnPosition;

        switch (opening)
        {
            case RoomOpenings.Left:
                roomToSpawn = LeftRooms[Random.Range(0, LeftRooms.Length)];
                spawnPosition = new Vector3(position.x - RoomSize, position.y, position.y);
                break;
            case RoomOpenings.Right:
                roomToSpawn = RightRooms[Random.Range(0, LeftRooms.Length)];
                spawnPosition = new Vector3(position.x + RoomSize, position.y, position.y);
                break;
            case RoomOpenings.Bottom:
                roomToSpawn = BottomRooms[Random.Range(0, LeftRooms.Length)];
                spawnPosition = new Vector3(position.x, position.y, position.z - RoomSize);
                break;
            case RoomOpenings.Top:
                roomToSpawn = TopRooms[Random.Range(0, LeftRooms.Length)];
                spawnPosition = new Vector3(position.x, position.y, position.z + RoomSize);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(opening), opening, null);
        }
        
        room.connections[opening] = true;

        var arrayCoordinatesZ = Math.Abs((int) spawnPosition.z / 40);
        var arrayCoordinatesX = Math.Abs((int) spawnPosition.x / 40);
        
        // TODO: binary or?
        if (MapSize < arrayCoordinatesX || _map[arrayCoordinatesZ, arrayCoordinatesX] is not null) 
        {
            return null;
        }
        
        // [start, 0]
        // [newRoom, 0] 
        
        var generatedRoom = Instantiate(roomToSpawn, spawnPosition, Quaternion.identity);
        _map[arrayCoordinatesZ, arrayCoordinatesX] = generatedRoom.GetComponent<Room>();
        
        return generatedRoom;
    }
    
    // BOSS SPAWNER ------------------------------------------------------------------------------------
    void Update()
    {
        if(WaitTime <= 0 && spawnedBoss == false)
        {
            // spawn boss
            for (int i = 0; i < Rooms.Count; i++)
            {
                if (i == Rooms.Count-1)
                {
                    Instantiate(Boss, Rooms[i].transform.position, Quaternion.identity);
                    Instantiate(NextFloor, Rooms[i].transform.position, Quaternion.identity);
                    spawnedBoss = true;
                }
            }
        }
        else
        {
            WaitTime -= Time.deltaTime;
        }
    }
    //-----------------------------------------------------------------------------------------------------
}
