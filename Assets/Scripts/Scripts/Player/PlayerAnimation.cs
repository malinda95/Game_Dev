using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerState))]

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
    InputController PlayerInputController;

    [SerializeField]
    private Camera Player_Camera;
    [SerializeField]
    private AudioController WalkingSound;

    private PlayerState playerState;
    // Use this for initialization
    void Awake () {
        animator = GetComponentInChildren<Animator>();
        PlayerInputController = GetComponentInChildren<InputController>();
        playerState = GetComponent<PlayerState>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Mathf.Abs(PlayerInputController.Vertical) > 0 || Mathf.Abs(PlayerInputController.Horizontal) > 0){
            Debug.Log("walking");
            WalkingSound.play();
        }

        animator.SetFloat("Vertical", PlayerInputController.Vertical);
        animator.SetFloat("Horizontal", PlayerInputController.Horizontal);

        //// Use for transition
        animator.SetBool("IsWalking", PlayerInputController.IsWalking);
        animator.SetBool("IsSprinting", PlayerInputController.IsSprinting);
        animator.SetBool("IsCrouched", PlayerInputController.IsCrouched);

        // up down animation 
        animator.SetFloat("AimAngle", CheckAngle(Player_Camera.transform.eulerAngles.x));
        //// aiming
        animator.SetBool("IsAiming",
         playerState.WeaponState == PlayerState.EWeaponState.AIMING || 
        playerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING);

        Debug.Log(CheckAngle(Player_Camera.transform.eulerAngles.x));
    }
    public float CheckAngle(float value)
    {
        float angle = value - 180;

        if (angle > 0)
            return angle - 180;

        return angle + 180;
    }
}
