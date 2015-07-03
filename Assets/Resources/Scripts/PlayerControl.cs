#define KEYBOARD

using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{


	public float speed;

	Vector3 direction;
    Vector3 rotation;
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

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);
#endif
	}

	public void move (Vector3 direction)
	{
		playerRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
	}
}
