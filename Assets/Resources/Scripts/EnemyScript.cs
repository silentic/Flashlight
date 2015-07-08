using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour 
{
	SpriteRenderer enemyRenderer;
	int visible;
	// Use this for initialization
	void Start () 
	{
		enemyRenderer = GetComponent<SpriteRenderer>();
		visible = 0;

		Light2D.RegisterEventListener(LightEventListenerType.OnEnter, OnLightEnter);
		Light2D.RegisterEventListener(LightEventListenerType.OnExit, OnLightExit);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnLightEnter(Light2D _light, GameObject _go)
	{
		if (_go.GetInstanceID() == gameObject.GetInstanceID())
		{
			if(visible <= 0)
			{
				Debug.Log("Object Entered Light");

				enemyRenderer.enabled = true;
			}
			visible += 1;
		}
	}

	void OnLightExit(Light2D _light, GameObject _go)
	{
		if (_go.GetInstanceID() == gameObject.GetInstanceID())
		{
			visible -= 1;
			if(visible <= 0)
			{
				Debug.Log("Object Exited Light");

				enemyRenderer.enabled = false;
			}

		}
	}
}

