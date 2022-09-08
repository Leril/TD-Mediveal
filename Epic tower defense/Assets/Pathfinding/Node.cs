using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Node
{
    public Vector2Int Coordinates;
    public bool IsWalkable;
    [FormerlySerializedAs("IsExploder")] public bool IsExplored;
    public bool IsPath;
    public Node ConnectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        Coordinates = coordinates;
        IsWalkable = isWalkable;
    }
}
