using UnityEngine.Networking;
using UnityEngine;

public class WeponManager : NetworkBehaviour {

 
    [SerializeField]
    private string gunLayerName = "Gun";

    [SerializeField]
    private Transform weponHolder;

    [SerializeField]
    private PlayerWeapon priamaryWeapon;

    private PlayerWeapon currentWeapon;

	// Use this for initialization
	void Start () {
        EquipWeapon(priamaryWeapon);
	}

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    void EquipWeapon (PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponIns = (GameObject)Instantiate(_weapon.graphics, weponHolder.position, weponHolder.rotation);
        _weaponIns.transform.SetParent(weponHolder);
        if (isLocalPlayer)
            _weaponIns.layer = LayerMask.NameToLayer(gunLayerName);
    }
	
}
