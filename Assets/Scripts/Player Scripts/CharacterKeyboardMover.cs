
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/**
 * This component moves a player controlled with a CharacterController using the keyboard.
 */
[RequireComponent(typeof(CharacterController))]
public class CharacterKeyboardMover: MonoBehaviour {
    [Tooltip("Speed of player keyboard-movement, in meters/second")]
    [SerializeField] float walkingSpeed = 2f;
    [SerializeField] float runningSpeed = 5;

    [SerializeField] float jumpHeight = 1.5f; // גובה הקפיצה

    [SerializeField] float gravity = 9.81f;

    float currentSpeed;

    private CharacterController cc;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    [SerializeField] InputAction runAction;


    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        runAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        runAction.Disable();
    }
    void OnValidate()
    {
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Button);
        if (moveAction.bindings.Count == 0)
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");

        if (jumpAction == null)
            jumpAction = new InputAction(type: InputActionType.Button);
        if (jumpAction.bindings.Count == 0)
            jumpAction.AddBinding("<Keyboard>/space");
        if (runAction == null)
            runAction = new InputAction(type: InputActionType.Button);
        if (runAction.bindings.Count == 0)
            runAction.AddBinding("<Keyboard>/leftShift");
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        currentSpeed = walkingSpeed;
    }

    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 horizontalVelocity = new Vector3(0, 0, 0);

    void Update()
    {
        currentSpeed = runAction.ReadValue<float>() > 0 ? runningSpeed : walkingSpeed; // קביעת מהירות הליכה בהתאם ללחיצה על המקש שמפעיל את הריצה
        // התנועה במישור היא מחוץ לתנאי כדי שלא תהיה תלות בהיותו של השחקן על הקרקע כדי שיהיה אפשר להמשיך עם התנועה גם כאשר השחקן קופץ
        Vector3 movement = moveAction.ReadValue<Vector2>(); // תנועה במישור
        horizontalVelocity.x = movement.x * currentSpeed;
        horizontalVelocity.z = movement.y * currentSpeed;

        if (cc.isGrounded)
        {
            velocity.y = -gravity * Time.deltaTime; // לוודא שהשחקן לא "מרחף"

            if (jumpAction.triggered)
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity); // חישוב מהירות קפיצה
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime; // הפעלת כוח הכבידה
        }

        // תנועה בכיוון שמסתכלים
        Vector3 finalVelocity = transform.TransformDirection(horizontalVelocity) + new Vector3(0, velocity.y, 0);

        cc.Move(finalVelocity * Time.deltaTime);
    }
}
