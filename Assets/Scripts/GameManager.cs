using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    
    private UIController uIController;
    private int maxCommandPoints;

    [SerializeField] private LevelData level;
    private int currentPlayerUnit;
    private int currentEnemyUnits;
    private int currentCommandPoints;

    public static event Action OnGameStart;
   
    private void Awake()
    {
        instance = this;
        uIController = gameObject.GetComponent<UIController>();
        maxCommandPoints = level.maxCommandPoints;
    }
    private void Start()
    {
        uIController.StartButtonState(false);
        UnitPlacer.OnPlacedUnit += RegisterUnit;
    }
    public void GameStart()
    {
        OnGameStart?.Invoke();
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
            currentPlayerUnit -= 1;
            currentCommandPoints -= _commandPoints;
            uIController.UpdatePlayerUnit(currentPlayerUnit);
            uIController.UpdateCommandPoints(currentCommandPoints);
            if (currentPlayerUnit == 0)
            {
                uIController.StartButtonState(false);
            }
        }
        else
        {
            uIController.UpdateEnemyUnit(1);
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
}
