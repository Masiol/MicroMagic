using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LevelFailedPanel : MonoBehaviour
{

    [SerializeField] private Image background;
    [SerializeField] private RectTransform panelContent;
    [SerializeField] private Button restartLevel;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        menuButton.onClick.AddListener(GoToMenu);
        restartLevel.onClick.AddListener(Restart);

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


        transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            background.DOFade(0.75f, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
            {
                panelContent.DOScale(Vector3.one, 0.5f).SetEase(Ease.InQuart);
            });
        });
    }

    private void Restart()
    {
        GameManager.instance.RestartGame();
    }

    private void GoToMenu()
    {
        GameManager.instance.GoMenu();
    }
}

