using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
    // Use this for initialization
   
	void Awake () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        //// Use for transition
        //animator.SetBool("IsWalking", GameManager.Instance.InputController.IsWalking);
        //animator.SetBool("IsSprinting", GameManager.Instance.InputController.IsSprinting);
        //animator.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);

        //// up down animation 
        //animator.SetFloat("AimAngle", PlayerAim.GetAngle());
        //// aiming
        //animator.SetBool("IsAiming",
                         // GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || 
                         //GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING);

    }
}
