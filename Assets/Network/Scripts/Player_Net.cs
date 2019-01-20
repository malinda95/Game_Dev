using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class Player_Net : NetworkBehaviour
{

    [SyncVar]
    private bool _isDead;
    public bool isDead{
        get{
            return _isDead;
        }
        protected set {
            _isDead = value;
        }
    }


    private void Update()
    {
        if (!isLocalPlayer){
            return;
        }

        if(Input.GetKeyDown(KeyCode.K)){
            RpcTakeDamage(12000);
        }
    }



    [SerializeField]
    private int max_health = 100;

    [SyncVar]
    private int current_health;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void SetupPlayer()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length;i++){
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();
    }

    [ClientRpc]
    public void RpcTakeDamage(int ammount ){

        if (isDead)
            return;
        current_health -= ammount;
        Debug.Log(transform.name + " has " + current_health + "health now");

        if(current_health <=0){
            Die();
        }
    }

    void Die(){
        isDead = true;

        // Disable component 
       

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        ////Disable GameObjects
        //for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        //{
        //    disableGameObjectsOnDeath[i].SetActive(false);
        //}

        //Disable the collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        StartCoroutine(Respawn());


    }

    //respawn Method
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager_Net.instance.matchSettings.respawnTime);
        SetDefaults();
        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        //yield return new WaitForSeconds(0.1f);

        SetupPlayer();

        Debug.Log(transform.name + " respawned.");
    }

    public void SetDefaults()
    {
        isDead = false;

        current_health = max_health;

        //Enable the components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        ////Enable the gameobjects
        //for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        //{
        //    disableGameObjectsOnDeath[i].SetActive(true);
        //}

        //Enable the collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;

        ////Create spawn effect
        //GameObject _gfxIns = (GameObject)Instantiate(spawnEffect, transform.position, Quaternion.identity);
        //Destroy(_gfxIns, 3f);
    }


}
