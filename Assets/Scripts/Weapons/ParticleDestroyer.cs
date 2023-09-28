using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyParticle());        
    }

    private IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
