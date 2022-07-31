using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Words : MonoBehaviour
{
    public string chosenWord = "";

    [SerializeField] GameObject letterUI;
    [SerializeField] TextAsset wordsFile;

    private string[] words;

    private void Awake()
    {
        GameManager.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState state)
    {
        if(state == GameState.GeneratingWord)
        {
            ChooseRandomWord();
            GameManager.instance.UpdateGameState(GameState.Guessing);
        }
    }

    private void Start()
    {
        ReadWords();
    }

    public void ReadWords()
    {
        Debug.Log("Reading Words");
        words = wordsFile.text.Split('\n');
    }

    public void ChooseRandomWord()
    {
        int randomWordIndex = Random.Range(0, words.Length - 1);
        chosenWord = words[randomWordIndex].ToUpper().Trim();
        Debug.Log("Chosen Word: " + chosenWord);
        //GetComponent<AudioSource>().clip = gameSounds[0];
        //GetComponent<AudioSource>().Play();
        foreach (char letter in chosenWord.ToCharArray())
        {
            GameObject newLetter = Instantiate(letterUI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newLetter.GetComponentInChildren<Text>().text = letter.ToString();
            newLetter.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 0);
            newLetter.transform.SetParent(GameManager.instance.guessHandler.wordContainer.transform, false);
        }
    }

    public void DisplayWord(string wordToDisplay)
    {
        foreach (char letter in wordToDisplay.ToCharArray())
        {
            GameObject newLetter = Instantiate(letterUI, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            newLetter.GetComponentInChildren<Text>().text = letter.ToString();
            newLetter.GetComponentInChildren<Text>().color = new Color(1, 1, 1, 1);
            newLetter.transform.SetParent(GameManager.instance.guessHandler.wordContainer.transform, false);
        }
    }
}
