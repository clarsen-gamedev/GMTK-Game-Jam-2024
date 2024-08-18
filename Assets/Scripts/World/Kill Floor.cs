// Name: Collectible.cs
// Author: Connor Larsen
// Date: 08/18/2024
// Description: 

using UnityEngine;

public class KillFloor : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] Transform teleportLocation;
    #endregion

    #region Trigger Functions
    private void OnTriggerEnter(Collider other)
    {
        // Check if an object has hit the Kill Floor
        if (other.gameObject.layer == LayerMask.NameToLayer("Objects"))
        {
            other.transform.position = teleportLocation.position;   // Move the object to the teleport position
        }
    }
    #endregion
}