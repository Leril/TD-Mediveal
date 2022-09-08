using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    
    [Tooltip("Unity grid size - should math UnityEditor snap settings")]
    [SerializeField] private int unityGridSize = 10;

    public int UnityGridSize => unityGridSize;
    
    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    public Dictionary<Vector2Int, Node> Grid => _grid;

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                _grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }

    public void BlockNode(Vector2Int coordinate)
    {
        if (_grid.ContainsKey(coordinate))
            _grid[coordinate].IsWalkable = false;
    }

    public void ResetNodes()
    {
        foreach (var (_, value) in _grid)
        {
            value.ConnectedTo = null;
            value.IsExplored = false;
            value.IsPath = false;
        }
    }

    public Vector2Int GetCoordinateFromPosition(Vector3 position)
    {
        var _coordinates = new Vector2Int
        {
            x = Mathf.RoundToInt(position.x / unityGridSize),
            y = Mathf.RoundToInt(position.z / unityGridSize)
        };

        return _coordinates;
    }

    public Vector3 GetPositionFromCoordinate(Vector2Int coordinate)
    {
        var _position = new Vector3
        {
            x = coordinate.x * unityGridSize,
            z = coordinate.y * unityGridSize
        };

        return _position;
    }
    public Node getNode(Vector2Int coordinates)
    {
        if(_grid == null)
            print("dfuk");
        
        if (_grid.ContainsKey(coordinates))
            return _grid[coordinates];
        
        return null;
    }
}
