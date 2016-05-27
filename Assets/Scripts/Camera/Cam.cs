using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public Transform m_Player;
	Vector3 m_Offset;
	// Use this for initialization
	void Start () {
		m_Offset = transform.position - m_Player.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 pos = m_Player.position + m_Offset;
		transform.position = Vector3.Lerp (transform.position, pos, 5*Time.deltaTime);
	}
}
