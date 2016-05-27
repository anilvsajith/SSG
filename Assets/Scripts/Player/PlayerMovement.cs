using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody m_Player;
	Animator m_Anim;
	Vector3 m_Movement;
	public float m_Speed = 5.0f;
	LayerMask m_LayerMask;
	float m_MaxCamLength = 100;

	void Awake(){
		m_LayerMask = LayerMask.GetMask ("Floor");
		m_Player = GetComponent<Rigidbody> ();
		m_Anim = GetComponent<Animator> ();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Move (h, v);
		Turn ();
		Moving_Animation (h,v);
	}

	void Move(float h, float v){
		m_Movement.Set (h, 0, v);
		m_Movement = m_Movement.normalized * m_Speed * Time.deltaTime;
		m_Player.MovePosition (m_Player.position + m_Movement);
	}

	void Turn()
	{
		Vector3 m_MousePosition = Input.mousePosition;
		Ray camray = Camera.main.ScreenPointToRay (m_MousePosition);
		RaycastHit result;
		Physics.Raycast (camray, out result, m_MaxCamLength,m_LayerMask);
		Vector3 m_RelPosition = result.point - m_Player.transform.position;
		Quaternion m_Rotate = Quaternion.LookRotation (m_RelPosition);
		m_Player.transform.rotation =m_Rotate;
	}

	void Moving_Animation(float h, float v){
		bool m_IsMoving = h != 0 || v != 0;
		m_Anim.SetBool ("Is_Moving", m_IsMoving);
	}
}
