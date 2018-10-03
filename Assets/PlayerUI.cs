using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerUI : MonoBehaviour {

    [SerializeField]
    GameObject Scor;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Scor.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            Scor.SetActive(false);
        }
    }
}
