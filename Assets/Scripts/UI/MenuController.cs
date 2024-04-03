using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public RectTransform[] panelTransforms;
    private AudioSource audioS;
    public Button startButton;
    // Start is called before the first frame update

    private void Start()
    {
        startButton.onClick.AddListener(LevelManager.instance.LoadNextLevel);
        audioS = GetComponent<AudioSource>();
    }
    public void StartCurrentLevel()
    {
        // Pobierz referencj� do obiektu LevelManager
        LevelManager levelManager = FindObjectOfType<LevelManager>();

        // Sprawd�, czy uda�o si� znale�� LevelManager
        if (levelManager != null)
        {
            // Uruchom aktualn� scen� za pomoc� metody LoadNextLevel z LevelManagera
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
