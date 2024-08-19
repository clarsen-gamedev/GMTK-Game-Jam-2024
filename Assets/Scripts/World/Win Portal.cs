// Name: Win Portal.cs
// Author: Connor Larsen
// Date: 08/19/2024
// Description: 

using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPortal : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] string targetScene;
    #endregion

    #region Collider Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<FPSController>().playerScale == FPSController.PlayerScale.NORMAL)
        {
            // Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Load the win scene
            SceneManager.LoadScene(targetScene);
        }
    }
    #endregion
}