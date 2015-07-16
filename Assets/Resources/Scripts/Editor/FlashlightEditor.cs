using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(Flashlight))]
public class FlashlightEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Flashlight flashlight = (Flashlight)target;
		Rect r;

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("[Battery]",EditorStyles.boldLabel);

		//Battery Color Bar
		EditorGUILayout.BeginHorizontal();
		float maxWidth = EditorGUIUtility.currentViewWidth;
		r = GUILayoutUtility.GetRect(1,10,GUIStyle.none,GUILayout.MaxWidth((flashlight.maxBattery - flashlight.middleBatteryZone)*maxWidth/flashlight.maxBattery));
		EditorGUI.DrawRect(r,flashlight.hiBatteryUIColor);
		r = GUILayoutUtility.GetRect(1,10,GUIStyle.none,GUILayout.MaxWidth((flashlight.middleBatteryZone - flashlight.lowBatteryZone)*maxWidth/flashlight.maxBattery));
		EditorGUI.DrawRect(r,flashlight.middleBatteryUIColor);
		r = GUILayoutUtility.GetRect(1,10,GUIStyle.none,GUILayout.MaxWidth((flashlight.lowBatteryZone)*maxWidth/flashlight.maxBattery));
		EditorGUI.DrawRect(r,flashlight.lowBatteryUIColor);

		EditorGUILayout.EndHorizontal();
		flashlight.maxBattery = EditorGUILayout.FloatField("MaxBattery", flashlight.maxBattery);
		flashlight.middleBatteryZone = EditorGUILayout.FloatField("MiddleBattery", flashlight.middleBatteryZone);
		flashlight.lowBatteryZone = EditorGUILayout.FloatField("LowBattery", flashlight.lowBatteryZone);
		flashlight.consumeRate = EditorGUILayout.FloatField("Consume/sec", flashlight.consumeRate);
		flashlight.regenRate = EditorGUILayout.FloatField("Regen/sec" , flashlight.regenRate);
		EditorGUILayout.LabelField("Time(C/R) = " + (flashlight.maxBattery / flashlight.consumeRate) + " / " + (flashlight.maxBattery / flashlight.regenRate).ToString("#.##") + " sec");

		EditorGUILayout.Space();

		EditorGUILayout.LabelField("[Light]",EditorStyles.boldLabel);
		EditorGUILayout.LabelField("High Battery");
		flashlight.hiRadius = EditorGUILayout.FloatField("Light Radius", flashlight.hiRadius);
		flashlight.hiColor = EditorGUILayout.ColorField("Color" , flashlight.hiColor);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Mid Battery");
		flashlight.middleBatteryRadius = EditorGUILayout.FloatField("Light Radius", flashlight.middleBatteryRadius);
		flashlight.middleColor = EditorGUILayout.ColorField("Color" , flashlight.middleColor);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Low Battery");
		flashlight.lowBatteryRadius = EditorGUILayout.FloatField("Light Radius", flashlight.lowBatteryRadius);
		flashlight.lowColor = EditorGUILayout.ColorField("Color" , flashlight.lowColor);

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("[UI]",EditorStyles.boldLabel);
		flashlight.batteryUIObject = (GameObject)EditorGUILayout.ObjectField("Object",flashlight.batteryUIObject,typeof(GameObject),true);
		EditorGUILayout.LabelField("Color");
		flashlight.hiBatteryUIColor = EditorGUILayout.ColorField("Hi Color" , flashlight.hiBatteryUIColor);
		flashlight.middleBatteryUIColor = EditorGUILayout.ColorField("Mid Color" , flashlight.middleBatteryUIColor);
		flashlight.lowBatteryUIColor = EditorGUILayout.ColorField("Low Color" , flashlight.lowBatteryUIColor);

		if(GUI.changed){ EditorUtility.SetDirty(flashlight);}
	}

}
