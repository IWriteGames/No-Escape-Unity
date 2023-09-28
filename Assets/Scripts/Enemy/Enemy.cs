using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator enemyAnimator;

    public int enemyDamage;
    public int health;

    private GameObject player;

    public bool playerInReach;
    public float attackDelayTimer;
    public float howMuchEarlierStartAttackAnim;
    public float delayBetweenAttacks; 

    public UnityEngine.AI.NavMeshAgent NavMeshAgent;

    //Audio
    public AudioClip zombieGrab;
    public AudioClip zombieBurp;
    public AudioClip zombieSnarl;

    public AudioSource zombieAudioSource;

    private void Awake() 
    {
        zombieAudioSource = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        howMuchEarlierStartAttackAnim = 1f;
        delayBetweenAttacks = 0.5f;
        InvokeRepeating("SoundZombies", 1f, UnityEngine.Random.Range(10, 20));
    }

    void Update()
    {        
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = player.transform.position;        
    }

    private void SoundZombies()
    {
        if(UnityEngine.Random.Range(0, 2) == 0)
        {
            zombieAudioSource.PlayOneShot(zombieSnarl, 1);
        } else {
            zombieAudioSource.PlayOneShot(zombieBurp, 1);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInReach = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (playerInReach)
        {
            attackDelayTimer += Time.deltaTime;

            if (attackDelayTimer >= delayBetweenAttacks - howMuchEarlierStartAttackAnim && attackDelayTimer <= delayBetweenAttacks)
            {
                enemyAnimator.SetTrigger("AttackPlayer");
            }

            if (attackDelayTimer >= delayBetweenAttacks)
            {
                player.GetComponent<PlayerHealthController>().DamagePlayer(enemyDamage);
                StartCoroutine(UIManager.Instance.FadeAway());
                zombieAudioSource.PlayOneShot(zombieGrab, 1);
                attackDelayTimer = 0;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInReach = false;
            attackDelayTimer = 0;
        }
    }

    public void DamageEnemy(int damage)
    {

        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
            ZombieSpawner.Instance.enemiesAlive--;
            ZombieSpawner.Instance.CheckRounds();
            GameManager.Instance.totalEnemiesDefeat++;
        }
    }
}
