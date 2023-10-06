using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private InputManager inputManager;
    private SpriteRenderer spriteRenderer;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float spriteRendererHeight;
    private float spriteRendererWidth;

    public Rigidbody2D theRB;
    public float moveSpeed;

    public Animator myAnim;
    
    public string areaTransitionName;

    public bool isFreezed = false;
    public bool isMobile = false;

    private Vector2 movementInput;
    private Vector3 currentMovement;
    private bool isMovementPressed;

    private void Awake()
    {
        inputManager = new InputManager();

        inputManager.Player.Walk.started += Move;
        inputManager.Player.Walk.performed += Move;
        inputManager.Player.Walk.canceled += Move;
    }

    private void Move(InputAction.CallbackContext context)
    {
        movementInput =  context.ReadValue<Vector2>();

        currentMovement.x = movementInput.x;
        currentMovement.y = movementInput.y;
        isMovementPressed = movementInput.x != 0 || movementInput.y != 0;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            } 
        }

        DontDestroyOnLoad(gameObject);

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            CameraController customCamera = FindObjectOfType<CameraController>();

            spriteRendererHeight = spriteRenderer.bounds.size.y;
            spriteRendererWidth = spriteRenderer.bounds.size.x;
            if(customCamera)
            {
                SetBounds(customCamera.theMap.localBounds.min, customCamera.theMap.localBounds.max);
            }
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found.");
        }

    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    private void FixedUpdate()
    {

        if (!isFreezed)
        {
            if (isMovementPressed)
            {
                theRB.MovePosition(transform.position + currentMovement.normalized * moveSpeed * Time.deltaTime);

                if (currentMovement.x != 0 || currentMovement.y != 0)
                {
                    myAnim.SetFloat("lastMoveX", currentMovement.x);
                    myAnim.SetFloat("lastMoveY", currentMovement.y);
                }
            }
            myAnim.SetFloat("moveX", currentMovement.x);
            myAnim.SetFloat("moveY", currentMovement.y);
        }
        else
        {
            theRB.velocity = Vector2.zero;
            myAnim.SetFloat("moveX", 0);
            myAnim.SetFloat("moveY", 0);
        }
    }

    public void SetBounds(Vector3 botLeft, Vector3 topRight)
    {
        bottomLeftLimit = botLeft + new Vector3(spriteRendererWidth / 2, spriteRendererHeight / 3, 0f);
        topRightLimit = topRight + new Vector3(-spriteRendererWidth / 2, -spriteRendererHeight / 3, 0f);
    }

    private void OnEnable()
    {
        inputManager.Player.Enable();
    }

    private void OnDisable()
    {
        inputManager.Player.Disable();
    }

}
