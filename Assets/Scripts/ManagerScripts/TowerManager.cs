using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour, IDataPersistence
{
    public static TowerManager instance;

    [SerializeField] TowerBehaviorScript _towerPrefab;
    [SerializeField] TextMeshProUGUI _generateButton;
    List<Vector2Int> pathList;
    List<Vector3> _towerAreas = new List<Vector3>();
    List<Vector3> _usedAreas;

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }

    void Start()
    {
        _usedAreas = new List<Vector3>();
        pathList = PathManager.instance.GetPathList();
        GenerateAreas();
    }

    public void LoadData(GameData gameData)
    {
        this._usedAreas = gameData.usedTowerAreas;
        foreach (Vector3 usedArea in _usedAreas)
        {
            TowerBehaviorScript exTower = Instantiate(_towerPrefab);
            exTower.transform.position = usedArea;
            _towerAreas.Remove(usedArea);
        }
    }

    public void SaveData(GameData gameData)
    {
        gameData.usedTowerAreas = this._usedAreas;
    }

    void GenerateAreas()
    {
        Vector3 towerArea;
        List<Vector2Int> neighbours;
        List<int> limits = TerrainManager.instance.GetRowAndCol();
        foreach (Vector2Int waypoint in pathList)
        {
            neighbours = GetNeighbours(waypoint);
            foreach (Vector2Int neighbour in neighbours)
            {
                if (!pathList.Contains(neighbour) && neighbour.x >= 0 && limits[0] > neighbour.x && neighbour.y >= 0 && limits[1] > neighbour.y )
                {
                    towerArea = new Vector3(neighbour.x, neighbour.y, -1);
                    _towerAreas.Add(towerArea);
                    Debug.Log(towerArea);
                }    

            }
        }
    }

    List<Vector2Int> GetNeighbours(Vector2Int coordinate)
    {
        List<Vector2Int> neighbourList = new List<Vector2Int>();
        neighbourList.Add(coordinate + Vector2Int.up);
        neighbourList.Add(coordinate + Vector2Int.down);
        neighbourList.Add(coordinate + Vector2Int.right);
        neighbourList.Add(coordinate + Vector2Int.left);
        return neighbourList;
    }

    public void GenerateTower()
    {
        if(_towerAreas.Count <= 0)
        {
            Debug.Log("No more place to place tower!");
        }
        else
        {
            Vector3 newTowerPosition = _towerAreas[Random.Range(0, _towerAreas.Count - 1)];
            _towerAreas.Remove(newTowerPosition);
            _usedAreas.Add(newTowerPosition);
            TowerBehaviorScript newTower = Instantiate(_towerPrefab);
            newTower.transform.position = newTowerPosition;
        }

    }    
}
