using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private EnemyBehaviorScript tempPrefab;
    public static EnemyPool instance;
    public Dictionary<int, List<EnemyBehaviorScript>> _monstersList = new Dictionary<int, List<EnemyBehaviorScript>>();
    public List<int> poolSize = new List<int>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CreatePool()
    {
        for (int i = 0; i < poolSize.Count; i++)
        {
            List<EnemyBehaviorScript> tempEnemyList = new List<EnemyBehaviorScript>();
            _monstersList.Add(i, tempEnemyList);
            for (int j = 0; j < poolSize[i]; j++)
            {
                EnemyBehaviorScript newEnemy = Instantiate(tempPrefab);
                newEnemy.StartEnemy(i);
                newEnemy.ResetEnemy();
            }
        }
    }
}
