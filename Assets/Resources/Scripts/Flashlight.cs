using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour 
{
	public GameObject flashLightObject;
	Light2D flashLight;

	float battery;
	bool lightOn;
	public float consumeRate;
	public float regenRate;
	public bool isTouched;

	//UI 

	public GameObject batteryUIObject;
	RectTransform batteryUI;


	// Use this for initialization
	void Start () 
	{
		flashLight = flashLightObject.GetComponent<Light2D>();
		battery = 100;

		batteryUI = batteryUIObject.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
#if KEYBOARD
		if(Input.GetMouseButton(0))
#else
		if(isTouched)
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
