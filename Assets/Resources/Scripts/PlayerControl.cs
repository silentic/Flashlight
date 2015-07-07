#define KEYBOARD

using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{


	public float speed;

	Vector3 moveDirection;
    Vector3 rotateDirection;
    Rigidbody2D playerRigidbody;

	
	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody2D>();
		moveDirection = Vector3.zero;
	}

	void Update () 
	{
#if KEYBOARD
		moveDirection.Set(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0);
		moveDirection.z = 0;
		move (moveDirection);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		rotateDirection = mousePos - transform.position;
		rotateDirection.z = 0;
		turn (rotateDirection);
#endif
	}

	public void move (Vector3 direction)
	{
		playerRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
	}

	public void turn (Vector3 direction)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
	}
}
