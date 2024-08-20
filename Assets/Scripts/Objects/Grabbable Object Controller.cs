// Name: Grabbable Object Controller.cs
// Author: Connor Larsen - + a teeny bit JM <3
// Date: 08/16/2024
// Description: 

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabbableObjectController : MonoBehaviour
{
    #region Hidden Variables
    private ScalableObjectController scaleController;

    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private float lerpSpeed = 10f;
    private float rotateAngle = 180f;
    Vector3 rotationEulerAngles;

    private GameManager gameManager;

    private FPSController player;
    private FPSController smallPlayer;
    #endregion

    #region Functions
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        player = gameManager.normalPlayer.GetComponent<FPSController>();
        smallPlayer = gameManager.smallPlayer.GetComponent<FPSController>();

        player.canMove = true;
        smallPlayer.canMove = true;

        scaleController = GetComponent<ScalableObjectController>();
        objectRigidbody = GetComponent<Rigidbody>();
        rotationEulerAngles = player.transform.eulerAngles;
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
        //if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.R))
        if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.F))
        {
            if (objectRigidbody.isKinematic)
            {
                player.canMove = false;
                smallPlayer.canMove = false;
            }
        }
        //if (Input.GetKeyUp(KeyCode.H) || Input.GetKeyUp(KeyCode.G) || Input.GetKeyUp(KeyCode.T) || Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.R))
        if (Input.GetKeyUp(KeyCode.H) || Input.GetKeyUp(KeyCode.G) || Input.GetKeyUp(KeyCode.T) || Input.GetKeyUp(KeyCode.F))
        {
            if (objectRigidbody.isKinematic)
            {
                player.canMove = true;
                smallPlayer.canMove = true;
            }
        }


        //reset rotation to pickup position, testing
        if(Input.GetKeyUp(KeyCode.Y))
        {
            player.canMove = true;
            smallPlayer.canMove = true;
        }
    }

    public void GrabObject(Transform objectGrabPoint)
    {
        objectGrabPointTransform = objectGrabPoint;
        objectRigidbody.isKinematic = true;

        gameManager.pickupText.text = "E - Drop";

        // Play the grab sound effect
        gameManager.PlaySoundEffect(gameManager.normalPlayer.GetComponent<AudioSource>(), gameManager.interactSound);
        gameManager.PlaySoundEffect(gameManager.smallPlayer.GetComponent<AudioSource>(), gameManager.interactSound);
    }

    public void RotateObject()
    {

        // Rotate the object 90 degrees along the Y axis (right)
        /*if(Input.GetKey(KeyCode.R))
        {
            objectRigidbody.transform.Rotate(0, Input.GetAxis("Mouse X") * rotateAngle * Time.deltaTime, Input.GetAxis("Mouse Y") * rotateAngle * Time.deltaTime, Space.Self);
        }*/

        if (Input.GetKey(KeyCode.H))
        {

            objectRigidbody.transform.Rotate(0, -rotateAngle * Time.deltaTime, 0, Space.Self);
        }
        
        // Rotate the object 90 degrees along the X axis (up)
        if (Input.GetKey(KeyCode.G))
        {

            objectRigidbody.transform.Rotate(0, 0, -rotateAngle * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.F))
        {
            objectRigidbody.transform.Rotate(0, rotateAngle * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.T))
        {
            objectRigidbody.transform.Rotate(0, 0, rotateAngle * Time.deltaTime, Space.Self);
        }

        if (Input.GetKey(KeyCode.Y))
        {
            objectRigidbody.transform.localEulerAngles = new Vector3(0.0f, objectRigidbody.transform.localEulerAngles.y, 0.0f);
            objectRigidbody.transform.localEulerAngles = new Vector3(objectRigidbody.transform.localEulerAngles.x, 0.0f, 0.0f);
            objectRigidbody.transform.localEulerAngles = new Vector3(0.0f, 0.0f, objectRigidbody.transform.localEulerAngles.z);
        }
    }

    public void DropObject()
    {
        objectGrabPointTransform = null;
        objectRigidbody.isKinematic = false;

        gameManager.pickupText.text = "E - Pickup";
    }
    #endregion
}