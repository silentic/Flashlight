using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapNode : MonoBehaviour 
{

	List<GameObject> linkedNode;
	int wallLayer;
	int wallMask;
	int nodeLayer;
	int nodeMask;
	int layerMask;

	// Use this for initialization
	void Start () 
	{
		linkedNode = new List<GameObject>();
		wallLayer = 9;
		wallMask = 1 << wallLayer;
		nodeLayer = 10;
		nodeMask = 1 << nodeLayer;

		layerMask = wallMask | nodeMask;
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
			linkedNode.Add(hit.collider.gameObject);
			Debug.DrawLine(transform.position,linkedNode[linkedNode.Count - 1].transform.position,Color.white,1000f);
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
