// Name: FPS Controller.cs
// Author: Connor Larsen
// Date: 08/16/2024
// Description: 

using Unity.VisualScripting;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    #region Public and Serialized Variables
    [Header("Player Controller Variables")]
    public float walkingSpeed = 5f;
    public float runningSpeed = 10f;
    public float jumpSpeed = 6f;
    public float gravity = 20f;

    [Header("Object Pickup Variables")]
    public float pickupDistance = 5f;
    public Transform objectGrabPoint;
    public LayerMask pickupLayerMask;
    
    [HideInInspector] public enum PlayerScale { NORMAL, SMALL, NONE };
    [Header("Player Scale Variables")]
    public PlayerScale playerScale = PlayerScale.NORMAL;

    [Header("Camera")]
    public Camera playerCamera;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [HideInInspector] public bool canMove = true;
    #endregion

    #region Private Variables
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    // Object pickup
    private GrabbableObjectController grabbableObject;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region General Movement
        // When grounded, recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to sprint
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        // Move the player
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // Control jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion

        #region Interactions
        // Left click = shrink
        if (Input.GetMouseButtonDown(0))
        {
            ChangeObjectScale(0);
        }

        // Right click = grow
        if (Input.GetMouseButtonDown(1))
        {
            ChangeObjectScale(1);
        }

        // Press E = Pickup/Drop
        PickupObject();
        #endregion
    }

    // Input 0 for left click and 1 for right click, like in the update function
    private void ChangeObjectScale(int input)
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            ScalableObjectController objectController = objectHit.GetComponent<ScalableObjectController>();

            // Left click (shrink) is clicked
            if (input == 0)
            {
                if (playerScale == PlayerScale.NORMAL && objectController.scaleType == ScalableObjectController.ScaleType.RED ||
                    playerScale == PlayerScale.SMALL && objectController.scaleType == ScalableObjectController.ScaleType.GREEN)
                {
                    objectHit.GetComponent<ScalableObjectController>().ShrinkObject();
                }
            }
            // Right click (grow) is clicked
            else if (input == 1)
            {
                if (playerScale == PlayerScale.NORMAL && objectController.scaleType == ScalableObjectController.ScaleType.RED ||
                    playerScale == PlayerScale.SMALL && objectController.scaleType == ScalableObjectController.ScaleType.GREEN)
                {
                    objectHit.GetComponent<ScalableObjectController>().GrowObject();
                }
            }
        }
    }

    private void PickupObject()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbableObject == null)
            {
                // Pickup the object
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out grabbableObject))
                    {
                        grabbableObject.GrabObject(objectGrabPoint);
                    }
                }
            }
            else
            {
                // Drop the object
                grabbableObject.DropObject();
                grabbableObject = null;
            }
            
        }
        
    }
    #endregion
}