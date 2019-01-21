using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour {

    [SerializeField]
    PauseMenu PlayerPauseMenu;

	[SyncVar]
	private bool _isDead = false;
	public bool isDead
	{
		get { return _isDead; }
		protected set { _isDead = value; }
	}
    private NetworkManager networkManager;

   

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    public float GetHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }

	[SerializeField]
	private Behaviour[] disableOnDeath;
	private bool[] wasEnabled;

	[SerializeField]
	private GameObject[] disableGameObjectsOnDeath;

	[SerializeField]
	private GameObject deathEffect;

	[SerializeField]
	private GameObject spawnEffect;

    [SerializeField]
    private GameObject killEffect;

    private bool firstSetup = true;
    private int life_Count;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        life_Count = 3;
    }

    public void SetupPlayer ()
    {
		if (isLocalPlayer)
		{
			//Switch cameras
			GameManager.instance.SetSceneCameraActive(false);
			GetComponent<PlayerSetup>().playerUIInstance.SetActive(true);
		}

		CmdBroadCastNewPlayerSetup();
    }

	[Command]
	private void CmdBroadCastNewPlayerSetup ()
	{
		RpcSetupPlayerOnAllClients();
    }

	[ClientRpc]
	private void RpcSetupPlayerOnAllClients ()
	{
		if (firstSetup)
		{
			wasEnabled = new bool[disableOnDeath.Length];
			for (int i = 0; i < wasEnabled.Length; i++)
			{
				wasEnabled[i] = disableOnDeath[i].enabled;
			}

			firstSetup = false;
		}

		SetDefaults();
	}

	[ClientRpc]
    public void RpcTakeDamage (int _amount)
    {
		if (isDead)
			return;

        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

		if (currentHealth <= 0)
		{
			Die();
		}
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.G)){
            RpcTakeDamage(200);
        }
    }

    private void Die()
	{
		isDead = true;
        life_Count -= 1;
		//Disable components
		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = false;
		}

		//Disable GameObjects
		for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
		{
			disableGameObjectsOnDeath[i].SetActive(false);
		}

		//Disable the collider
		Collider _col = GetComponent<Collider>();
		if (_col != null)
			_col.enabled = false;
        if(life_Count>=1){
            //Spawn a death effect
            GameObject _gfxIns = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(_gfxIns, 3f);
        }
        else{
            //Spawn a death effect
            GameObject _gfxIns = (GameObject)Instantiate(killEffect, transform.position,Quaternion.identity);
            Destroy(_gfxIns, 5f);
        }
		

		//Switch cameras
		if (isLocalPlayer)
		{
			GameManager.instance.SetSceneCameraActive(true);
			GetComponent<PlayerSetup>().playerUIInstance.SetActive(false);
		}

		Debug.Log(transform.name + " is DEAD!");

		StartCoroutine(Respawn());
	}

	private IEnumerator Respawn ()
	{

        if(life_Count>0){
            yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

            Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
            transform.position = _spawnPoint.position;
            transform.rotation = _spawnPoint.rotation;

            yield return new WaitForSeconds(0.1f);

            SetupPlayer();
        }
        else{
            PlayerPauseMenu.LeaveRoom();
        }


		Debug.Log(transform.name + " respawned.");
	}

    public void SetDefaults ()
    {
		isDead = false;

        currentHealth = maxHealth;

		//Enable the components
		for (int i = 0; i < disableOnDeath.Length; i++)
		{
			disableOnDeath[i].enabled = wasEnabled[i];
		}

		//Enable the gameobjects
		for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
		{
			disableGameObjectsOnDeath[i].SetActive(true);
		}

		//Enable the collider
		Collider _col = GetComponent<Collider>();
		if (_col != null)
			_col.enabled = true;

		//Create spawn effect
		GameObject _gfxIns = (GameObject)Instantiate(spawnEffect, transform.position, Quaternion.identity);
		Destroy(_gfxIns, 3f);
	}

}
