using UnityEngine;
using System.Collections;

public class Enemy_01 : Enemy
{
	public float onLightSpeed = 3;
	public float normalSpeed = 5;

	protected override void Start()
	{
		base.Start();

		speed = normalSpeed;
	}

	protected override void stayFlashLightEffect()
	{
		speed = onLightSpeed;
	}

	protected override void exitLightEffect()
	{
		base.exitLightEffect();
		
		speed = normalSpeed;
	}

	void die()
	{
		Destroy (gameObject);
	}
}
