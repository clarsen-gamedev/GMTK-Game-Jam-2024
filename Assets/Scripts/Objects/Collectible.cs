// Name: Collectible.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using UnityEngine;

public class Collectible : MonoBehaviour
{
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
        // Check to see if the player is in the SMALL mode, NORMAL player cannot collect these
        if (other.tag == "Player" && other.GetComponent<FPSController>().playerScale == FPSController.PlayerScale.SMALL)
        {
            // Increase collectiblesFound in the GameManager by 1
            gameManager.collectiblesFound++;
            gameManager.UI_CollectibleCounter.text = "Scales: " + gameManager.collectiblesFound + "/" + gameManager.totalCollectibles;

            // Delete collectible
            Destroy(gameObject);
        }
    }
    #endregion
}