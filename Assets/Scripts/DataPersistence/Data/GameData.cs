using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalMonstersKilled;
    public int level;
    public List<Vector3> usedTowerAreas;

    public GameData()
    {
        this.level = 0;
        this.totalMonstersKilled = 0;
        this.usedTowerAreas = new List<Vector3>();
    }
}
