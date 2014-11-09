using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CyalumeAudioBridge))]
public sealed class CyalumeAudioBridgeEditor : Editor
{
	#region [ Fold Out Flags ]
	private bool isBridgeFoldOut_        = true;
	private bool isAudioAnalyzerFoldOut_ = true;
	private bool isMaterialFoldOut_      = true;
	private bool isDefaultFoldOut_       = false;
	#endregion

	public override void OnInspectorGUI()
	{
		var bridge = target as CyalumeAudioBridge;

		isBridgeFoldOut_ = EditorGUILayout.Foldout(isBridgeFoldOut_, "Bridge Settings");
		if (isBridgeFoldOut_) {
			EditorGUI.indentLevel++; {
				DrawBridgeInspector();
			} EditorGUI.indentLevel--;
			EditorGUILayout.Separator();
		}

		isAudioAnalyzerFoldOut_ = EditorGUILayout.Foldout(isAudioAnalyzerFoldOut_, "Audio (Read-only)");
		if (isAudioAnalyzerFoldOut_) {
			EditorGUI.indentLevel++; {
				if (bridge.audioObject) {
					if (Application.isPlaying) {
						DrawAudioInspector();
					} else {
						EditorGUILayout.HelpBox("This field will be available in Playing Mode.", MessageType.None);
					}
				} else {
					EditorGUILayout.HelpBox("Please set Audio Object.", MessageType.Warning);
				}
			} EditorGUI.indentLevel--;
			EditorGUILayout.Separator();
		}

		isMaterialFoldOut_ = EditorGUILayout.Foldout(isMaterialFoldOut_, "Material");
		if (isMaterialFoldOut_) {
			EditorGUI.indentLevel++; {
				if (bridge.cyalumeObject) {
					if (Application.isPlaying) {
						DrawMaterialInspector();
					} else {
						EditorGUILayout.HelpBox("This field will be available in Playing Mode.", MessageType.None);
						EditorGUILayout.HelpBox("In Editor Mode, please use 'Cyalume' material inspector (it also appears in ParticleSystem inspector in Cyalume Object).", MessageType.Info);
					}
				} else {
					EditorGUILayout.HelpBox("Please set Cyalume Object.", MessageType.Warning);
				}
			} EditorGUI.indentLevel--;
			EditorGUILayout.Separator();
		}

		isDefaultFoldOut_ = EditorGUILayout.Foldout(isDefaultFoldOut_, "Default Inspector");
		if (isDefaultFoldOut_) {
			EditorGUI.indentLevel++; {
				DrawDefaultInspector();
			} EditorGUI.indentLevel--;
			EditorGUILayout.Separator();
		}
	}

	void DrawBridgeInspector()
	{
		var bridge = target as CyalumeAudioBridge;

		var syncAudio = EditorGUILayout.Toggle("Sync Audio", bridge.isControledByAudio);
		if (syncAudio != bridge.isControledByAudio) {
			bridge.isControledByAudio = syncAudio;
		}

		var cyalumeObject = EditorGUILayout.ObjectField("Cyalume Object", bridge.cyalumeObject, typeof(GameObject), true) as GameObject;
		if (cyalumeObject != bridge.cyalumeObject) {
			bridge.cyalumeObject = cyalumeObject;
		}

		var audioObject = EditorGUILayout.ObjectField("Audio Object", bridge.audioObject, typeof(GameObject), true) as GameObject;
		if (audioObject != bridge.audioObject) {
			bridge.audioObject = audioObject;
		}

		if (GUI.changed) {
			EditorUtility.SetDirty(bridge);
		}
	}

	void DrawAudioInspector()
	{
		var bridge = target as CyalumeAudioBridge;

		EditorGUILayout.FloatField("Volume", bridge.audioAnalyzer.volume);
	}

	void DrawMaterialInspector()
	{
		var bridge = target as CyalumeAudioBridge;

		var baseColor = EditorGUILayout.ColorField("Base Color", bridge.cyalume.baseColor);
		if (baseColor != bridge.cyalume.baseColor) {
			bridge.cyalume.baseColor = baseColor;
		}

		var waveX = EditorGUILayout.FloatField("Wave Factor X", bridge.cyalume.waveX);
		if (waveX != bridge.cyalume.waveX) {
			bridge.cyalume.waveX = waveX;
		}

		var waveZ = EditorGUILayout.FloatField("Wave Factor Z", bridge.cyalume.waveZ);
		if (waveZ != bridge.cyalume.waveZ) {
			bridge.cyalume.waveZ = waveZ;
		}

		var pitch = EditorGUILayout.FloatField("Pitch (wave/sec)", bridge.cyalume.wavePitch);
		if (pitch != bridge.cyalume.wavePitch) {
			bridge.cyalume.wavePitch = pitch;
		}
	}
}
