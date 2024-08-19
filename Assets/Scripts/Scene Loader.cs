// Name: Scene Loader.cs
// Author: Connor Larsen
// Date: 08/19/2024
// Description: Controls the function of the game's title screen

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Functions
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    #endregion
}