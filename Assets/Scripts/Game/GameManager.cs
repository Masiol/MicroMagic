using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    public static event Action OnGameStart;
    [HideInInspector] public int currentEnemyUnits;
    [SerializeField] private LevelData level;

    private UIController uIController;
    private int maxCommandPoints;  
    private int currentPlayerUnit;
    private int currentCommandPoints;
    private bool startGame;
    private bool finishMap;
   
    private void Awake()
    {
        currentEnemyUnits = 0;
        instance = this;
        UnitPlacer.OnPlacedUnit += RegisterUnit;
        uIController = gameObject.GetComponent<UIController>();
        currentEnemyUnits = level.CountEnemyUnits();
        maxCommandPoints = level.maxCommandPoints;
    }
    private void Start()
    {
        uIController.StartButtonState(false);
    }
    public void GameStart()
    {
        OnGameStart?.Invoke();
        startGame = true;
    }
    private void OnDisable()
    {
        UnitPlacer.OnPlacedUnit -= RegisterUnit;
    }
    private void RegisterUnit(int _unit, int _commandPoints)
    {
        currentPlayerUnit += _unit;
        currentCommandPoints += _commandPoints;
        uIController.UpdatePlayerUnit(currentPlayerUnit);
        uIController.UpdateCommandPoints(currentCommandPoints);

        if(currentPlayerUnit > 0)
        {
            uIController.StartButtonState(true);
        }
        
    }
    public void UnregisterUnit(bool _isEnemy, int _commandPoints)
    {
        if (!_isEnemy)
        {
            currentPlayerUnit--;
            currentCommandPoints -= _commandPoints;
            uIController.UpdatePlayerUnit(currentPlayerUnit);
            uIController.UpdateCommandPoints(currentCommandPoints);
            if (currentPlayerUnit == 0)
            {
                uIController.StartButtonState(false);
            }
            CheckEndGame();
        }
        else
        {
            uIController.UpdateEnemyUnit(1);
            currentEnemyUnits--;
            CheckEndGame();
        }
    }
    public bool CanPlaceNextUnit(int _commandPoints)
    {
        if (currentCommandPoints + _commandPoints <= maxCommandPoints)
        {
            return true;
        }
        else
        {
            uIController.CommandPointReachedAlert();
            return false;
        }
    }
    public int GetCurrentCommandPoints()
    {
        return currentCommandPoints;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void CheckEndGame()
    {
        if (!finishMap)
        {           
            if (currentEnemyUnits == 0 && startGame)
            {
                if (SceneManager.GetActiveScene().name != "Level4")
                {
                    finishMap = true;
                    Debug.Log(currentEnemyUnits);
                    uIController.LevelComplete();
                    LevelManager.instance.CompleteLevel(level.level);
                }
                else
                {
                    finishMap = true;
                    Debug.Log(currentEnemyUnits);
                    uIController.GameComplete();
                }
            }
            if (currentPlayerUnit == 0 && startGame)
            {
                finishMap = true;
                uIController.GameOver();
                Debug.Log("gameOver");
            }
        }
    }
    
}
