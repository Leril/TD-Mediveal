using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [FormerlySerializedAs("isPlaceable")] [SerializeField] private bool isSuccessfull;
    [SerializeField] private Tower towerPrefab;

    private GridManager _gridManager;
    private Pathfinder _pathfinder;
    
    private Vector2Int _coordinates = new Vector2Int();

    public bool IsSuccessfull => isSuccessfull;


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

            if (!isSuccessfull)
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
            isSuccessfull = isPlaced;

            if (isSuccessfull)
            {
                _gridManager.BlockNode(_coordinates);
                _pathfinder.NotifyReceivers();
            }
        }
    }
}
