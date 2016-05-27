using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
	public GameObject healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public GameObject gameOver;
	PlayerShooting playerShooting;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead = false;
    bool damaged;


    void Awake ()
	{ 
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
		playerShooting = GetComponentInChildren<PlayerShooting> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }

	void Update()
	{
		UpdateUI ();
		if (damaged) {
			playerAudio.Play ();
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage(int x){
		damaged = true;
		currentHealth -= x;

		
		if (currentHealth <= 0 && !isDead) {
			OnDeath ();
		}
	}

	void OnDeath()
	{
		isDead = true;
		gameOver.GetComponent<Animator> ().enabled = true;
		healthSlider.transform.FindChild ("Fill Area").gameObject.SetActive (false);
		playerMovement.enabled = false;
		playerShooting.enabled = false;
		anim.SetTrigger ("Is_Dead");
		playerAudio.clip = deathClip;
		playerAudio.Play ();

	}

	void UpdateUI(){
		healthSlider.GetComponent<Slider>().value = currentHealth;


	}
		
	

}
