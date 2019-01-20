using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooter :  NetworkBehaviour{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private PlayerWeapon Pw =new PlayerWeapon();
    void Start()
    {
       
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }


    }

    private void Update()
    {
        if (Pw.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / Pw.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
    }
    [Client]
    void Shoot(){
        Debug.Log("TEST: Fire ");
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, Pw.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name,(int)Pw.damage, transform.name);
            }

           
        }
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage, string _sourceID)
    {
        Debug.Log(_playerID + " has been shot.");

        Player_Net _player = GameManager_Net.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage);
    }
}
