using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    Animator animator;
    // Use this for initialization
    private PlayerAim m_playerAim;
    private PlayerAim PlayerAim
    {
        get{
            if (m_playerAim == null)
                m_playerAim = GameManager.Instance.LocalPlayer.playerAim;
            return m_playerAim;
        }
    }
	void Awake () {
        animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Vertical", GameManager.Instance.InputController.Vertical);
        animator.SetFloat("Horizontal", GameManager.Instance.InputController.Horizontal);

        // Use for transition
        animator.SetBool("IsWalking", GameManager.Instance.InputController.IsWalking);
        animator.SetBool("IsSprinting", GameManager.Instance.InputController.IsSprinting);
        animator.SetBool("IsCrouched", GameManager.Instance.InputController.IsCrouched);

        // up down animation 
        animator.SetFloat("AimAngle", PlayerAim.GetAngle());
        // aiming
        animator.SetBool("IsAiming",
                          GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || 
                         GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING);

    }
}
