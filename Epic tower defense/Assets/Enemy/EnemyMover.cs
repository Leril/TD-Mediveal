using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> path;
    [SerializeField] [Range(0, 5)] private float speed = 1f;


    private Enemy _enemy;
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();
        
        var waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach (var waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            var startPosition = transform.position;
            var endPosition = waypoint.transform.position;
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
