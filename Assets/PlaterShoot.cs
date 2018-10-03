using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeponManager))]
public class PlaterShoot : NetworkBehaviour {

    private const string PLAYER_TAG = "Player";

    
    private  PlayerWeapon currentWeapon;
    private WeponManager weaponManager;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LayerMask mask;

    void Start ()
    {
        if (cam == null )
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }
        weaponManager = GetComponent<WeponManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    void PlayShoot()
    {
        _audioSource = GameObject.Find(name: "Player").GetComponent<AudioSource>(); //.find so usefule
        Play();
    }

    private void Play()
    {
        _audioSource.Play();
    }

    void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (Input.GetButtonDown("Fire1"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                
            }
            Play();
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
        
    }

    [Client]
	void Shoot ()
	{
        Debug.Log("Pew pew");

		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask) )
		{
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerHit(_hit.collider.name,  currentWeapon.damage);
            }
            
        }
        
    }

    [Command]
    void CmdPlayerHit (string _playerID, int _damage)
    {
        Debug.Log(_playerID + "has taken A Hit");

        Player _player = Manager.GetPlayer(_playerID);
        _player.RpcTakeDMG(_damage);
    }
	
}
