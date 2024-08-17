// Name: Player Scale Platform.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using UnityEngine;

public class PlayerScalePlatform : MonoBehaviour
{
    #region Collider Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<FPSController>().playerScale == FPSController.PlayerScale.SMALL)
        {
            // Change player scale to normal and teleport to middle of the room
            Debug.Log("Player has entered the platform");
        }
    }
    #endregion
}