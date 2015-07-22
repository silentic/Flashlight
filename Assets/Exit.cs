using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour 
{
	int visible;

	void Start()
	{
		visible = 0;

		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Player")
		{
			Game.manager.gameOver();
		}
	}

	void OnDestroy()
	{		
		Light2D.UnregisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.UnregisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}

	void OnLightEnter(Light2D light, GameObject go)
	{
		if (go.GetInstanceID() == gameObject.GetInstanceID())
		{
			if(visible <= 0)
			{
				foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
				{
					renderer.enabled = true;
				}
				
			}
			visible += 1;
		}
	}

	void OnLightExit(Light2D light, GameObject go)
	{
		if (go.GetInstanceID() == gameObject.GetInstanceID())
		{
			visible -= 1;
			if(visible <= 0)
			{
				foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
				{
					renderer.enabled = false;
				}
			}
			
		}
	}


}
