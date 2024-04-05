using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public RectTransform[] panelTransforms;
    public Button startButton; 
    private AudioSource audioS;

    private void Start()
    {
        startButton.onClick.AddListener(LevelManager.instance.LoadNextLevel);
        audioS = GetComponent<AudioSource>();
    }
    public void StartCurrentLevel()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();

        if (levelManager != null)
        {
            levelManager.LoadNextLevel();
        }
        else
        {
            Debug.LogError("LevelManager not found!");
        }
    }
    public void HidePanel(int panelIndex)
    {
        // Ukrywamy panel pod podanym indeksem
        panelTransforms[panelIndex].DOAnchorPosY(-1500f, 1f).SetEase(Ease.InCirc);
    }

    public void ShowPanel(int panelIndex)
    {
        // Pokazujemy panel pod podanym indeksem
        panelTransforms[panelIndex].DOAnchorPosY(-62.5f, 1f).SetEase(Ease.OutBounce);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        audioS.Play();
    }
}
