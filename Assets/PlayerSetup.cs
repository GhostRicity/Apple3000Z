using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour { 
    [SerializeField]
    Behaviour[] componetsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    GameObject playerUIprefab;
    private GameObject playerUIInstance;

    Camera sceanCamera;

    void Start ()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssingRemoteLayer();

        }
        else
        {   //disabel the screen camera
            sceanCamera = Camera.main;
             if (sceanCamera != null)
            {
                Camera.main.gameObject.SetActive(false);
            }

            playerUIInstance = Instantiate(playerUIprefab);
            playerUIInstance.name = playerUIprefab.name;
        }
        GetComponent<Player>().Setup();
    }

    public override void OnStartClient() //methond in net behavor class is calld when a client is set up localy
    {
        base.OnStartClient();
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        Manager.RegisterPlayer(_netID, _player); //error
        
    }


    void RegisterPlayer ()
    {
        string _ID = "Player" + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }
    void AssingRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void DisableComponents ()
    {
        for (int i = 0; i < componetsToDisable.Length; i++)
        {
            componetsToDisable[i].enabled = false;  //bug error
        }
    }

    void OnDisable()
    {
        Destroy(playerUIInstance);
        if(sceanCamera !=null)
        {
            sceanCamera.gameObject.SetActive(true);

            Manager.UnRegisterPlayer(transform.name);
        }
    }
}

