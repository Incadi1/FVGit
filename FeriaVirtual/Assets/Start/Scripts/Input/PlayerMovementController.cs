using Start.Inputs;
using Mirror;
using UnityEngine;

public class PlayerMovementController : NetworkBehaviour
{
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private CharacterController controller = null;
    //[SerializeField] private Animation animation = null;

    private Vector2 previousInput;

    private Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
    }

    private new Controls Controls
    {
        get
        {
            if (controls != null)
            {
                return controls;
            }
            return controls = new Controls();
        }
    }

    public override void OnStartAuthority()
    {
        enabled = true;

        Controls.Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        Controls.Player.Move.canceled += ctx => ResetMovement();
    }

    [ClientCallback]
    private void OnEnable()
    {
        controls.Enable();
    }

    [ClientCallback]
    private void OnDisable()
    {
        controls.Disable();
    }

    [ClientCallback]
    private void Update()
    {
        Move();


    }


    private void SetMovement(Vector2 movement)
    {
        previousInput = movement;
    }

    private void ResetMovement()
    {
        previousInput = Vector2.zero;
    }

    private void Move()
    {

        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;

        right.y = 0f;
        forward.y = 0f;

        Vector3 movement = right.normalized * previousInput.x + forward.normalized * previousInput.y;

        controller.Move(movement * movementSpeed * Time.deltaTime);



        /*Vector3 x = anim.GetAxis("Horizontal");
        Vector3 y = anim.transform("Vertical");

        //Linea de comandos
        transform.Rotate(0, x * Time.deltaTime * speedRotation, 0);
        transform.Translate(0, 0, y *  movementSpeed * Time.deltaTime);

        anim.SetFloat("speedX", x);
        anim.SetFloat("speedY", y);*/

    }

}
 