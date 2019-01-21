using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	[SerializeField]
	RectTransform thrusterFuelFill;

    [SerializeField]
    RectTransform healthBarFill;

    [SerializeField]
    Text ammoText;

    [SerializeField]
	GameObject pauseMenu;

    private Player player;
	private PlayerController controller;
    private WeaponManager weaponManager;

	public void SetPlayer (Player _player)
	{
		player = _player;
        controller = player.GetComponent<PlayerController>();
        weaponManager = player.GetComponent<WeaponManager>();
	}

	void Start ()
	{
		PauseMenu.IsOn = false;
	}

	void Update ()
	{
		SetFuelAmount (controller.GetThrusterFuelAmount());
        SetHealthAmount(player.GetHealthPct());
        SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TogglePauseMenu();
		}
	}

	public void TogglePauseMenu ()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
		PauseMenu.IsOn = pauseMenu.activeSelf;
    }

	void SetFuelAmount (float _amount)
	{
		thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
	}

    void SetHealthAmount(float _amount)
    {
        healthBarFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void SetAmmoAmount(float _amount)
    {
        ammoText.text = _amount.ToString();
    }
}
