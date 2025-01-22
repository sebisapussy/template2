using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public float bobAmount = 0.05f;  // The amount the camera will bob up and down
    public float bobSpeed = 5f;      // The speed of the bobbing effect

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public static bool Freeze = false;

    public bool canMove = true;
    public static bool chair = false;
    public GameObject chairObject;

    CharacterController characterController;
    public CameraBob cameraBob;

    void Start()
    {

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Initialize CameraBob
        cameraBob = new CameraBob(playerCamera, bobAmount, bobSpeed,gameObject.GetComponent<AudioSource>());
    }

    void Update()
    {
        if (canMove && !Freeze)
        {
            HandleMovement();
        }

        HandleJumping();
        HandleRotation();
        HandleInteraction();

        // Update camera bobbing regardless of other input checks
        if (!Freeze)
        {
            cameraBob.UpdateBobbing();
        }
        
    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump") && canMove && !Freeze)
            {
                moveDirection.y = jumpPower;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

    void HandleRotation()
    {
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!Freeze)
            {
                Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == chairObject)
                    {
                        TeleportPlayer(chairObject.transform.position);
                        Freeze = true; // Freeze the player after teleportation
                        chair = true;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Freeze)
            {
                if (chair)
                {
                    Freeze = false;
                    chair = false;
                }
            }
        }

        void TeleportPlayer(Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }
    }
}
