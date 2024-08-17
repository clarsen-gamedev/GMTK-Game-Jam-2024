// Name: Grabbable Object Controller.cs
// Author: Connor Larsen
// Date: 08/16/2024
// Description: 

using UnityEngine;

public class GrabbableObjectController : MonoBehaviour
{
    #region Private Variables
    private FPSController player;

    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private float lerpSpeed = 10f;
    private float rotateAngle = 90f;
    #endregion

    #region Functions
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        player.canMove = true;

        objectRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);

            // Object rotation
            RotateObject();
        }
    }

    private void Update()
    {
        // Lock and unlock the player movement when rotating
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.T))
        {
            player.canMove = false;
        }
        if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.T))
        {
            player.canMove = true;
        }
    }

    public void GrabObject(Transform objectGrabPoint)
    {
        objectGrabPointTransform = objectGrabPoint;
        objectRigidbody.isKinematic = true;
    }

    public void RotateObject()
    {
        // Rotate the object 90 degrees along the Y axis (left/right)
        if (Input.GetKey(KeyCode.R))
        {
            objectRigidbody.transform.Rotate(0, rotateAngle * Time.deltaTime, 0);
        }
        
        // Rotate the object 90 degrees along the X axis (up/down)
        if (Input.GetKey(KeyCode.T))
        {
            objectRigidbody.transform.Rotate(rotateAngle * Time.deltaTime, 0, 0);
        }
    }

    public void DropObject()
    {
        objectGrabPointTransform = null;
        objectRigidbody.isKinematic = false;
    }
    #endregion
}