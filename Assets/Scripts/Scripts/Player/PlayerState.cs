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

   

    private void Update()
    {
        SetWeaponState();
        SetMoveState();
    }

    void SetWeaponState()
    {
        WeaponState = EWeaponState.IDLE;

        if (inputController.Fire1)
            WeaponState = EWeaponState.FIRING;

        if (inputController.Fire2)
            WeaponState = EWeaponState.AIMING;

        if (inputController.Fire1 && inputController.Fire2)
            WeaponState = EWeaponState.AIMEDFIRING;
    }

    void SetMoveState()
    {
        MoveState = EMoveState.RUNNINING;

        if (inputController.IsSprinting)
            MoveState = EMoveState.SPRINTING;

        if (inputController.IsWalking)
            MoveState = EMoveState.WALKING;

        if (inputController.IsCrouched)
            MoveState = EMoveState.CROUCHING;
    }
}

