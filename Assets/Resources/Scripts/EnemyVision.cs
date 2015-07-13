using UnityEngine;
using System.Collections;

public class EnemyVision : MonoBehaviour 
{
	Enemy mainObject;

	public float viewAngle;
	float sightRadius;

	int layerMask;
	

	// Use this for initialization
	void Start () 
	{
		mainObject = transform.parent.GetComponent<Enemy>();
		sightRadius = GetComponent<CircleCollider2D>().radius;

		layerMask = ~Game.nodeMask;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay2D(Collider2D otherCollider)
	{
		if(otherCollider.tag == "Player")
		{
			GameObject player = otherCollider.gameObject;
			Vector3 ray = player.transform.position - transform.position;
			ray.Set(ray.x , ray.y , 0);
			RaycastHit2D hit = Physics2D.Raycast(transform.position , ray.normalized , ray.magnitude , layerMask);
			if(hit.collider.tag == "Player")
			{
				if(Vector3.Angle(mainObject.transform.right , ray) < viewAngle/2)
				{
					Debug.Log("see");
				}
			}
		}
	}
}
