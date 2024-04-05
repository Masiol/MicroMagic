using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameCompletePanel : MonoBehaviour
{

    [SerializeField] private Image background;
    [SerializeField] private RectTransform panelContent;
    [SerializeField] private Button menuButton;

    private void Start()
    {
        menuButton.onClick.AddListener(GoToMenu);

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


        transform.DOScale(Vector3.one, 0.75f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            background.DOFade(0.75f, 0.5f).SetEase(Ease.InSine).OnComplete(() =>
            {
                panelContent.DOScale(Vector3.one, 0.5f).SetEase(Ease.InQuart);
            });
        });
    }

    private void LoadLevel()
    {
        LevelManager.instance.LoadNextLevel();
    }

    private void GoToMenu()
    {
        LevelManager.instance.ResetGame();
        GameManager.instance.GoMenu();
    }

}

