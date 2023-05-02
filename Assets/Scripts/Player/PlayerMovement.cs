using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Interactable playerFocus; // an Interactable, selected when middle clicked

    [SerializeField] Transform cameraTransform;
    [SerializeField] Camera mainCamera;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float runSpeed = 10;
    [SerializeField] private float walkSpeed = 5;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private LayerMask aimableLayer;

    private Rigidbody rb;
    private Quaternion targetRotation;
    private RaycastHit[] cameraHitObjs;
    private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Check if mouse is hovering over UI. If so, stop player movement
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;
        Vector3 moveDir = verticalInput * cameraForward + horizontalInput * cameraRight;
        Vector3 pToMouseDir = Vector3.zero;
        moveDir.Normalize();

        // *Raycast logics*
        Ray playerToMouseRay = new Ray();

        // Create a ray from camera to the mouse on screen
        Ray cameraToMouseRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        Ray cameraToPlayerRay = new Ray(mainCamera.transform.position, mainCamera.transform.position - transform.position);
        cameraHitObjs = Physics.RaycastAll(cameraToPlayerRay);
        if (cameraHitObjs != null)
        {
            foreach (RaycastHit hit in cameraHitObjs)
            {
                Debug.Log(hit.collider.name);
            }
        }

        // Find Camera hitting point in world space
        if (Physics.Raycast(cameraToMouseRay, out RaycastHit mouseHit, float.MaxValue, aimableLayer)) // cast the ray and see if it hits anything in world
        {
            aimPoint.position = mouseHit.point + new Vector3(0, 2, 0);
            pToMouseDir = aimPoint.position - transform.position;
            
            // Raycast from player to mouse (For shooting/aiming logics)
            //if (Physics.Raycast(transform.position, pToMouseDir, out RaycastHit playerToMouseHit, 50f, aimableLayer))
            //{
            //    Debug.Log(playerToMouseHit);
            //}

            // Cast a ray from player to marker
            playerToMouseRay = new Ray(transform.position, pToMouseDir);
        }

        if (Input.GetMouseButton(1))
        {
            targetRotation = Quaternion.LookRotation(playerToMouseRay.direction); // Character points towards mouse position
            Debug.DrawRay(transform.position, pToMouseDir, Color.red); // Debug

        } else if (moveDir.magnitude != 0) // Rotate the character towards the WSAD movement direction
        {
            targetRotation = Quaternion.LookRotation(moveDir);
        }

        // switch from current rrotation to target rotation
        targetRotation.eulerAngles = new Vector3(0f, targetRotation.eulerAngles.y, 0f); // only rotates Y-axis
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); 

        // Check if player is running or not
        if (Input.GetKey("left shift"))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        // Remove focus using left click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                RemoveFocus();
            }
        }
        // Set focus using middle click
        if (Input.GetMouseButtonDown(2))
        {
            // Using Raycast to detect mouse collision
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus (Interactable newFocus)
    {
        if (newFocus != playerFocus)
        {
            if (playerFocus != null)
                playerFocus.OnDefocused();

            playerFocus = newFocus;
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (playerFocus != null)
            playerFocus.OnDefocused();

        playerFocus = null;
    }
}
