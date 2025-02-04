using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up,
    down,
    foreward,
    backwards,
    left,
    right
}
public static class DirectionExtensions
{
    public static Vector3Int DirectionToVector(this Direction direction)
    {
        switch (direction)
        {
            case Direction.up:
                return Vector3Int.up;
            case Direction.down:
                return Vector3Int.down;
            case Direction.foreward:
                return Vector3Int.forward;
            case Direction.backwards:
                return Vector3Int.back;
            case Direction.left:
                return Vector3Int.left;
            case Direction.right:
                return Vector3Int.right;
            default:
                return Vector3Int.zero;
        }
    }

    public static Direction VectorToDirection(this Direction direction)
    {
        switch (direction)
        {
            case Direction.up:
                return Direction.down;
            case Direction.down:
                return Direction.up;
            case Direction.foreward:
                return Direction.backwards;
            case Direction.backwards:
                return Direction.foreward;
            case Direction.left:
                return Direction.right;
            case Direction.right:
                return Direction.left;
            default:
                return Direction.up;
        }
    }
}