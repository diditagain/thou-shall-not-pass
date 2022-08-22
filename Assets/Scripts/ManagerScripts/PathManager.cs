using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PathManager : MonoBehaviour
{

    public static PathManager instance;
    [SerializeField] GameObject _map;
    //[SerializeField] List<List<GameObject>> _tileList = new List<List<GameObject>>();
    [SerializeField] List<Vector2Int> pathList = new List<Vector2Int>();
    List<Vector3> waypoints = new List<Vector3>();
    private void Awake()
    {
        if(instance == null) { instance = this; }
        SetWaypoints();
    }

    public List<Vector2Int> GetPathList()
    {
        return pathList;
    }

    void SetWaypoints()
    {
        Vector3 waypoint;
        foreach (Vector2Int path in pathList)
        {
            waypoint = new Vector3(path.x, path.y, -1);
            waypoints.Add(waypoint);
        }
    }
    public Vector3[] GetWaypoints()
    {
        return waypoints.ToArray();
    }

}
