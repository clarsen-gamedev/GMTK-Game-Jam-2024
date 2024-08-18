// Name: Player Scale Platform.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using UnityEngine;

public class PlayerScalePlatform : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] Transform teleportPoint;
    #endregion

    #region Hidden Variables
    private GameManager gameManager;
    #endregion

    #region Functions
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    #endregion

    #region Collider Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<FPSController>().playerScale == FPSController.PlayerScale.SMALL)
        {
            // Disable the small player and enable the normal player
            gameManager.normalPlayer.SetActive(true);
            gameManager.smallPlayer.SetActive(false);

            // Move the small player back to their teleport point
            gameManager.smallPlayer.transform.position = teleportPoint.position;

            Debug.Log("Player has entered the platform");
        }
    }
    #endregion
}