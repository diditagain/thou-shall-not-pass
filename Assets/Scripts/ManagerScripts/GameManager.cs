using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        EnemyPool.instance.CreatePool();
    }

    public void StartNewgame()
    {
        DataPersistenceManager.instance.NewGame();
        LevelManager.instance.StartLevel();

    }

    public void LoadGame()
    {
        DataPersistenceManager.instance.LoadGame();
        LevelManager.instance.StartLevel();
    }
}
