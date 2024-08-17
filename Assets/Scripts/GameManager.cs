// Name: GameManager.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector Variables
    [Header("Prefabs")]
    public GameObject normalPlayer;
    public GameObject smallPlayer;
    #endregion

    #region Hidden Variables
    #endregion

    #region Functions
    private void Awake()
    {
        // Start game with small player active and normal player inactive
        normalPlayer.SetActive(true);
        smallPlayer.SetActive(false);
    }
    #endregion
}