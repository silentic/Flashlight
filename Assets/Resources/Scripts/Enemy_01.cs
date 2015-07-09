using UnityEngine;
using System.Collections;

public class Enemy_01 : Enemy
{


	protected override void enterLightEffect()
	{
		base.enterLightEffect();

	}


	protected override void exitLightEffect()
	{
		base.exitLightEffect();
		
		die ();
	}

	void die()
	{
		Destroy (gameObject);
	}
}
