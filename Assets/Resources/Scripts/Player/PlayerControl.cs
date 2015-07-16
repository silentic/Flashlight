using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour 

{
	bool visionOn;
	public float visionRange;
	public static float playerVision;

	public float speed;
	
    Rigidbody2D playerRigidbody;
#if KEYBOARD
	Vector3 moveDirection;
	Vector3 rotateDirection;
#endif
	
	void Awake()
	{
		playerRigidbody = GetComponent<Rigidbody2D>();
		playerVision = visionRange;
#if KEYBOARD
		moveDirection = Vector3.zero;
		foreach(GameObject o in GameObject.FindGameObjectsWithTag("Controller"))
        {
			o.SetActive(false);
		}
#endif
	}

	void FixedUpdate () 
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
