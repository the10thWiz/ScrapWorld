using UnityEngine;
using UnityEditor;
using System.IO;

public class CutsceneEditor : EditorWindow {
	
	string SaveTo = "Assets/Scripts";
	string FileName;
	string Data = "using System.Collections; using System.Collections.Generic; using UnityEngine; public class ";
	string AddData;
	string CustomVarables;
	string functions;
	string varables;
	int CutsceneCount = 1;
	int FrameNumber;
	
	
	[MenuItem("Window/Cutscene/CutsceneEditor")]
	
	
	public static void ShowWindow () {
		GetWindow<CutsceneEditor>("CutsceneEditor");
	}

	void OnGUI () {
		GUILayout.Label("Welcome to Cutscene Editor! Made by kotek900", EditorStyles.boldLabel);
		
		SaveTo = EditorGUILayout.TextField("Save to: ", SaveTo);
		FileName=EditorGUILayout.TextField("File name: ", FileName);
		varables=EditorGUILayout.TextField("All varables: ", varables);
		CutsceneCount = EditorGUILayout.IntField("Cutscene Number: ", CutsceneCount);
		FrameNumber = EditorGUILayout.IntField("Run function on frame: ", FrameNumber);
		AddData = EditorGUILayout.TextField("Function: ", AddData);
		
		if(GUILayout.Button("Add function")) {
			functions=functions+" if (Cutscene.Count=="+CutsceneCount+"&&Cutscene.Timer=="+FrameNumber+") "+AddData;
		}
		
		if(GUILayout.Button("Remove all functions")) {
		functions="";
		}
		
		 if(GUILayout.Button("Save")) {
			 Save();
		 }
		
	}
	void Save() {
	File.WriteAllText(SaveTo+"/"+FileName+".cs", Data+FileName+" : MonoBehaviour { "+varables+"public Cutscene Cutscene; void Update () { if (Cutscene.InCutscene) {"+functions+"}}}");
	}
	
}
