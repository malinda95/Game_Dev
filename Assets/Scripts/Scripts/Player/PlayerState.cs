using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private InputController inputController;

    private void Start()
    {
        inputController = GetComponent<InputController>();
    }

    public enum EMoveState
    {
        WALKING,
        RUNNINING,
        CROUCHING,
        SPRINTING
    }

    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }

    public EMoveState MoveState;
    public EWeaponState WeaponState;

   

    //private void Update()
    //{
    //    SetWeaponState();
    //    SetMoveState();
    //}

    //void SetWeaponState()
    //{
    //    WeaponState = EWeaponState.IDLE;

    //    if (InputController.Fire1)
    //        WeaponState = EWeaponState.FIRING;

    //    if (InputController.Fire2)
    //        WeaponState = EWeaponState.AIMING;

    //    if (InputController.Fire1 && InputController.Fire2)
    //        WeaponState = EWeaponState.AIMEDFIRING;
    //}

    //void SetMoveState()
    //{
    //    MoveState = EMoveState.RUNNINING;

    //    if (InputController.IsSprinting)
    //        MoveState = EMoveState.SPRINTING;

    //    if (InputController.IsWalking)
    //        MoveState = EMoveState.WALKING;

    //    if (InputController.IsCrouched)
    //        MoveState = EMoveState.CROUCHING;
    //}
}

