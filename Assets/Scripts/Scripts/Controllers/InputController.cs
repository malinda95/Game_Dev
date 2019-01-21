using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Fire2;
    public bool Reload;
    // add to animation handle
    public bool IsSprinting;
    public bool IsWalking;
    public bool IsCrouched;
    public bool MouseWheelUp;
    public bool MouseWheelDown;

	
	// Update is called once per frame
	void Update () {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        //MouseInput 
        Fire1 = Input.GetButton("Fire1");
        Fire2 = Input.GetButton("Fire2");
        Reload = Input.GetKey(KeyCode.R);
        IsWalking = Input.GetKey(KeyCode.LeftAlt);
        IsSprinting = Input.GetKey(KeyCode.LeftShift);
        IsCrouched = Input.GetKey(KeyCode.C);
        MouseWheelUp = Input.GetAxis("Mouse ScrollWheel")>0;
        MouseWheelDown = Input.GetAxis("Mouse ScrollWheel")<0;

	}
}
