using UnityEngine;

[System.Serializable]
public class PlayerWeapon
{

    public string name = "Sodoff";

    public int damage = 75;  //helth and dmg are going to be calculated in integers
    public float range = 35f;
    //public int ammo = 2;
    public float fireRate = 1f;
    public GameObject graphics;
}
