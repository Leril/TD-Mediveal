using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    public Vector2Int StartCoordinates => startCoordinates;

    [SerializeField] private Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates => destinationCoordinates;

    private Node _startNode;
    private Node _destinationNode;
    private Node _currentSearchNode;

    private readonly Vector2Int[] _directions = {Vector2Int.right, Vector2Int.left, Vector2Int.down, Vector2Int.up,};
    private GridManager _gridManager;

    private Queue<Node> _frontier = new Queue<Node>();

    private Dictionary<Vector2Int, Node> _reachedNodes = new Dictionary<Vector2Int, Node>();
    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();

        if (_gridManager != null)
        {
            _grid = _gridManager.Grid;
            _startNode = _grid[startCoordinates];
            _destinationNode = _grid[destinationCoordinates];
            _startNode.IsWalkable = true;
            _destinationNode.IsWalkable = true;
        }
    }

    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        _gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    private void ExploreNeighbours()
    {
        var neighbors = 
            (from direction 
                in _directions
                select _currentSearchNode.Coordinates + direction
                into potentialNewNeighbourCoordinates 
                where _grid.ContainsKey(potentialNewNeighbourCoordinates)
                select _grid[potentialNewNeighbourCoordinates])
            .ToList();

        foreach (var variableNeighbor in neighbors
                     .Where(variableNeighbor => !_reachedNodes.ContainsKey(variableNeighbor.Coordinates)
                                                && variableNeighbor.IsWalkable))
        {
            variableNeighbor.ConnectedTo = _currentSearchNode;
            _reachedNodes.Add(variableNeighbor.Coordinates, variableNeighbor);
            _frontier.Enqueue(variableNeighbor);
        }
    }

    void BreadthFirstSearch()
    {
        _frontier.Clear();
        _reachedNodes.Clear();
        
        bool isRunning = true;
        
        _frontier.Enqueue(_startNode);
        _reachedNodes.Add(startCoordinates, _startNode);

        while (_frontier.Count > 0 && isRunning)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.IsExplored = true;
            
            ExploreNeighbours();

            if (_currentSearchNode.Coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
        
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = _destinationNode;
        
        path.Add(currentNode);
        currentNode.IsPath = true;

        while (currentNode.ConnectedTo != null)
        {
            currentNode = currentNode.ConnectedTo;
            path.Add(currentNode);
            currentNode.IsPath = true;
        }
        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            bool previousState = _grid[coordinates].IsWalkable;
            _grid[coordinates].IsWalkable = false;
            var newPath = GetNewPath();
            _grid[coordinates].IsWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
}
