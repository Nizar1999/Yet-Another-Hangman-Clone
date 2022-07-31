using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnStateChanged += OnStateChanged;
    }
    private void Update()
    {
        if (GameManager.instance.state == GameState.Restarting)
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                GameManager.instance.UpdateGameState(GameState.GeneratingWord);
            }
        }
    }

    private void OnStateChanged(GameState newState)
    {
        if(newState == GameState.GeneratingWord)
        {
            GetComponent<Animator>().Play("fadeOut");
        }
        if (newState == GameState.Restarting)
        {
            GetComponent<Animator>().Play("fadeIn");
        }
    }
}
