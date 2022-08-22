using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGeneratorScriptable", menuName = "ScriptableObjects/EnemyGeneratorScriptableObject")]
public class EnemyGeneratorScriptableObject : SingletonScriptableObjects<EnemyGeneratorScriptableObject>
{
    [Header("Monster Types")]
    public List<MonsterScriptableObject> monsters;
    
}
