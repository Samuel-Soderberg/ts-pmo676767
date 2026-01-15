using UnityEngine;

public static class DoorUtils
{
    public static DoorDirection Opposite(DoorDirection dir)
    {
        return dir switch
        {
            DoorDirection.North => DoorDirection.South,
            DoorDirection.South => DoorDirection.North,
            DoorDirection.East => DoorDirection.West,
            DoorDirection.West => DoorDirection.East,
            _ => dir
        };
    }
    public static DoorDirection Rotate(DoorDirection dir, int rotation)
    {
        int steps = rotation / 90;
        for (int i = 0; i < steps; i++)
        {
            dir = dir switch
            {
                DoorDirection.North => DoorDirection.West,
                DoorDirection.West => DoorDirection.South,
                DoorDirection.South => DoorDirection.East,
                DoorDirection.East => DoorDirection.North,
                _ => dir
            };
        }
        return dir;
    }
}