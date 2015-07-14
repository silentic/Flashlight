using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapNode : MonoBehaviour 
{

	List<GameObject> linkedNode;
	static int layerMask = Game.wallMask | Game.nodeMask;

	// Use this for initialization
	void Start () 
	{
		linkedNode = new List<GameObject>();

		//layerMask = Game.wallMask | Game.nodeMask;
		checkLinkedNode(Vector2.up);
		checkLinkedNode(Vector2.down);
		checkLinkedNode(Vector2.left);
		checkLinkedNode(Vector2.right);

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void checkLinkedNode(Vector2 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position , direction, Mathf.Infinity , layerMask);
		if(hit.collider.tag == "Node")
		{
			GameObject node = hit.collider.transform.parent.gameObject;
			linkedNode.Add(node);
			Debug.DrawLine(transform.position,node.transform.position,Color.white,1000f);
		}
	}

	public GameObject getRandomLinkedNode(GameObject tryExcludeNode)
	{
		GameObject node;
		while(true)
		{
			int random = Random.Range(0,linkedNode.Count);
			node = linkedNode[random];

			//1 node = forced return
			if(linkedNode.Count == 1)
				return node;

			//exclude visited node
			if(node != tryExcludeNode)
				return node;
		}
		return null;
	}

}
