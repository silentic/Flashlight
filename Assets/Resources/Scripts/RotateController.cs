using UnityEngine;
using System.Collections;

public class RotateController : MonoBehaviour 
{

	Vector3 origin;
	Vector3 targetDirection;
	Transform player;
	RectTransform rectTransform;
	
	// Use this for initialization
	void Start () 
	{
		rectTransform = GetComponent<RectTransform>();
		origin = rectTransform.position;
		player = Game.player.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(0))
		{
			//move analog
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.z = 0;
			targetDirection = (mousePosition - origin).normalized;
			rectTransform.position = origin + targetDirection * 25;
			
			//move player
			Game.player.GetComponent<PlayerControl>().turn(targetDirection);
		}
		else
		{
			rectTransform.position = origin;
		}
	}
}
