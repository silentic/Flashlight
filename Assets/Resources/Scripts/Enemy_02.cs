using UnityEngine;
using System.Collections;

public class Enemy_02 : Enemy
{


	protected override void enterLightEffect()
	{
		base.enterLightEffect();

	}


	protected override void exitLightEffect()
	{
		base.exitLightEffect();
		hp = maxHp;
	}

	protected override void stayFlashLightEffect()
	{
		hp -= 0.1f;
		if(hp <= 0)
		{
			die ();
		}
	}

	void die()
	{
		Destroy (gameObject);
	}
}
