using UnityEngine;
using TMPro;

public class MonsterLineManager : MonoBehaviour
{
    public MonsterLines monsterLines;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        string[] monsterLinesArray = monsterLines._monsterLinesArray;
        text.text = monsterLinesArray[Random.Range(0, monsterLinesArray.Length - 1)];
    }

}
