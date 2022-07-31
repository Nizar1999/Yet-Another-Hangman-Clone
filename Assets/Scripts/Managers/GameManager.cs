using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public static event System.Action<GameState> OnStateChanged;
    public static event System.Action<int,bool> OnGuess;
    public GameState state;
    public GuessesHandler guessHandler;

    [SerializeField] AudioClip bgMusic;
    [SerializeField] AudioClip startSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;

    private Words word;
    private int mistakes = 0;
    private string wordFromCorrect = "";

    private void Awake()
    {
        word = GetComponent<Words>();
        guessHandler = GetComponent<GuessesHandler>();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        AudioManager.instance.PlayMusic(bgMusic);
        UpdateGameState(GameState.Restarting);
    }

    private void Update()
    {
        HandleInput();
        if (state == GameState.Guessing)
        {
            HandleNewGuess();
        }
    }

    private void HandleNewGuess()
    {
        if (guessHandler.AttemptToGetGuess())
        {
            if (guessHandler.CheckGuess(word.chosenWord))
            {
                HandleCorrectGuess();
            }
            else
            {
                HandleWrongGuess();
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void HandleWrongGuess()
    {
        if (mistakes == 8)
        {
            TriggerLose();
        }
        else
        {
            mistakes += 1;
            OnGuess?.Invoke(mistakes, false);
        }
    }

    private void TriggerLose()
    {
        guessHandler.ResetGuesses();
        word.DisplayWord("Lose");
    
        AudioManager.instance.PlaySound(loseSound);
    
        Invoke("DisplayCorrect", 2);
    }

    private void TriggerWin()
    {
        guessHandler.ResetGuesses();
        word.DisplayWord("Win");
    
        AudioManager.instance.PlaySound(winSound);
        Invoke("DelayRestart", 2);
    }

    private void HandleCorrectGuess()
    {
        wordFromCorrect += guessHandler.guess.ToString();
        foreach(char letter in word.chosenWord.ToCharArray()) {
            if(!wordFromCorrect.Contains(letter.ToString()))
            {
                return;
            }
        }
        TriggerWin();
    }

    private void DisplayCorrect()
    {
        guessHandler.ResetGuesses();
        word.DisplayWord(word.chosenWord);
        Invoke("DelayRestart", 2);
    }

    public void DelayRestart()
    {
        wordFromCorrect = "";
        mistakes = 0;
        guessHandler.ResetGuesses();
        UpdateGameState(GameState.Restarting);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;
        if(newState == GameState.Restarting)
        {
            AudioManager.instance.PlaySound(startSound);
        }
        OnStateChanged?.Invoke(newState);
    }
}
public enum GameState
{
    GeneratingWord,
    Guessing,
    Restarting,
}
