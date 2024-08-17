// Name: Player Scale Portal.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using UnityEngine;

public class PlayerScalePortal : MonoBehaviour
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
        if (other.tag == "Player" && other.GetComponent<FPSController>().playerScale == FPSController.PlayerScale.NORMAL)
        {
            // Disable the normal player and enable the small player
            gameManager.normalPlayer.SetActive(false);
            gameManager.smallPlayer.SetActive(true);

            // Move the normal player back to their teleport point
            gameManager.normalPlayer.transform.position = teleportPoint.position;

            Debug.Log("Player has entered the portal");
        }
    }
    #endregion
}