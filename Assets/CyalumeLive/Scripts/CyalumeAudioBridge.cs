using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CyalumeAudioBridge : MonoBehaviour
{
	// target ParticleSystem-attached object
	// (ParticleSystem's material must be Cyalume)
	public GameObject cyalumeObject;
	private CyalumeController cyalume_;
	public CyalumeController cyalume {
		get { return cyalume_; }
	}

	// target AudioSource-attached object
	public GameObject audioObject;
	private AudioAnalyzer audio_;
	public AudioAnalyzer audioAnalyzer {
		get { return audio_; }
	}

	// if true, cyalume color and wave amplitude follow
	// the target audio source
	public bool isControledByAudio = true;

	// Frequency thresholds
	public float lowFrequencyThreshold  = 1000;
	public float midFrequencyThreshold  = 2000;
	public float highFrequencyThreshold = 3000;

	// Enhance paramter for each frequency color
	public float lowFreqColorEnhancer  = 1.0f;
	public float midFreqColorEnhancer  = 2.0f;
	public float highFreqColorEnhancer = 4.0f;

	// if the musicvolume becomes higher than this value,
	// then the wave direction becomes vertical from horizontal.
	public float audioVolumeThreshold = 0.5f;

	// Direction
	// (if isControledByAudio is true, these are automatically calculated.)
	public float horizontal = 1.0f;
	public float vertical   = 0.0f;

	// Filter paramters
	public float colorQuickness = 0.15f;
	public float waveAmplitudeQuickness = 0.02f;
	public float waveDirectionQuickness = 0.5f;

	void Start()
	{
		audio_ = audioObject.GetComponent<AudioAnalyzer>();
		if (audio_ == null) {
			audio_ = audioObject.AddComponent<AudioAnalyzer>();
		}

		cyalume_ = cyalumeObject.GetComponent<CyalumeController>();
		if (cyalume_ == null) {
			cyalume_ = cyalumeObject.AddComponent<CyalumeController>();
		}
	}

	void Update()
	{
		if (isControledByAudio) {
			UpdateWaveDirection();
			UpdateWaveAmplitude();
			UpdateWaveColor();
		}
	}

	void UpdateWaveDirection()
	{
		// lower  volume level : horizontal wave
		// higher volume level : vertical wave
		if (audio_.volume > audioVolumeThreshold) {
			horizontal += (0.0f - horizontal) * waveDirectionQuickness;
		} else {
			horizontal += (1.0f - horizontal) * waveDirectionQuickness;
		}
		vertical = 1 - horizontal;
	}

	void UpdateWaveAmplitude()
	{
		var targetWaveX = horizontal * audio_.volume * 2; // 2 is enhancer
		var targetWaveZ = vertical   * audio_.volume * 2;
		cyalume_.waveX += (targetWaveX - cyalume_.waveX) * waveAmplitudeQuickness;
		cyalume_.waveZ += (targetWaveZ - cyalume_.waveZ) * waveAmplitudeQuickness;
	}

	void UpdateWaveColor()
	{
		float low = 0.0f, mid = 0.0f, high = 0.0f;
		float sampleRate = AudioSettings.outputSampleRate;

		for (int i = 0; i < audio_.spectrum.Length; ++i) {
			var freq = sampleRate / audio_.resolution * i; // kHz
			if (freq < lowFrequencyThreshold) {
				low += audio_.spectrum[i];
			} else if (freq < midFrequencyThreshold) {
				mid += audio_.spectrum[i];
			} else if (freq < highFrequencyThreshold) {
				high += audio_.spectrum[i];
			}
		}

		Color target = new Color(
			high * highFreqColorEnhancer,
			mid  * midFreqColorEnhancer,
			low  * lowFreqColorEnhancer);

		var color = cyalume_.baseColor;
		color.r += (target.r - color.r) * colorQuickness;
		color.g += (target.g - color.g) * colorQuickness;
		color.b += (target.b - color.b) * colorQuickness;
		cyalume_.baseColor = color;
	}
}
