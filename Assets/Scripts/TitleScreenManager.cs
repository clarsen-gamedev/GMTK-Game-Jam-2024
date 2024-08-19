// Name: TitleScreenManager.cs
// Author: Connor Larsen
// Date: 08/19/2024
// Description: Controls the function of the game's title screen

using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    #region Public and Serialized Variables
    [Header("UI Screens")]
    [SerializeField] GameObject titleUI;        // Reference to the screen to play and quit the game
    [SerializeField] GameObject wizardUI;       // Reference to the screen which shows the wizard's introduction
    [SerializeField] GameObject creditsUI;      // Reference to the screen which shows the credits

    [Header("Audio")]
    [SerializeField] AudioClip menuSelect;      // Sound that plays when a menu option is clicked

    // Hidden from inspector
    [HideInInspector] public enum UIScreens { TITLE, WIZARD_INTRO, CREDITS, NONE }; // Enum types for each type of screen
    [HideInInspector] public UIScreens currentScreen;                               // Reference to the currently active screen
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = menuSelect;  // Load the audio clip for clicking a menu option
        UISwitch(UIScreens.TITLE);                      // Start on the title screen
    }

    // Function for enabling and disabling UI screens
    public void UISwitch(UIScreens screen)
    {
        // If the title screen is selected...
        if (screen == UIScreens.TITLE)
        {
            // Activate selected screen
            titleUI.SetActive(true);

            // Disable all other screens in the scene
            wizardUI.SetActive(false);
            creditsUI.SetActive(false);

            // Set currentScreen to the selected screen
            currentScreen = UIScreens.TITLE;
        }

        // If the instructions screen is selected...
        else if (screen == UIScreens.WIZARD_INTRO)
        {
            // Activate selected screen
            wizardUI.SetActive(true);

            // Disable all other screens in the scene
            titleUI.SetActive(false);
            creditsUI.SetActive(false);

            // Set currentScreen to the selected screen
            currentScreen = UIScreens.WIZARD_INTRO;
        }

        // If the credits screen is selected...
        else if (screen == UIScreens.CREDITS)
        {
            // Activate selected screen
            creditsUI.SetActive(true);

            titleUI.SetActive(false);
            wizardUI.SetActive(false);

            // Set currentScreen to the selected screen
            currentScreen = UIScreens.CREDITS;
        }
    }
    #endregion

    #region Button Functions
    // Play Game Button
    public void PlayGame()
    {
        GetComponent<AudioSource>().Play(); // Play the button click sound effect
        UISwitch(UIScreens.WIZARD_INTRO);   // Switch to the instructions screen
    }

    // Begin Game Button
    public void BeginGame()
    {
        GetComponent<AudioSource>().Play();     // Play the button click sound effect
        SceneManager.LoadScene("CastleLevel");  // Load the first level of the game
    }

    // Credits Button
    public void Credits()
    {
        GetComponent<AudioSource>().Play(); // Play the button click sound effect
        UISwitch(UIScreens.CREDITS);        // Switch to the credits screen
    }

    // Menu Button
    public void Menu()
    {
        GetComponent<AudioSource>().Play(); // Play the button click sound effect
        UISwitch(UIScreens.TITLE);          // Switch to the title screen
    }

    // Quit Button
    public void QuitGame()
    {
        GetComponent<AudioSource>().Play(); // Play the button click sound effect
        Application.Quit();                 // Quit the game
    }
    #endregion
}