using System.Collections;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TowerBehaviorScript : MonoBehaviour
{
    [SerializeField] TowerScriptableObject _tower;
    private float _damage;
    private float _attackSpeed;
    private Sprite _sprite;
    private float _range;

    private List<EnemyBehaviorScript> _activeEnemies = new List<EnemyBehaviorScript>();

    public EnemyBehaviorScript targetEnemy;



    private void Start()
    {
        // _name = _tower.towerName;
        _damage = _tower.damage;
        _sprite = _tower.sprite;
        _attackSpeed = _tower.attackSpeed * 0.3f;
        _range = _tower.range;
        gameObject.GetComponent<SpriteRenderer>().sprite = _sprite;
        this.transform.localScale = Vector3.one * 0.6f;

        StartCoroutine(ShootEnemy());
    }

    /// <summary>
    /// Gets the closest active enemy by checking looping through active enemies list in LevelManager
    /// </summary>
    /// <returns></returns>
    private EnemyBehaviorScript GetClosestEnemy()
    {
        _activeEnemies = LevelManager.instance.GetActiveEnemyList();
        targetEnemy = null;
        for (int i = 0; i < _activeEnemies.Count; i++)
        {
            EnemyBehaviorScript activeEnemy = _activeEnemies[i]; 
            if (targetEnemy == null)
            {
                targetEnemy = activeEnemy;
            }
            else if (Vector3.Distance(activeEnemy.transform.position, targetEnemy.transform.position) < Vector3.Distance(targetEnemy.transform.position, transform.position))
            {
                targetEnemy = activeEnemy;
            }

        }
        return targetEnemy;
    }
    /// <summary>
    /// Shoots at closest enemy periodically
    /// </summary>
    /// <returns></returns>
    IEnumerator ShootEnemy()
    {
        float distance;
        while (true)
        {
            
            Vector3 towerPosition = this.gameObject.transform.position;
            targetEnemy = GetClosestEnemy();
            if (targetEnemy == null) { yield return null; }
            try 
            {
                Vector3 targetPosition = targetEnemy.transform.position;
                distance = Vector3.Distance(targetPosition, towerPosition);
                if (distance <= _range)
                {
                    Debug.Log("Enemy shot " + distance + " ");
                    transform.DOPunchScale(Vector3.one * 0.1f, 0.2f, 0, 0);
                    targetEnemy.TakeHit(_damage);
                }
            } catch(NullReferenceException e) {  }
            finally { }

            yield return new WaitForSeconds(_attackSpeed);

        }
    }
}
