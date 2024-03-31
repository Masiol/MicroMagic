using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static event Action OnGameStart;

    private void Awake()
    {
        instance = this;
    }
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }
}
