using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 10.00f;
    public float range = 100f;

	EnemyHealth enemy;
    float timer;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
//    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
		gunParticles = GetComponentInChildren <ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();

    }

	void Update()
	{
		timer += Time.deltaTime;
		if (Input.GetMouseButtonDown (0) && (timer>timeBetweenBullets)) {
			timer = 0;	
			shootRay = new Ray (transform.position, transform.forward);
			RaycastHit result;
			if (Physics.Raycast (shootRay, out result, range, shootableMask)) {
				gunLine.SetPosition (1, result.point);
				enemy = result.collider.GetComponent<EnemyHealth> ();
				if (enemy) {
					enemy.TakeShotDamage (25,result.point);
				}
			} else {
				gunLine.SetPosition (1, transform.position + transform.forward * range);
			}			
			gunLight.enabled = true;
			gunLine.enabled = true;
			gunParticles.Play ();
			gunAudio.Play ();
			gunLine.SetPosition (0, transform.position);
			
		}
		if (timer >= 0.05f && (gunLight.enabled || gunLine.enabled)) {
			gunLight.enabled = false;
			gunLine.enabled = false;
		}		
	}



}
