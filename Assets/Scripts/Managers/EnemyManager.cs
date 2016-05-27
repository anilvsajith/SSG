using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject[] enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
	public int count;


    void Start ()
    {
		count = 0;	
       	InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
		if (playerHealth.currentHealth <= 0f || count >= 20) {
			return;
		} else {
			count++;
		}

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		int enemyIndex = Random.Range (0, enemy.Length);
		Instantiate (enemy[enemyIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
