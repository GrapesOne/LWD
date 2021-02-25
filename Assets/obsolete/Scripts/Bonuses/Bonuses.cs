using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Sprites;
public class Bonuses : MonoBehaviour {
	private ParticleSystem Particle;
	private SpriteRenderer Renderer;
	private AudioSource Audio;
	private float Multi=2, TimeToDead=0.3f,EndDisappearance=0.05f;
	protected bool Tink = true;

	void Start()
	{
		Audio =  gameObject.GetComponent<AudioSource> ();
		Particle = gameObject.GetComponent<ParticleSystem> ();
		Renderer = gameObject.GetComponent<SpriteRenderer> ();
		AnotherStart ();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Bonus")
		{
			if (Tink)
			{
				ActBonus();
				if (Particle)
					Particle.Play();
				if (Audio)
					Audio.Play();
				if (Renderer)
					ImageDesappearanceAndDestoy();
			}
			Tink = false;
		}
	}
	async UniTaskVoid ImageDesappearanceAndDestoy()
	{
		float MinusA = Renderer.color.a/Multi;
		for (int i = 0; i < Multi*2; i++) {
			await UniTask.Delay((int)((TimeToDead-EndDisappearance) / Multi*1000));
			Renderer.color = new Color (Renderer.color.r, Renderer.color.g, Renderer.color.b, Renderer.color.a - MinusA);
		}
	
	}

	void OnEnable()
	{
		StopAllCoroutines ();
		if (Renderer)
			Renderer.color = new Color (Renderer.color.r, Renderer.color.g, Renderer.color.b, 1f);
		if (Particle)
			Particle.Stop ();
		if (Audio)
			Audio.Stop ();
		AnotherEnable ();
		Tink = true;
	}
	async UniTaskVoid  Timer(){
		await UniTask.Delay((int)(TimeToDead*1000));
		PoolManager.putGameObjectToPool (gameObject);
	}


	protected virtual void ActBonus()
	{
	}
	protected virtual void AnotherStart()
	{
	}
	protected virtual void AnotherEnable()
	{
	}
}
