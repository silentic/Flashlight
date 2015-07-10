using UnityEngine;
using System.Collections;

public class Enemy_02 : Enemy
{
	Color originalColor;
	Color enemyColor;

	protected override void Start () 
	{
		base.Start();

		originalColor = enemyRenderer.color;
		enemyColor = originalColor;
	}

	protected override void enterLightEffect()
	{
		base.enterLightEffect();
	}


	protected override void exitLightEffect()
	{
		base.exitLightEffect();
		hp = maxHp;
		enemyColor = originalColor;

	}

	protected override void stayFlashLightEffect()
	{
		hp -= 0.1f;
		enemyColor.a -= 0.01f;
		enemyRenderer.color = enemyColor;

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


