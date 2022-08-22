using UnityEngine;

[CreateAssetMenu(fileName ="TowerData", menuName = "ScriptableObjects/TowerScriptableObject")]
public class TowerScriptableObject : ScriptableObject
{
    [Header("Tower Properties")]
    public string towerName;
    public Sprite sprite;
    public float damage;
    public float attackSpeed;
    public float range;
}
