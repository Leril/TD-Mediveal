using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool isPlaceable;
    [SerializeField] private Tower towerPrefab;

    private GridManager _gridManager;
    private Pathfinder _pathfinder;
    
    private Vector2Int _coordinates = new Vector2Int();

    public bool IsPlaceable => isPlaceable;


    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        if (_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinateFromPosition(transform.position);

            if (!isPlaceable)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (_gridManager.getNode(_coordinates).IsWalkable && !_pathfinder.WillBlockPath(_coordinates))
        {
            var isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceable = !isPlaced;
            _gridManager.BlockNode(_coordinates);
        }
    }
}
