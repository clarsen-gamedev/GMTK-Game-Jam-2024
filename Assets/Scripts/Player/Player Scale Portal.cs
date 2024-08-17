// Name: Player Scale Portal.cs
// Author: Connor Larsen
// Date: 08/17/2024
// Description: 

using UnityEngine;

public class PlayerScalePortal : MonoBehaviour
{
    #region Collider Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Change the player scale to small and teleport to last used scale platform
        }
    }
    #endregion
}