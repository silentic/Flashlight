using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyNodeDetector : MonoBehaviour 
{
	Enemy mainObject;

	[HideInInspector]
	public GameObject lastVisitedNode;
	
	static int layerMask = Game.wallMask | Game.nodeMask;

	// Use this for initialization
	void Start () 
	{
		mainObject = transform.parent.GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		MapNode currentNode = collider.GetComponent<MapNode>();
		if(currentNode != null)
		{
			if(mainObject.chasingPlayer)
			{
				lastVisitedNode = currentNode.transform.parent.gameObject;
				return;
			}

			GameObject nextNode;
			nextNode = currentNode.getRandomLinkedNode(lastVisitedNode);
			mainObject.targetPosition = nextNode.transform.position;
			lastVisitedNode = currentNode.transform.parent.gameObject;
		}

	}
		
	public GameObject findNode()
	{
		List<GameObject> nodes = new List<GameObject>();
		//check and add to random pull
		GameObject temp;
		temp = checkNode(Vector2.up);
		if(temp != null) nodes.Add(temp);
		temp = checkNode(Vector2.down);
		if(temp != null) nodes.Add(temp);
		temp = checkNode(Vector2.left);
		if(temp != null) nodes.Add(temp);
		temp = checkNode(Vector2.right);
		if(temp != null) nodes.Add(temp);
		
		//return random node
		if(nodes.Count > 0)
		{
			int random = Random.Range(0,nodes.Count);
			return nodes[random];
		}
		else return null;
	}
	
	GameObject checkNode(Vector2 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position , direction , Mathf.Infinity , layerMask);
		if(hit.collider == null) return null;
		if(hit.collider.tag == "Node")
		{
			return hit.collider.transform.parent.gameObject;

		}
		return null;
	}


}
