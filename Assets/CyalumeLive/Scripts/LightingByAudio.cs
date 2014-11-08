using UnityEngine;
using System.Collections;

public class LightingByAudio : MonoBehaviour
{
	public GameObject cyalumeObject;
	private CyalumeAudioBridge bridge_;
	private Light light_;

	void Start()
	{
		bridge_ = cyalumeObject.GetComponent<CyalumeAudioBridge>();
		light_ = GetComponent<Light>();
	}

	void Update()
	{
		if (bridge_ == null || light == null) { return; }

		light_.color = bridge_.cyalume.baseColor;
	}
}
