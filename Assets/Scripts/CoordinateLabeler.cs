using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 Renames and labels tiles according to their coordinates. Only works in editor mode. 
Makes it easier to set a path for enemies.
 */
[ExecuteInEditMode]
public class CoordinateLabeler : MonoBehaviour
{

    TextMeshPro coordinateText;
    Vector2Int parentCoordinates;

    private void Awake()
    {
        this.gameObject.SetActive(true);
        coordinateText = GetComponent<TextMeshPro>();   
    }
    
    void Update()
    {
        parentCoordinates = GetParentCoordinates();
        SetCoordinateText(parentCoordinates);
        SetParentObjectName(parentCoordinates);
    }



    /// <summary>
    /// Gets coordinates of parent object
    /// </summary>
    /// <returns>Vector2Int</returns>
    Vector2Int GetParentCoordinates()
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.y);
        return coordinates;
    }
    /// <summary>
    /// Sets the text of TextMeshProElement
    /// </summary>
    /// <param name="coordinates"></param>
    void SetCoordinateText(Vector2Int coordinates)
    {
        coordinateText.text = coordinates.ToString();
    }
    /// <summary>
    /// Sets the name of the parent in hierarchy
    /// </summary>
    /// <param name="coordinates"></param>
    void SetParentObjectName(Vector2Int coordinates)
    {
        transform.parent.name = coordinates.ToString();
    }
}
