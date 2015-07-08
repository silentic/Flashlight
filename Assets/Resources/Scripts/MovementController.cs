//#define KEYBOARD

using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour 
{

	Vector3 origin;
	Vector3 targetDirection;
	Transform player;
	RectTransform rectTransform;
	bool isTouched;

	public Transform bg;

	float maxRange;

	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
		origin = rectTransform.position;
		player = Game.player.transform;

		float scale = GameObject.Find ("UI").GetComponent<Canvas>().scaleFactor;
		maxRange = bg.GetComponent<RectTransform>().rect.width*scale/2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if(Input.GetMouseButton(0))
		if(isTouched)
		{
			//move analog
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 0;
			targetDirection = (mousePosition - origin);
			if(targetDirection.magnitude > maxRange)
				targetDirection = targetDirection.normalized*maxRange;
			rectTransform.position = origin + targetDirection;
		
			//move player
			Vector3 movement = targetDirection/maxRange;		// percentage of analog move
			Game.player.GetComponent<PlayerControl>().move(movement);
		}
		else
		{
			rectTransform.position = origin;
		}
	}

	public void touchStart()
	{
		isTouched = true;
	}

	public void touchStop()
	{
		isTouched = false;
	}
}
