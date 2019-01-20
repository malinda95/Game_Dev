using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    // Component caching
    private PlayerMotor motor;
    private Animator animator;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        // Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical) * speed;

        // animation 
        animator.SetFloat("Vertical", _zMov);
        animator.SetFloat("Horizontal", _xMov);

        //Apply movement
        motor.Move(_velocity);



        //animator.SetFloat("Horizontal", _xMov);

        // Calculate rotation as a 3D vector(turning around)

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (turning around)
        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotationX = new Vector3(_xRot, 0f, 0f) * lookSensitivity;
       
        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);


        // Calculate the thrusterforce based on player input
        // Calculate the thrusterforce based on player input
        Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump"))
        {

            _thrusterForce = Vector3.up * thrusterForce;

        }
        // Apply the thruster force
        motor.ApplyThruster(_thrusterForce);
    }
}
