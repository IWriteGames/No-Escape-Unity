using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    public float moveSpeed;
    public float lifeTime;

    [SerializeField] private int damage;

    public Rigidbody theRB;

    public GameObject impactEffectZombie;
    public GameObject impactEffect;


    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().DamageEnemy(damage);
            Instantiate(impactEffectZombie, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);

        } else {
            Destroy(gameObject);
            Instantiate(impactEffect, transform.position + (transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
        }


    }

}
