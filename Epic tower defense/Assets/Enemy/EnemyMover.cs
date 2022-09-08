using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] private float speed = 1f;

    private List<Node> _path = new List<Node>();
    private Enemy _enemy;
    
    private GridManager _gridManager;
    private Pathfinder _pathfinder;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _gridManager = FindObjectOfType<GridManager>();
        _pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        var coordinates = resetPath ? _pathfinder.StartCoordinates : _gridManager.GetCoordinateFromPosition(transform.position);
        
        StopAllCoroutines();
        
        _path.Clear();
        _path = _pathfinder.GetNewPath(coordinates);
        
        StartCoroutine(FollowPath());
    }

    private void ReturnToStart()
    {
        transform.position = _gridManager.GetPositionFromCoordinate(_pathfinder.StartCoordinates);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 1; i < _path.Count; i++)
        {
            var startPosition = transform.position;
            var endPosition = _gridManager.GetPositionFromCoordinate(_path[i].Coordinates);
            var travelPercent = 0f;

            transform.LookAt(endPosition);
            
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        ReachTheEnd();
    }

    private void ReachTheEnd()
    {
        gameObject.SetActive(false);
        _enemy.StealGold();
    }
}
