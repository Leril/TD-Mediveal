using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.gray;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, .5f, 0f);
    
    private TextMeshPro _label;
    private Vector2Int _coordinates;
    private GridManager _gridManager;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _label.enabled = false;

        _gridManager = FindObjectOfType<GridManager>();
        
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            _label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }

    void DisplayCoordinates()
    {
        if(_gridManager == null)
            return;
        
        var parentPos = transform.parent.position;
        _coordinates.x = Mathf.RoundToInt(parentPos.x / _gridManager.UnityGridSize);
        _coordinates.y = Mathf.RoundToInt(parentPos.z / _gridManager.UnityGridSize);
        
        _label.text = _coordinates.x + "," + _coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }

    void SetLabelColor()
    {
        if(_gridManager == null)
            return;

        Node node = _gridManager.getNode(_coordinates);
        
        if(node == null)
            return;
        
        if (!node.IsWalkable)
        {
            _label.color = blockedColor;
        }
        else if (node.IsPath)
        {
            _label.color = pathColor;
        }
        else if (node.IsExplored)
        {
            _label.color = exploredColor;
        }
        else
        {
            _label.color = defaultColor;
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
