using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {

    [SerializeField] Text text;
    PlayerShoot playerShoot;
    WeaponReloader vreloader;
	// Use this for initialization
	void Awake () {
        GameManager.Instance.OnLocalPlayerJoin += HandleOnLocalPlayerJoin;
	}
	
    void HandleOnLocalPlayerJoin(Player player)
    {
        playerShoot = player.GetComponent<PlayerShoot>();
        playerShoot.OnWeaponSwitch += HandleOnWeaponSwitch;
        // HandleOnAmmoChanged();
    }

    void HandleOnWeaponSwitch(Shooter activeWeapon)
    {
        vreloader = activeWeapon.reloader;
        vreloader.OnAmmoChanged += HandleOnAmmoChanged;
        HandleOnAmmoChanged();
    }
    void HandleOnAmmoChanged()
    {
        int amountInInventory = vreloader.RoundsRemainingInInventory;
        int amountInClip = vreloader.RoundsRemainingInClip;
        text.text = string.Format("{0}/{1}", amountInClip, amountInInventory);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
