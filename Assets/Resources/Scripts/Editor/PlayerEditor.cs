using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(PlayerControl))]
public class PlayerEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		PlayerControl player = (PlayerControl)target;

		player.speed = EditorGUILayout.FloatField("Move Speed",player.speed);
		player.visionRange = EditorGUILayout.FloatField("Vision Range",player.visionRange);
		updateVision(player.visionRange);

		if(GUI.changed){ EditorUtility.SetDirty(player);}
	}

	public void updateVision(float value)
	{
		Light2D temp = GameObject.Find ("Vision").GetComponent<Light2D>();
		temp.LightRadius = value;
	}
}
