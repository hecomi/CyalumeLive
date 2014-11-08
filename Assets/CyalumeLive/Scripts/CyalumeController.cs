using UnityEngine;
using System.Collections;

public class CyalumeController : MonoBehaviour
{
	private ParticleSystem particleSystem_;

	public Color baseColor {
		get { return renderer.material.GetColor("_BaseColor"); }
		set { renderer.material.SetColor("_BaseColor", value); }
	}

	public float waveX {
		get { return renderer.material.GetFloat("_WaveFactorX"); }
		set { renderer.material.SetFloat("_WaveFactorX", value); }
	}

	public float waveZ {
		get { return renderer.material.GetFloat("_WaveFactorZ"); }
		set { renderer.material.SetFloat("_WaveFactorZ", value); }
	}

	public float waveCorrection {
		get { return renderer.material.GetFloat("_WaveCorrection"); }
		set { renderer.material.SetFloat("_WaveCorrection", value); }
	}

	public float delayByDistance {
		get { return renderer.material.GetFloat("_Delay"); }
		set { renderer.material.SetFloat("_Delay", value); }
	}

	public float wavePitch {
		get { return renderer.material.GetFloat("_Pitch"); }
		set { renderer.material.SetFloat("_Pitch", value); }
	}

	public float bend {
		get { return renderer.material.GetFloat("_Bend"); }
		set { renderer.material.SetFloat("_Bend", value); }
	}

	public Texture texture {
		get { return renderer.material.GetTexture("_MainTex"); }
		set { renderer.material.SetTexture("_MainTex", value); }
	}

	public Color startColor {
		get { return particleSystem_.startColor; }
		set { particleSystem_.startColor = value; }
	}

	void Start()
	{
		particleSystem_ = GetComponent<ParticleSystem>();
	}
}
