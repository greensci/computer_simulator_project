using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{
    [SerializeField]
    public PlayerControls controls;
    //static control varrrrssss (why didnt i think of this before...)
    //controls normal
    public static bool inJump;
    public static bool inSprint;

    public static bool inPrimary;
    public static bool inPrimaryTrigger;
    public static bool inLinkMode;
    public static bool inDisarmMode;
    public static bool inUse;
    public static bool inRotationMode;
    public static Vector2 walkAxis;
    public static Vector2 mouseWheel;
    public static bool inWheelUp;
    public static bool inWheelDown;
    //cam
    public static Vector2 cameraAxis;
    //mouse

    void Awake()
    {
        controls = new PlayerControls();
    }
    void Start()
    {
        controls.Disable();
        controls.Enable();
    }
    void Update()
    {
        walkAxis = controls.Gameplay.Move.ReadValue<Vector2>();
        cameraAxis = controls.Gameplay.Look.ReadValue<Vector2>() * 0.05f;
        mouseWheel = controls.Gameplay.Scroll.ReadValue<Vector2>();

        inPrimary = controls.Gameplay.Primary.IsPressed();
        inPrimaryTrigger = controls.Gameplay.Primary.triggered;

        inWheelDown = controls.Gameplay.ScrollDown.IsPressed();
        inWheelUp = controls.Gameplay.ScrollUp.IsPressed();

        inJump = controls.Gameplay.Jump.IsPressed();

        inLinkMode = controls.Gameplay.LinkMode.triggered;
        inDisarmMode = controls.Gameplay.DisarmMode.triggered;
        inUse = controls.Gameplay.Use.triggered;
        inRotationMode = controls.Gameplay.RotationMode.IsPressed();

        inSprint = controls.Gameplay.Sprint.IsPressed();
    }
}