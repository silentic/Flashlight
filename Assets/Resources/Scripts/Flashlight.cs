using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour 
{
	public GameObject flashLightObject;
	Light2D flashLight;
	
	bool lightOn;
	bool isTouched;
	public float consumeRate;
	public float regenRate;

	float battery;

	[Header("Hi Battery")]
	public float maxBattery;
	public float hiRadius;
	public Color hiColor;

	[Header("Mid Battery")]
	public float middleBatteryZone;
	public float middleBatteryRadius;
	public Color middleColor;

	[Header("Low Battery")]
	public float lowBatteryZone;
	public float lowBatteryRadius;
	public Color lowColor;


	[Header("UI")]
	public GameObject batteryUIObject;
	RectTransform batteryUITransform;
	Image batteryUIImage;
	



	// Use this for initialization
	void Start () 
	{
		flashLight = flashLightObject.GetComponent<Light2D>();
		batteryUITransform = batteryUIObject.GetComponent<RectTransform>();
		batteryUIImage = batteryUIObject.GetComponent<Image>();

		battery = maxBattery;
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
				//can't turn on when RED
				if(battery < lowBatteryZone) return;

				lightOn = true;

				//toggle on lost some battery
				battery = Mathf.Clamp(battery - 2 , 0 , 100);
			}

			updateLightBeam();
			battery = Mathf.Clamp(battery - consumeRate , 0 , 100);
		}

		else
		{
			flashLight.LightRadius = 0.001f;
			lightOn = false;

			if(battery < 100)
			{
				battery = Mathf.Clamp(battery + regenRate , 0 , 100);
			}
		}
		updateUI ();
	}

	void updateUI()
	{
		batteryUITransform.localScale = new Vector3(battery * 0.01f , 1 , 1);

		if(battery < lowBatteryZone)
		{
			batteryUIImage.color = Color.red;
		}
		else if(battery < middleBatteryZone)
		{
			batteryUIImage.color = Color.yellow;
		}
		else
		{
			batteryUIImage.color = Color.white;
		}
	}

	void updateLightBeam()
	{
		if(battery < lowBatteryZone)
		{
			flashLight.LightRadius = lowBatteryRadius;
		}
		else if(battery < middleBatteryZone)
		{
			flashLight.LightRadius = middleBatteryRadius;
		}
		else
		{
			flashLight.LightRadius = hiRadius;
		}
	}

	public void touch(bool t)
	{
		isTouched = t;
	}
}
