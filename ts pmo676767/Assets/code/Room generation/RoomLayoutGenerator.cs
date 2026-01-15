using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int r = Random.Range(i, list.Count);
            (list[i], list[r]) = (list[r], list[i]);
        }
    }
}
public class Doorsinworld
{
    public Door Door;
    public Vector2Int doorpos;
}
public class RoomLayoutGenerator : MonoBehaviour
{
    public RoomData startRoom;
    public List<RoomData> roomPool;
    public int maxRooms = 10;

    public RoomRenderer roomRenderer;
    private List<Doorsinworld> doorsopen;

    private int placedroomcount = 0;
    private int safety = 0;
    private List<RoomData> originalRoomPool;
    private void Start()
    {
        Generate();
    }
    [ContextMenu("Generate layout")]
    public void Generate()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy"); foreach (GameObject obj in targets) { Destroy(obj); }
        GameObject[] target = GameObject.FindGameObjectsWithTag("Photographable"); foreach (GameObject obj in targets) { Destroy(obj); }


        foreach (Enemy enemy in EnemyRegestry.allEnemys)
        {
            Destroy(enemy);
        }
        EnemyRegestry.allEnemys.Clear();
        foreach (Body body in BodyRegistry.allBodies)
        {
            Destroy(body);
        }

        originalRoomPool = new List<RoomData>(roomPool);
        
        placedroomcount = 0;
        safety = 0;
        doorsopen = new List<Doorsinworld>();
        Vector2Int currentotherdoorrotpos = new Vector2Int(0, 0);

        bool succes = false;
        RoomInstance starroom = new RoomInstance
        {
            room = startRoom,
            roomGridPosition = new Vector2Int(0, 0),
            rotation = 0
        };
        roomRenderer.DrawRoom(starroom);
        doorsopen.Add(new Doorsinworld { Door = starroom.room.doors[0], doorpos = starroom.room.doors[0].position });
        while (placedroomcount < maxRooms && safety < 1000)
        {
            if (doorsopen.Count == 0)
            {
                    Restart();
                    return;
            }
            
            Doorsinworld currentdoor = doorsopen[UnityEngine.Random.Range(0, doorsopen.Count)];
            List<RoomData> shuffled = new List<RoomData>(roomPool);
            shuffled.Shuffle();

            foreach (var room in shuffled)
            {
                Door currentotherdoor = room.doors[Random.Range(0, room.doors.Count)];
                for (int rot = 0; rot < 360; rot += 90)
                {
                    if (DoorUtils.Rotate(currentdoor.Door.direction, 180) == DoorUtils.Rotate(currentotherdoor.direction, rot))
                    {
                        Debug.Log(DoorUtils.Rotate(currentdoor.Door.direction, 0));
                        Debug.Log(DoorUtils.Rotate(currentotherdoor.direction, rot));
                        Debug.Log(rot);

                        currentotherdoorrotpos = RoomMath.RotateTile(
                        currentotherdoor.position,
                        room.width,
                        room.height,
                        rot
                        );
                        DoorDirection currentotherdoordir = DoorUtils.Rotate(currentotherdoor.direction, rot);

                        RoomInstance roomtodraw = new RoomInstance
                        {
                            room = room,
                            roomGridPosition = (currentdoor.doorpos - currentotherdoorrotpos) + new Vector2Int(0, 0),
                            rotation = rot
                        };
                        if (roomRenderer.DrawRoom(roomtodraw))
                        {
                            placedroomcount++;
                            foreach (var door in room.doors)
                            {
                                if (door != currentotherdoor)
                                {
                                    DoorDirection waitforregdoordir = (DoorUtils.Rotate(door.direction, rot));
                                    Vector2Int waitforregdoorpos = RoomMath.RotateTile(
                                    door.position,
                                    room.width,
                                    room.height,
                                    rot
                                    );
                                    Door converteddoor = new Door { direction = waitforregdoordir, position = (currentdoor.doorpos - currentotherdoorrotpos) + waitforregdoorpos };
                                    Debug.Log(converteddoor.direction);
                                    Debug.Log(converteddoor.position);
                                    doorsopen.Add(new Doorsinworld { Door = converteddoor, doorpos = converteddoor.position });
                                }
                            }
                            doorsopen.Remove(currentdoor);
                            roomPool.Remove(room);
                            succes = true;
                            break;
                        }
                        else
                        {
                            succes = false;
                            break;
                        }
                    }
                }
                if (succes == true)
                    break;
            }
            Debug.Log(succes);
            safety++;
            if (succes == false)
                roomRenderer.Closedoor(currentdoor);
        }
        if (placedroomcount < 15)
        {
            Restart();
            return;
        }
        foreach (Doorsinworld door in doorsopen) 
        {
            roomRenderer.Closedoor(door);
        }
        Invoke("cleanup", 0.01f);
    }
            
          

    private void Restart()
    {
        foreach (Enemy enemy in EnemyRegestry.allEnemys)
        {
            Destroy(enemy);
        }
        EnemyRegestry.allEnemys.Clear();
        foreach (Body body in BodyRegistry.allBodies)
        {
            Destroy(body);
        }
        BodyRegistry.allBodies.Clear();
        if (roomRenderer.floorTilemap != null)
            roomRenderer.floorTilemap.ClearAllTiles();
        if (roomRenderer.wallTilemap != null)
            roomRenderer.wallTilemap.ClearAllTiles();

            roomPool = new List<RoomData>(originalRoomPool);


        Generate();
    }
    public void cleanup()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Photographable");
        List<GameObject> inlist = new List<GameObject>();
        foreach (Body b in BodyRegistry.allBodies)
        {
            inlist.Add(b.gameObject);
        }
        foreach (GameObject obj in target)
        {
                if (!inlist.Contains(obj))
                {
                    Destroy(obj);
                }
        }
        
    }
}
