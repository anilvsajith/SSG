using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
	public Canvas HealthCanvas;
	public Slider HealthBar;
	public float startingHealth = 100;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
	EnemyMovement enemyMovement; 
    Animator anim;
    AudioSource enemyAudio;
    public ParticleSystem hitParticles;
	public ParticleSystem deathParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake ()
	{
		enemyMovement = GetComponent<EnemyMovement> ();
		isSinking = false;
		isDead = false;
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        //hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
		currentHealth = startingHealth;
		HealthBar.value = (currentHealth/startingHealth)*100;
    }
		
	void Update(){
		
		HealthCanvas.transform.rotation = Quaternion.Euler(0,0,0);
		if (isSinking) {
			transform.Translate (Vector3.up * -sinkSpeed * Time.deltaTime);
			Destroy (gameObject, 2);
		}

	}
	public void TakeShotDamage(int amount, Vector3 point)
	{
		if (!isDead) {
			hitParticles.transform.position = point;
			hitParticles.Play ();
			currentHealth -= amount;
			HealthBar.value = (currentHealth/startingHealth)*100;
			enemyAudio.Play ();
			if (currentHealth <= 0) {
				Ondeath ();
			}
		}
			
	}
		
	void Ondeath()
	{	
		HealthCanvas.enabled = false;
		FindObjectOfType<EnemyManager> ().count--;
		FindObjectOfType<ScoreManager> ().score += scoreValue;
		isDead = true;
		GetComponent<NavMeshAgent> ().enabled = false;
		enemyMovement.enabled = false;
		enemyAudio.clip = deathClip;
		enemyAudio.Play ();
		gameObject.layer = 0;
		deathParticles.Play ();
		anim.SetTrigger ("IsDead");

	}
	void StartSinking()
	{
		capsuleCollider.isTrigger = true;
		isSinking = true;
	}

    
}
