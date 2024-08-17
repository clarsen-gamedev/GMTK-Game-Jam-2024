// Name: Grabbable Object Controller.cs
// Author: Connor Larsen
// Date: 08/16/2024
// Description: 

using UnityEngine;

public class GrabbableObjectController : MonoBehaviour
{
    #region Private Variables
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private float lerpSpeed = 10f;
    #endregion

    #region Functions
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }

    public void GrabObject(Transform objectGrabPoint)
    {
        objectGrabPointTransform = objectGrabPoint;
        objectRigidbody.useGravity = false;
    }

    public void DropObject()
    {
        objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
    }
    #endregion
}