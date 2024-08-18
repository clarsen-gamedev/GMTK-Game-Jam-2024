// Name: GameManager.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Inspector Variables
    [Header("Prefabs")]
    public GameObject normalPlayer;
    public GameObject smallPlayer;

    [Header("UI Elements")]
    public TMP_Text UI_CollectibleCounter;
    #endregion

    #region Hidden Variables
    [HideInInspector] public int collectiblesFound;
    #endregion

    #region Functions
    private void Awake()
    {
        // Start game with small player active and normal player inactive
        normalPlayer.SetActive(false);
        smallPlayer.SetActive(true);

        // Set collectiblesFound to 0
        collectiblesFound = 0;
    }
    #endregion
}