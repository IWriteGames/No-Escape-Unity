using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private bool isCollected = false;
    private float TimeLifeObject = 90f;

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
            PlayerHealthController.Instance.HealPlayer();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
            isCollected = true;
        }
    }


}
