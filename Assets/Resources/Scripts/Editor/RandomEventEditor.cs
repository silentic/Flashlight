using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(RandomEventGenerator))]
public class RandomEventEditor : Editor
{
	bool showDefault;


	public override void OnInspectorGUI()
	{
		RandomEventGenerator randomEvent = (RandomEventGenerator)target;
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();
		showDefault = EditorGUILayout.Toggle("Default Editor",showDefault);
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Separator();
		if(!showDefault)
		{

			
			EditorGUILayout.LabelField("[Screen Ghost]",EditorStyles.boldLabel);
			randomEvent.ghostOnTheScreen = (GameObject)EditorGUILayout.ObjectField("Ghost Object",randomEvent.ghostOnTheScreen,typeof(GameObject),true);
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Prob",GUILayout.MaxWidth(50));
			randomEvent.ghostScreenProb = EditorGUILayout.Slider(randomEvent.ghostScreenProb, 0 , 100);
			EditorGUILayout.EndHorizontal();
			
			randomEvent.ghostScreenDuration = EditorGUILayout.FloatField("Duration",randomEvent.ghostScreenDuration);
			randomEvent.ghostScreenDelay = EditorGUILayout.FloatField("Delay" , randomEvent.ghostScreenDelay);
			
			EditorGUILayout.Space();

		}
		else
		{
			DrawDefaultInspector();
		}



		if(GUI.changed){ EditorUtility.SetDirty(randomEvent);}
	}
}
