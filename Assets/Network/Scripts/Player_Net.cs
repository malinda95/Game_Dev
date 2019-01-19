using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class Player_Net : NetworkBehaviour {

    [SerializeField]
    private int max_health = 100;

    [SyncVar]
    private int current_health;

    private void Awake()
    {
        current_health = max_health;
    }

    public void TakeDamage(int ammount ){
        current_health -= ammount;
        Debug.Log(transform.name + " has " + current_health + "health now");
    }



}
