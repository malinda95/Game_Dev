using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour {

    public void SetRotation(float dx){
        transform.eulerAngles = new Vector3(transform.eulerAngles.x - dx, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    public float GetAngle(){
        return CheckAngle(transform.eulerAngles.x);
    }

    public float CheckAngle(float value){
        float angle = value - 180;

        if(angle >0)
            return angle - 180;

        return angle + 180;
    }

   
}
