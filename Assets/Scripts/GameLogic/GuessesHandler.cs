using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessesHandler : MonoBehaviour
{
    public GameObject wordContainer;
    public KeyCode guess;

    [SerializeField] GameObject guessesMadeUI;

    private static readonly KeyCode[] keyCodes = { KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y, KeyCode.Z };
    private List<string> guessesMade = new List<string>();

    public bool AttemptToGetGuess()
    {
        if (Input.anyKeyDown)
        {
            DetectKey();
            if (guess != KeyCode.None)
            {
                return true;
            }
        }
        return false;
    }

    private void DetectKey()
    {
        guess = KeyCode.None;
        foreach (KeyCode vKey in keyCodes)
        {
            if (Input.GetKeyDown(vKey))
            {
                if (!guessesMade.Contains(vKey.ToString()))
                    guess = vKey;
            }
        }
    }

    public bool CheckGuess(string chosenWord)
    {
        Debug.Log("Guessing: " + guess);
        guessesMadeUI.GetComponent<Text>().text += guess.ToString() + ' ';
        guessesMade.Add(guess.ToString());
        if (chosenWord.Contains(guess.ToString()))
        {
            foreach (Text letter in wordContainer.GetComponentsInChildren<Text>())
            {
                if (letter.text == guess.ToString())
                    letter.color = new Color(1, 1, 1, 1);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ResetGuesses()
    {
        foreach (Transform child in wordContainer.transform)
        {
            Destroy(child.gameObject);
        }

        guessesMade.Clear();
        guessesMadeUI.GetComponent<Text>().text = "";
    }

}
