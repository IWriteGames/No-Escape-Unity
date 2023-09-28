using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour
{
    private bool isCollected = false;
    private float TimeLifeObject = 60f;

    [SerializeField] private Weapon G18;
    [SerializeField] private Weapon MP18;

    private void Update() 
    {
        if(isCollected)
        {
            TimeLifeObject -= Time.deltaTime;
            if(TimeLifeObject <= 0f)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<Collider>().enabled = true;
                isCollected = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !isCollected)
        {
            G18.GetComponent<Weapon>().actualAmmoMagazine = G18.GetComponent<Weapon>().sizeMagazine;
            G18.GetComponent<Weapon>().actualAmmoReserve = G18.GetComponent<Weapon>().maxAmmo;

            MP18.GetComponent<Weapon>().actualAmmoMagazine = MP18.GetComponent<Weapon>().sizeMagazine;
            MP18.GetComponent<Weapon>().actualAmmoReserve = MP18.GetComponent<Weapon>().maxAmmo;

            PlayerController.Instance.UpdateWeaponInfoUI();

            isCollected = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
