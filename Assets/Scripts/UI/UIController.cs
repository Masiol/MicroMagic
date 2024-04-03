using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

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

    [SerializeField] private RectTransform unitsPanelBar;
    [SerializeField] private RectTransform startButton;

    [SerializeField] private TextMeshProUGUI commandPointReachedAlert;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button startBtn;
    [Space]
    [Header("Unit Stats")]
    [SerializeField] private TextMeshProUGUI unitName;
    [SerializeField] private TextMeshProUGUI unitHealth;
    [SerializeField] private TextMeshProUGUI unitAttackSpeed;
    [SerializeField] private TextMeshProUGUI unitDamage;
    [SerializeField] private TextMeshProUGUI unitRange;
    [SerializeField] private TextMeshProUGUI unitCommandPoints;

    [SerializeField] private Transform gameCompletePanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject gameOverPanel;



    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        GameManager.OnGameStart += HideUI;
        currentEnemyUnits = gameManager.currentEnemyUnits;

        levelText.text = "Level:" + "   " + levelData.level.ToString();
        commandPoints.text = "Command Points:" + "   " + currentCommandPoints + " / " + levelData.maxCommandPoints.ToString();
        enemyUnits.text = "Enemy Units:" + "   " + currentEnemyUnits;
        playerUnits.text = "Player Units:" + "   " + "0";

        SetupButtons();
    }

    private void SetupButtons()
    {
        restartButton.onClick.AddListener(GameManager.instance.RestartGame);
    }
    private void HideUI()
    {
        float targetY = -500f;
        float duration = 4.25f;

        startButton.DOAnchorPosY(targetY, duration).SetEase(Ease.OutBack);

        unitsPanelBar.DOAnchorPosY(targetY, duration).SetEase(Ease.OutBack);
    }
    public void UpdatePlayerUnit(int _playerUnit)
    {
       playerUnits.text = "Player Units:" + "   " + _playerUnit;
    }
    public void UpdateCommandPoints(int _commandPoints)
    {
        commandPoints.text = "Command Points:" + "   " + _commandPoints + " / " + levelData.maxCommandPoints.ToString();
    }
    public void UpdateEnemyUnit(int _enemyUnit)
    {
        currentEnemyUnits -= _enemyUnit;
        enemyUnits.text = "Enemy Units:" + "   " + currentEnemyUnits;
    }
    public void CommandPointReachedAlert()
    {
        commandPointReachedAlert.color = new Color(commandPointReachedAlert.color.r, commandPointReachedAlert.color.g, commandPointReachedAlert.color.b, 0f); // Ustawienie przezroczystoœci na 0, aby rozpocz¹æ z efektem fade-in
        commandPointReachedAlert.DOFade(1f, 0.5f).OnComplete(() =>
        {
            // Po zakoñczeniu efektu fade-in, rozpocznij efekt fade-out po czasie wyœwietlania
            commandPointReachedAlert.DOFade(0f, 0.5f).SetDelay(1);
        });
    }

    public void StartButtonState(bool _state)
    {
        startBtn.interactable = _state;
    }

    public void UpdatePanelStat(UnitSO _unit)
    {
        unitName.text = _unit.unitName;
        unitHealth.text = "Health:  " + _unit.health.ToString();
        unitAttackSpeed.text = "Atk Speed:  " + _unit.scaleSpeed.ToString() + " / s";
        unitDamage.text = "Damage:  " + _unit.damage.ToString();
        unitDamage.text = "Range:  " + _unit.attackRange.ToString();
        unitCommandPoints.text = "Command Points:  " + _unit.commandPoints.ToString();
    }
    public void LevelComplete()
    {
        GameObject levelCompleteUI = Instantiate(levelCompletePanel);
        levelCompleteUI.transform.SetParent(gameCompletePanel);
        levelCompleteUI.transform.localScale = Vector3.zero; // Ustawienie pocz¹tkowej skali na zero

        // Animacja skalowania do skali 1 za pomoc¹ DoTween
        levelCompleteUI.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InQuint);
    }

    public void GameOver()
    {
        GameObject gameOverUI = Instantiate(gameOverPanel);
        gameOverUI.transform.SetParent(gameCompletePanel);
        gameOverUI.transform.localScale = Vector3.zero; // Ustawienie pocz¹tkowej skali na zero

        // Animacja skalowania do skali 1 za pomoc¹ DoTween
        gameOverUI.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InQuint);
    }

}
