// Name: GameManager.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector Variables
    [Header("Prefabs")]
    public GameObject normalPlayer;
    public GameObject smallPlayer;

    [Header("Gameplay Variables")]
    public int totalCollectibles;

    [Header("UI Panels")]
    public GameObject gameUI;
    public GameObject pauseUI;

    [Header("UI Elements")]
    public TMP_Text UI_CollectibleCounter;
    #endregion

    #region Hidden Variables
    [HideInInspector] public int collectiblesFound;
    [HideInInspector] public enum UIScreen { GAME, PAUSE, NONE };
    [HideInInspector] public UIScreen currentScreen = UIScreen.NONE;

    [HideInInspector] public bool isPaused = false;
    #endregion

    #region Functions
    private void Awake()
    {
        // Start game with small player active and normal player inactive
        normalPlayer.SetActive(false);
        smallPlayer.SetActive(true);

        // Enable the Game UI
        currentScreen = UIScreen.GAME;
        UISwitch(UIScreen.GAME);

        // Set collectiblesFound to 0
        collectiblesFound = 0;
        UI_CollectibleCounter.text = "Scales: " + collectiblesFound + "/" + totalCollectibles;
    }

    private void Update()
    {
        // Press Tab = Grow/Shrink Player
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (smallPlayer.activeSelf == false)
            {
                normalPlayer.SetActive(false);
                smallPlayer.SetActive(true);
            }

            else if (normalPlayer.activeSelf == false)
            {
                normalPlayer.SetActive(true);
                smallPlayer.SetActive(false);
            }
        }

        // Press ESC = Pause Game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Only function if in the GAME or PAUSE screen
            if (currentScreen == UIScreen.GAME || currentScreen == UIScreen.PAUSE)
            {
                // If game is paused, resume game
                if (isPaused)
                {
                    ResumeGame();
                }
                // If game is active, pause game
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void ResumeGame()
    {
        // Play pause game audio
        //GetComponent<AudioSource>().clip = pauseSound;
        //GetComponent<AudioSource>().Play();

        // Unlock player movement
        normalPlayer.GetComponent<FPSController>().canMove = true;
        smallPlayer.GetComponent<FPSController>().canMove = true;

        // Switch to the game screen
        UISwitch(UIScreen.GAME);

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Resume game time
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        // Play pause game audio
        //GetComponent<AudioSource>().clip = pauseSound;
        //GetComponent<AudioSource>().Play();

        // Lock player movement
        normalPlayer.GetComponent<FPSController>().canMove = false;
        smallPlayer.GetComponent<FPSController>().canMove = false;

        // Switch to the pause screen
        UISwitch(UIScreen.PAUSE);

        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Pause game time
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UISwitch(UIScreen screen)
    {
        // Game UI
        if (screen == UIScreen.GAME)
        {
            // Disable all other screens
            pauseUI.SetActive(false);

            // Enable only the game screen
            gameUI.SetActive(true);
        }

        // Pause UI
        else if (screen == UIScreen.PAUSE)
        {
            // Disable all other screens
            gameUI.SetActive(false);

            // Enable only the pause screen
            pauseUI.SetActive(true);
        }
    }
    #endregion
}