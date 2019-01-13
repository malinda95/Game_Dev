using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerState))]

public class Player : MonoBehaviour {

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool MouseLocked;
    }

    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float crouchSpeed;

    [SerializeField] MouseInput MouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumMoveTreshold;

    Vector3 previousPosition;

    public PlayerAim playerAim;

    private MoveController m_MoveController;
    public MoveController MoveController
    {
        get
        {
            if (m_MoveController == null)
                m_MoveController = GetComponent<MoveController>();
            return m_MoveController;
        }
    }

    private PlayerState m_PlayerState;
    public PlayerState PlayerState
    {
        get
        {
            if (m_PlayerState == null)
                m_PlayerState = GetComponent<PlayerState>();
            return m_PlayerState;
        }
    }



    Vector2 mouseInput;
    InputController playerInput;

    void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
        if(MouseControl.MouseLocked){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        LookAround();
    }

    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        playerAim.SetRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    private void Move()
    {
        float moveSpeed = runSpeed;

        //change speed according use state 
        if(playerInput.IsWalking)
            moveSpeed = walkSpeed;
        if (playerInput.IsSprinting)
            moveSpeed = sprintSpeed;



        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
        MoveController.Move(direction);
        if (Vector3.Distance(transform.position,previousPosition)>minimumMoveTreshold)
            footSteps.play();


        previousPosition = transform.position;
    }
}
