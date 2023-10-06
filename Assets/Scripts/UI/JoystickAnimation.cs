using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class JoystickAnimation : MonoBehaviour
{
    public GameObject joystick;

    private Animator joystickAnimator;
    private OnScreenStick onScreenStick;
    private Vector3 joystickPosition;
    private Vector3 joystickNextPosition;
    private float movementRange;
    private float cameraAspect;

    void Start()
    {
        joystickAnimator = joystick.GetComponent<Animator>();
        onScreenStick = joystick.GetComponent<OnScreenStick>();
        movementRange = onScreenStick.movementRange;
        cameraAspect = Camera.main.aspect;
        SetJoystickPosition();
    }

    void Update()
    {
        if (cameraAspect != Camera.main.aspect)
        {
            cameraAspect = Camera.main.aspect;
            SetJoystickPosition();
        }
        joystickNextPosition = (joystickPosition - joystick.transform.position) / movementRange;
        Debug.Log(joystickNextPosition);
        //Debug.Log("moveX : " + joystickNextPosition.x + " moveY : " + joystickNextPosition.y);

    }

    private void FixedUpdate()
    {
        joystickAnimator.SetFloat("moveX", joystickNextPosition.x);
        joystickAnimator.SetFloat("moveY", joystickNextPosition.y);
    }

    public void SetJoystickPosition()
    {
        Debug.Log("SetJoystickPosition");
        joystickPosition = joystick.transform.position;
        Debug.Log(joystickPosition);
    }
}