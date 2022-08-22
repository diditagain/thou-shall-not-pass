using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour, IDataPersistence
{
    public static LevelManager instance;

    public int level = 1;
    Dictionary<int, List<EnemyBehaviorScript>> monsterPools;
    public List<EnemyBehaviorScript> _enemyList = new List<EnemyBehaviorScript>();
    public int enemyNumber;
    float creationDelay;
    public int killedMonsterCount;
    public int totalMonstersKilledcount;
    

    #region UI Elements
    [SerializeField] TextMeshProUGUI _waveText;
    [SerializeField] TextMeshProUGUI _totalEnemiesKilledText;
    [SerializeField] GameObject _generatetowerButton;
    [SerializeField] GameObject _mainMenuCanvas;
    [SerializeField] GameObject _gamePlayCanvas;
    [SerializeField] GameObject _map;
    #endregion
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void LoadData(GameData gameData)
    {
        this.level = gameData.level;
        this.totalMonstersKilledcount = gameData.totalMonstersKilled;
    }

    public void SaveData(GameData gameData)
    {
        gameData.level = this.level;
        gameData.totalMonstersKilled = this.totalMonstersKilledcount;
    }

    public void StartLevel()
    {
        _map.SetActive(true);
        _mainMenuCanvas.SetActive(false);
        _gamePlayCanvas.SetActive(true);
        killedMonsterCount = 0;
        enemyNumber = 0;
        _waveText.text = "Wave " + level.ToString();
        enemyNumber = level + 3;
        creationDelay = 2f;
        StartCoroutine(ActivateEnemies());
        
    }
    private IEnumerator ActivateEnemies()
    {
        monsterPools = EnemyPool.instance._monstersList;

        for (int i = 0; i < enemyNumber; i++)
        {
            List<EnemyBehaviorScript> randomPool = monsterPools[Random.Range(0, monsterPools.Count)];
            EnemyBehaviorScript nextEnemy = randomPool[0];
            nextEnemy.SetEnemy();
            _enemyList.Add(nextEnemy);
            yield return new WaitForSeconds(creationDelay);
        }
    }

    public void LevelFailed()
    {
        _generatetowerButton.SetActive(false);
        _map.SetActive(false);
        _mainMenuCanvas.SetActive(true);
        DisactivateEnemies();
    }

    public void LevelCompleted()
    {
        DisactivateEnemies();
        level++;
        StartLevel();
        Debug.Log("Level: " + level);
    }    
    private void DisactivateEnemies()
    {
        EnemyBehaviorScript enemy;
        for (int i = 0; i < _enemyList.Count; i++)
        {
            enemy = _enemyList[i];
            enemy.ResetEnemy();
            Debug.Log("Enemy Reset");
        }
        _enemyList.Clear();
    }

    /// <summary>
    /// Getter for Active enemies in scene
    /// </summary>
    /// <returns>List<EnemyBehaviorScript></returns>
    public List<EnemyBehaviorScript> GetActiveEnemyList()
    {
        return _enemyList;
    }

    public void MonsterKilled()
    {
        killedMonsterCount++;
        totalMonstersKilledcount++;
        _totalEnemiesKilledText.text = "Total Monsters Killed: " + totalMonstersKilledcount.ToString();
        if (killedMonsterCount >= enemyNumber)
        {
            LevelCompleted();
        }
    }    


    
}
