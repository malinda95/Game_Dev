using System;
using UnityEngine;
using UnityEngine.Networking;


[RequireComponent(typeof(Player_Net))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";


    [SerializeField]
    GameObject playerUIPrefab;
    [HideInInspector]
    public GameObject playerUIInstance;

    void Start()
    {
        // Disable components that should only be
        // active on the player that we control
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }

        else
        {
            GetComponent<Player_Net>().SetupPlayer();
            // Create PlayerUI
            playerUIInstance = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;

        }
    }
    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player_Net _player = GetComponent<Player_Net>();

        GameManager_Net.RegisterPlayer(_netID, _player);
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        GameManager_Net.UnRegisterPlayer(transform.name);
    }
}
