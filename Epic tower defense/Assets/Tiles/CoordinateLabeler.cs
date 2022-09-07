using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    void DisplayCoordinates()
    {
        var parentPos = transform.parent.position;
        _coordinates.x = Mathf.RoundToInt(parentPos.x / UnityEditor.EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(parentPos.z / UnityEditor.EditorSnapSettings.move.z);
        
        _label.text = _coordinates.x + "," + _coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }
}
