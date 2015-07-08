#define KEYBOARD

using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour 
{
	Light2D flashLight;

	float battery;
	public float consumeRate;
	public float regenRate;
	bool lightOn;

	//UI 
	RectTransform batteryUI;


	// Use this for initialization
	void Start () 
	{
		flashLight = GetComponent<Light2D>();
		battery = 100;

		batteryUI = GameObject.FindGameObjectWithTag("Battery").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
#if KEYBOARD
		if(Input.GetMouseButton(0))
#endif
		{
			//our of battery
			if(battery <= 0)
			{
				//turn off
				if(lightOn)
				{
					flashLight.LightRadius = 0f;
					lightOn = true;
					updateUI ();
				}

				return;
			}

			//turn on light
			if(!lightOn)
			{
				flashLight.LightRadius = 5f;
				lightOn = true;
			}

			battery = Mathf.Clamp(battery - consumeRate , 0 , 100);
		
			updateUI();
		}

		else
		{
			flashLight.LightRadius = 0.001f;
			lightOn = false;

			if(battery < 100)
			{
				battery = Mathf.Clamp(battery + regenRate , 0 , 100);
				updateUI();
			}
		}
	}

	void updateUI()
	{
		batteryUI.localScale = new Vector3(battery * 0.01f , 1 , 1);
	}
}
