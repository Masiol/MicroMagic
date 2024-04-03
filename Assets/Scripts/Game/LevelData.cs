using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level/Level")]

public class LevelData : ScriptableObject
{
    [SerializeField] private int Level;
    public int level => Level;

    [SerializeField] private int MaxCommandPoints;
    public int maxCommandPoints => MaxCommandPoints;

    private int CurrentEnemyCount;
    [HideInInspector] public int currentEnemyCount => CurrentEnemyCount;
    private List<GameObject> enemyUnits = new List<GameObject>();


    public int CountEnemyUnits()
    {
        GameObject[] enemyUnitObjects = GameObject.FindGameObjectsWithTag("EnemyUnit");
        foreach (GameObject enemyUnit in enemyUnitObjects)
        {
            enemyUnits.Add(enemyUnit);
        }
        CurrentEnemyCount = enemyUnits.Count;
        return CurrentEnemyCount;
    }

    private void OnDisable()
    {
        enemyUnits.Clear();
    }
}
