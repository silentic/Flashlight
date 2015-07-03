//#define KEYBOARD

using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{


	public float speed;

	Vector3 direction;
	Rigidbody2D playerRigidbody;

	
	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody2D>();
		direction = Vector3.zero;
	}

	void Update () 
	{
#if KEYBOARD
		direction.Set(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
		direction.z = 0;
		move (direction);
#endif
	}

	public void move (Vector3 direction)
	{
		playerRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
	}
}
