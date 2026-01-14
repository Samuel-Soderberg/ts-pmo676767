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
    private void Start()
    {
        Generate();
    }
    public void Generate()
    {
        doorsopen = new List<Doorsinworld>();
        Vector2Int calculatedpos = new Vector2Int(0, 0);
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
        //while (placedroomcount < maxRooms && safety <= 100)
        //{
        Doorsinworld currentdoor = doorsopen[UnityEngine.Random.Range(0, doorsopen.Count)];
        List<RoomData> shuffled = new List<RoomData>(roomPool);
        shuffled.Shuffle();

        //foreach (var room in shuffled)
        //{
        Door currentotherdoor = startRoom.doors[Random.Range(0, startRoom.doors.Count)];
        for (int rot = 0; rot < 360; rot += 90)
        {
            if (DoorUtils.Rotate(currentdoor.Door.direction, 180) == DoorUtils.Rotate(currentotherdoor.direction, rot))
            {
                currentotherdoorrotpos = RoomMath.RotateTile(
                currentotherdoor.position,
                startRoom.width,
                startRoom.height,
                rot
                );
                DoorDirection currentotherdoordir = DoorUtils.Rotate(currentotherdoor.direction, rot);

                if (rot == 0 || rot == 180)
                {
                    Vector2Int roomdim = new Vector2Int(startRoom.width, startRoom.height);
                    calculatedpos = currentotherdoorrotpos - roomdim;
                }
                if (rot == 90 || rot == 270)
                {
                    Vector2Int roomdim = new Vector2Int(startRoom.height, startRoom.width);
                    calculatedpos = currentotherdoorrotpos - roomdim;
                }
                Debug.Log(calculatedpos);

                RoomInstance roomtodraw = new RoomInstance
                {
                    room = startRoom,
                    roomGridPosition = calculatedpos,
                    rotation = rot
                };
                Debug.Log(roomRenderer.DrawRoom(roomtodraw));
                /* if (roomRenderer.DrawRoom(roomtodraw))
                 {
                     placedroomcount++;
                     foreach (var door in startRoom.doors)
                     {
                         DoorDirection waitforregdoordir = DoorUtils.Rotate(door.direction, rot);
                         Vector2Int waitforregdoorpos = RoomMath.RotateTile(
                         door.position,
                         room.width,
                         room.height,
                         rot
                         );

                         if (rot == 0 || rot == 180)
                         {
                             Vector2Int roomdim = new Vector2Int(room.width, room.height);
                             calculatedpos = waitforregdoorpos - roomdim;
                         }
                         if (rot == 90 || rot == 270)
                         {
                             Vector2Int roomdim = new Vector2Int(room.height, room.width);
                             calculatedpos = waitforregdoorpos - roomdim;
                         }
                         Door converteddoor = new Door { direction = waitforregdoordir, position = door.position };
                         doorsopen.Add(new Doorsinworld {Door = converteddoor, doorpos = calculatedpos });
                     }
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
                break; */
                //}
                //safety++;
                //if (succes == false)
                //roomRenderer.Closedoor(currentdoor);
                //}
            } 
        } 
    } 

    private void Restart()
    {
        
    }
}