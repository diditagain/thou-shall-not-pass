using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainManager : MonoBehaviour
{
    public static TerrainManager instance;
    [SerializeField] GameObject _map;
    [SerializeField] TerrainScript _terrainTile;
    [SerializeField] int rowNumber, colNumber;
    private List<int> _rowAndColNumbers = new List<int>();
    public TerrainScript[,] terrainArray;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        terrainArray = new TerrainScript[rowNumber, colNumber];
        for (int row = 0; row < rowNumber; row++)
        {
            for(int col = 0; col < colNumber; col++)
            {
                terrainArray[row, col] = Instantiate(_terrainTile);
                terrainArray[row, col].transform.position = new Vector3(col, row, 0);
                terrainArray[row, col].transform.SetParent(_map.transform);
            }
        }
        
    }

    private void Start()
    {
        SetRoadColor();
    }

    public List<int> GetRowAndCol()
    {
       _rowAndColNumbers.Add(colNumber);
       _rowAndColNumbers.Add(rowNumber);
        return _rowAndColNumbers;
    }

    private void SetRoadColor()
    {
        List<Vector2Int> pathList = PathManager.instance.GetPathList();
        Vector2Int terrainPosition;
        foreach (TerrainScript terrainScript in terrainArray)
        {
            int positionX = Mathf.RoundToInt(terrainScript.transform.position.x);
            int positionY = Mathf.RoundToInt(terrainScript.transform.position.y);
            terrainPosition = new Vector2Int(positionX, positionY);
            if (pathList.Contains(terrainPosition))
            {
                terrainScript.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            
        }
    }
}
