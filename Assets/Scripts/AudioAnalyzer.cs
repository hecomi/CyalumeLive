using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (AudioSource))]
public class AudioAnalyzer : MonoBehaviour
{
	public int resolution = 1024;
	public bool isDrawDebugLine = false;

	private float[] spectrum_;
	public float[] spectrum {
		get { return spectrum_; }
	}

	public float volume {
		get; private set;
	}

	void Awake()
	{
		spectrum_ = new float[resolution];
	}

	void Update()
	{
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		volume = 0.0f;
		for (var i = 0; i < spectrum.Length; ++i) {
			volume += spectrum[i];
		}
		volume /= (resolution / 300.0f /* 適当... */);

		if (isDrawDebugLine) {
			for (var i = 0; i < spectrum.Length - 1; ++i) {
				float scaleX = 10.0f / resolution;
				float scaleY = 100.0f;
				Debug.DrawLine(new Vector3(i * scaleX, spectrum[i] * scaleY, 0), new Vector3((i + 1) * scaleX, spectrum[i + 1] * scaleY, 0), Color.red);
				Debug.DrawLine(new Vector3(i * scaleX, Mathf.Log(spectrum[i] * scaleY), 0), new Vector3((i + 1) * scaleX, Mathf.Log(spectrum[i + 1] * scaleY), 0), Color.blue);
			}
		}
	}
}
