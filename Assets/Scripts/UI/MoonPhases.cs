using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonPhases : MonoBehaviour
{
    private void Awake()
    {
        GameManager.OnStateChanged += OnStateChanged;
        GameManager.OnGuess += UpdateMoonPhases;
    }

    private void OnStateChanged(GameState newState)
    {
        if (newState == GameState.Restarting)
        {
            SpriteRenderer[] rends = GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer rend in rends)
            {
                rend.color = new Color(1, 1, 1, 0);
            }
        }
    }

    private void UpdateMoonPhases(int mistakes, bool correct)
    {
        if (correct)
            return;
        Animation[] anims = GetComponentsInChildren<Animation>();
        SpriteRenderer[] rends = GetComponentsInChildren<SpriteRenderer>();
        rends[(mistakes - 1) % 4].color = new Color(1, 1, 1, mistakes <= 4 ? 0.2f : 1);
    }
}
