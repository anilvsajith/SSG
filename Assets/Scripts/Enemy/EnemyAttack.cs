using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.Find ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
		if (other.gameObject.tag == "Player") {
			playerInRange = true;
		}

    }


    void OnTriggerExit (Collider other)
    {
		playerInRange = false;
    }


    void Update ()
    {
		
		if (timer >= timeBetweenAttacks && playerInRange && playerHealth.currentHealth >= 0 && enemyHealth.currentHealth>0) {
			timer = 0;
			Attack ();
		} else if (playerHealth.currentHealth <= 0) {
			GetComponent<EnemyMovement> ().enabled = false;
			GetComponent<NavMeshAgent> ().enabled = false;
			anim.SetTrigger ("PlayerDead");		
		} else {
			timer += Time.deltaTime;
		}
			
    }


    void Attack ()
    {
		playerHealth.TakeDamage (attackDamage);
    }
}
