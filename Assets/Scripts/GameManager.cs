// Name: GameManager.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Inspector Variables
    [Header("Prefabs")]
    public GameObject normalPlayer;
    public GameObject smallPlayer;
    public GameObject roomDoor;
    public GameObject exitPortal;

    [Header("Gameplay Variables")]
    public int totalCollectibles;

    [Header("UI Panels")]
    public GameObject gameUI;
    public GameObject pauseUI;

    [Header("UI Elements")]
    public TMP_Text UI_CollectibleCounter;
    public GameObject rotationIndicators;
    public GameObject crosshair;
    public GameObject contextIndicator;
    public TMP_Text pickupText;
    public GameObject sizeIndicator;
    public GameObject upRotation;
    public GameObject downRotation;
    public GameObject leftRotation;
    public GameObject rightRotation;

    [Header("UI Sprites")]
    public Sprite smallSizeIndicator;
    public Sprite normalSizeIndicator;

    [Header("Sounds")]
    public AudioClip pickupSound;
    public AudioClip scalingDownSound;
    public AudioClip scalingUpSound;
    public AudioClip scalingLimitMax;
    public AudioClip interactSound;
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
        ResumeGame();

        // Start game with small player active and normal player inactive
        normalPlayer.SetActive(false);
        smallPlayer.SetActive(true);

        // Enable the Game UI
        currentScreen = UIScreen.GAME;
        UISwitch(UIScreen.GAME);

        // Set collectiblesFound to 0
        collectiblesFound = 0;
        UI_CollectibleCounter.text = collectiblesFound + "/" + totalCollectibles;
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

                sizeIndicator.GetComponent<Image>().sprite = smallSizeIndicator;
                sizeIndicator.GetComponent<Image>().color = Color.green;
            }

            else if (normalPlayer.activeSelf == false)
            {
                normalPlayer.SetActive(true);
                smallPlayer.SetActive(false);

                sizeIndicator.GetComponent<Image>().sprite = normalSizeIndicator;
                sizeIndicator.GetComponent<Image>().color = Color.red;
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

    public void CheckWinCondition()
    {
        if (collectiblesFound == totalCollectibles)
        {
            roomDoor.GetComponent<Animator>().SetTrigger("DoorOpen");
            exitPortal.GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void PlaySoundEffect(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
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