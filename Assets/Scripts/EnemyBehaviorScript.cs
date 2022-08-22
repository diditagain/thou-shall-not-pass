using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyBehaviorScript : MonoBehaviour
{
    EnemyGeneratorScriptableObject _enemyGenerator;
    MonsterScriptableObject _monster;

    [SerializeField] private Image _healthImage;

    #region Local Variables
    string m_Name;
    float m_Speed;
    float m_Armour;
    float m_Health;
    Vector3[] path;
    private int monsterType;
    Transform m_Prefab;
    #endregion

    bool m_Dead;


    /// <summary>
    /// Gets Monster data from MonsterScriptableObject according to monster type given as an int
    /// </summary>
    /// <param name="monsterType"></param>
    public void StartEnemy(int monsterType)
    {
        this.monsterType = monsterType;
        _enemyGenerator = EnemyGeneratorScriptableObject.Instance;
        _monster = _enemyGenerator.monsters[monsterType];
        path = PathManager.instance.GetWaypoints();
        m_Name = _monster.m_Name;
        m_Speed = _monster.m_Speed;
        m_Armour = _monster.m_Armour;
        m_Prefab = Instantiate(_monster.m_Prefab);
        m_Prefab.SetParent(this.gameObject.transform);
        m_Prefab.transform.localPosition = Vector3.zero;
        m_Prefab.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        transform.name = m_Name + EnemyPool.instance._monstersList[monsterType].Count;

    }


    /// <summary>
    /// Gets the Monster from its object pool and sets active in hierarchy
    /// </summary>
    public void SetEnemy()
    {
        _healthImage.fillAmount = 1;
        m_Dead = false;
        this.transform.position = new Vector3(path[0].x, path[0].y, 0);
        EnemyPool.instance._monstersList[monsterType].Remove(this);
        this.gameObject.SetActive(true);
        MoveEnemy();
    }

    /// <summary>
    /// Resets Monster info and adds back to object pool
    /// </summary>
    public void ResetEnemy()
    {
        DOTween.Kill(transform.name);
        this.transform.position = new Vector3(path[0].x, path[0].y, 0);
        this.gameObject.SetActive(false);
        Debug.Log("Disactivated");
        m_Health = _monster.m_Health;
        EnemyPool.instance._monstersList[monsterType].Add(this);
        //

    }

    /// <summary>
    /// public method to decrease health of the monster.
    /// Total damage taken = damage taken - (monster armour * 10)
    /// </summary>
    /// <param name="damage"></param>
    public void TakeHit(float damage)
    {
        float totalHealth = _monster.m_Health;
        float armourDamageAbsorb = m_Armour * 10;
        m_Health -= damage - armourDamageAbsorb;
        _healthImage.fillAmount = m_Health / totalHealth;
        if (m_Health <= 0) 
        {
            m_Dead = true;
            this.ResetEnemy();
            LevelManager.instance.MonsterKilled();
            LevelManager.instance._enemyList.Remove(this);
            Debug.Log("Enemy Died");
        }
        Debug.Log("Take Hit");
    }


    /// <summary>
    /// Moves enemy along the path array
    /// </summary>
    private void MoveEnemy()
    {
        float moveSpeed = m_Speed;
        transform.DOPath(path, moveSpeed * path.Length).OnComplete(() => LevelManager.instance.LevelFailed()).SetId(transform.name);
    }

}


