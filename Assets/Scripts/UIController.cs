using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameManager gameManager;
    private int currentCommandPoints;
    private int currentEnemyUnits;
    private int currentPlayerUnits;

    [SerializeField] private LevelData levelData;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI commandPoints;
    [SerializeField] private TextMeshProUGUI enemyUnits;
    [SerializeField] private TextMeshProUGUI playerUnits;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentEnemyUnits = levelData.CountEnemyUnits();

        levelText.text = "Level:" + "   " + levelData.level.ToString();
        commandPoints.text = "Command Points:" + "   " + currentCommandPoints + " / " + levelData.maxCommandPoints.ToString();
        enemyUnits.text = "Enemy Units:" + "   " + currentEnemyUnits;
        playerUnits.text = "Player Units:" + "   " + "0";
    }
}
