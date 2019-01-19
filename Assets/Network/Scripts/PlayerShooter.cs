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
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }
    [Client]
    void Shoot(){

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

        //Player _player = GameManager.GetPlayer(_playerID);
        //_player.RpcTakeDamage(_damage, _sourceID);
    }
}
