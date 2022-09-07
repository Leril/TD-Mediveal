using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.black;
    [SerializeField] private Color blockedColor = Color.gray;
    
    private TextMeshPro _label;
    private Vector2Int _coordinates;
    private Waypoint _waypoint;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _label.enabled = false;
        
        _waypoint = GetComponentInParent<Waypoint>();
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
        ColorCoordinates();
        ToggleLabels();
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

    void ColorCoordinates()
    {
        if (_waypoint.IsPlaceable)
        {
            _label.color = defaultColor;
        }
        else
        {
            _label.color = blockedColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.enabled;
        }
    }
}
