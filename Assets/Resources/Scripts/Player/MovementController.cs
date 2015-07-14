using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{

	Vector3 origin;
	Vector3 targetDirection;
	Transform player;
	RectTransform rectTransform;
	bool isTouched;
	int fingerID;

	public Transform bg;

	float maxRange;

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
		origin = rectTransform.position;
		player = Game.player.transform;
		fingerID = -1;

		maxRange = bg.GetComponent<RectTransform>().rect.width*Game.UIScale/2;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(isTouched)
		{
			Debug.Log(fingerID);
	
			//move analog
			{
#if KEYBOARD
				Vector3 mousePosition = Input.mousePosition;
#else
				Vector3 mousePosition = Input.GetTouch(fingerID).position;
#endif

				mousePosition.z = 0;
				targetDirection = (mousePosition - origin);
				if(targetDirection.magnitude > maxRange)
					targetDirection = targetDirection.normalized*maxRange;
				rectTransform.position = origin + targetDirection;
			}

			//move player
			Vector3 movement = targetDirection/maxRange;		// percentage of analog move
			player.GetComponent<PlayerControl>().move(movement);
		}
		else
		{
			rectTransform.position = origin;
		}
	}

	public void touchStart()
	{
		isTouched = true;

#if !KEYBOARD
		foreach(Touch t in Input.touches)
		{
			if(t.phase == TouchPhase.Began)
			{
				fingerID = t.fingerId;
				return;
			}
		}
#endif
	}

	public void touchStop()
	{
		isTouched = false;

		fingerID = -1;
	}
}
