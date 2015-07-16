using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour 
{
	Light2D flashLight;
	
	bool lightOn;
	bool isTouched;

	public float consumeRate;
	public float regenRate;
	float turnOnPenalty;

	float battery;

	[Header("[Flashlight]")]
	public float maxBattery;
	public float hiRadius;
	public Color hiColor;
	[Space (10)]
	public float middleBatteryZone;
	public float middleBatteryRadius;
	public Color middleColor;
	[Space (10)]
	public float lowBatteryZone;
	public float lowBatteryRadius;
	public Color lowColor;


	[Header("[Battery UI]")]
	public GameObject batteryUIObject;
	public Color hiBatteryUIColor;
	public Color middleBatteryUIColor;
	public Color lowBatteryUIColor;

	RectTransform batteryUITransform;
	Image batteryUIImage;




	// Use this for initialization
	void Start () 
	{
		flashLight = transform.FindChild("Flashlight").GetComponent<Light2D>();
		batteryUITransform = batteryUIObject.GetComponent<RectTransform>();
		batteryUIImage = batteryUIObject.GetComponent<Image>();

		turnOnPenalty = Mathf.Clamp( (regenRate - consumeRate)/2 , 0 , regenRate );

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
				battery = Mathf.Clamp(battery - turnOnPenalty , 0 , 100);
			}

			updateLightBeam();
			battery = Mathf.Clamp(battery - consumeRate*Time.fixedDeltaTime , 0 , 100);
		}

		else
		{
			flashLight.LightRadius = 0.001f;
			lightOn = false;

			if(battery < 100)
			{
				battery = Mathf.Clamp(battery + regenRate*Time.fixedDeltaTime , 0 , 100);
			}
		}
		updateUI ();

#if MAXBATTERY
		battery = maxBattery;
#endif
	}

	void updateUI()
	{
		batteryUITransform.localScale = new Vector3(battery * 0.01f , 1 , 1);

		if(battery < lowBatteryZone)
		{
			batteryUIImage.color = lowBatteryUIColor;
		}
		else if(battery < middleBatteryZone)
		{
			batteryUIImage.color = middleBatteryUIColor;
		}
		else
		{
			batteryUIImage.color = hiBatteryUIColor;
		}
	}

	void updateLightBeam()
	{
		if(RandomLightOff.blackOut)
		{
			flashLight.LightRadius = 0f;
			return;
		}

		if(battery < lowBatteryZone)
		{
			flashLight.LightRadius = lowBatteryRadius;
			flashLight.LightColor = lowColor;
		}
		else if(battery < middleBatteryZone)
		{
			flashLight.LightRadius = middleBatteryRadius;
			flashLight.LightColor = middleColor;
		}
		else
		{
			flashLight.LightRadius = hiRadius;
			flashLight.LightColor = hiColor;
		}
	}


	public void touch(bool t)
	{
		isTouched = t;
	}
}
