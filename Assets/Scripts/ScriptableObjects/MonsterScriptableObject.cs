using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterScriptableObject", menuName = "ScriptableObjects/MonsterScriptableObject")]
public class MonsterScriptableObject : ScriptableObject
{
    [Header("Monster Attributes")]
    public string m_Name;
    public float m_Speed;
    public float m_Armour;
    public float m_Health = 100;

    [Header("Monster Visual Elements")]
    public Transform m_Prefab;
    public Animation m_Animation;   


}
