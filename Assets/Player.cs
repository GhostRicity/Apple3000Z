using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour {

    private bool _Dead = false;
    public bool Dead
    {
        get { return _Dead; }
        protected set { _Dead = value; }
    }
    

    [SerializeField]
    private int maxHP = 100;
    [SyncVar] //will be the saame on all player thats why we sync it fam
    private int curentHP;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void Setup ()  //set curent helt to max hp
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();
    }
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDMG(99999);
        }
    }
    [Client]

    public void RpcTakeDMG( int _amount)
    {
        if (Dead)
            return;

        curentHP -= _amount;

        Debug.Log(transform.name + "new has" + curentHP + "Health.");  //only on local host for testing

        if ( curentHP <= 0 )
        {
            Die();
        }
    }

    //internal void RpcTakeDMG(int damage, string sourceID)
    //{
    //    throw new NotImplementedException();
    //}

    private void Die()
    {
        Dead = true;
        //disabel conponents
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        
            _col.enabled = false;
        
             Debug.Log(transform.name + "is DEAD");

        //call respan method
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn ()
    {
        //yield return new WaitForSeconds(Manager.ins.GameSetings.respawnTime);
        yield return new WaitForSeconds(3f);

        SetDefaults();
        Transform _spwan = NetworkManager.singleton.GetStartPosition();
        transform.position = _spwan.position;
        transform.rotation = _spwan.rotation;

        Debug.Log(transform.name + " is alive.");
    }

    public void SetDefaults ()
    {
        Dead = false;
        curentHP = maxHP;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if(_col !=null)
        {
            _col.enabled = true;
        }

    }


}

