using UnityEngine;

public class playerMove : MonoBehaviour
{
    CharacterController characterController;
    [Header("player stuff")]
    public static int playerMoney = 1500;

    [Header("Opciones de personaje")]
    public float walkSpeed = 6.0f;
    public float runSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    [Header("Opciones de camara")]
    public Camera cam;
    public float mouseHorizontal = 3.0f;
    public float mouseVertical = 2.0f;
    public float minRotation = -65.0f;
    public float maxRotation = 60.0f;
    float h_mouse, v_mouse;
    float multi = 0;
    float a, b = 0;
    public Vector3 currentInputVector;
    private Vector2 smoothInputVelocity;
    float smoothInputSpeed = .15f;
    public static bool isMovingVelocity;

    private Vector3 move = Vector3.zero;

    private float camOrgFOV = 60.0f;
    float curFOV = 60.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        camOrgFOV = cam.fieldOfView;
        curFOV = camOrgFOV;
    }

    void Update()
    {
        if (
                 characterController.velocity.x < -0.3f
                 || characterController.velocity.x > 0.3f
                 || characterController.velocity.z < -0.3f
                 || characterController.velocity.z > 0.3f
             )
        {
            isMovingVelocity = true;
        }
        else
            isMovingVelocity = false;
      /*  if (Input.GetKeyDown(KeyCode.L))
        {
            main.saveProgress();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            main.loadProgress();
        }*/

        if (!pickupController.isOnPCOS)
        {
            currentInputVector = Vector2.SmoothDamp(
                        currentInputVector,
                        GameControls.walkAxis,
                        ref smoothInputVelocity,
                        smoothInputSpeed
                    );
        }
        else
        {
            currentInputVector = Vector2.zero;
        }

        if (pickupController.canRotate && pickupController.pickedObject)
        {

        }
        else
        {
            if (pickupController.isOnPCOS)
            {

            }
            else
            {

                v_mouse += mouseVertical * GameControls.cameraAxis.y;
                v_mouse = Mathf.Clamp(v_mouse, minRotation, maxRotation);
                h_mouse = mouseHorizontal * GameControls.cameraAxis.x;
                cam.transform.localEulerAngles = new Vector3(-v_mouse, 0, 0);

                transform.Rotate(0, h_mouse, 0);

            }
        }


        if (characterController.isGrounded)
        {
            move = new Vector3(currentInputVector.x, 0.0f, currentInputVector.y);

            if (GameControls.inSprint)
            {
                if (isMovingVelocity)
                {
                    multi = Mathf.SmoothDamp(multi, runSpeed, ref a, smoothInputSpeed);
                    curFOV = Mathf.SmoothDamp(curFOV, camOrgFOV + 10, ref b, smoothInputSpeed);
                    move = transform.TransformDirection(move) * multi;
                }
                else
                {
                    multi = Mathf.SmoothDamp(multi, walkSpeed, ref a, smoothInputSpeed);
                    curFOV = Mathf.SmoothDamp(curFOV, camOrgFOV, ref b, smoothInputSpeed);
                    move = transform.TransformDirection(move) * multi;
                }
            }
            else
            {
                multi = Mathf.SmoothDamp(multi, walkSpeed, ref a, smoothInputSpeed);
                curFOV = Mathf.SmoothDamp(curFOV, camOrgFOV, ref b, smoothInputSpeed);
                move = transform.TransformDirection(move) * multi;
            }

            if (GameControls.inJump)
                move.y = jumpSpeed;
        }



        cam.fieldOfView = curFOV;
        move.y -= gravity * Time.deltaTime;
        characterController.Move(move * Time.deltaTime);

    }

}
