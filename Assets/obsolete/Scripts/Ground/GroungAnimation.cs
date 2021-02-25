using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GroungAnimation : MonoBehaviour {
	
	SpriteRenderer Renderer;
	GroundAnimator Animator;
	ParticleSystem Particle;

	float MaxSpeed = 3f, MinSpeed = 0.2f;
	public LayerMask PlayerMask = 9;
	public int NowSprite = 0, id = 0 ;
	public float Multi = 2, TimeToDead = 0.8f,EndDisappearance = 0.5f;

	private int  step, bound;
	private float time;
	private bool InCollision, busy;

	private CancellationTokenSource _tokenSource;

	void ChangeSpeed()
	{
		if (Animator == null) return;
		MaxSpeed = 0.01f * Animator.MaxFactor;
		MinSpeed = 0.01f * Animator.MinFactor;
		bound = Animator.Sets[Animator.NowSet].Set.Length;
		NowSprite = Random.Range(0, bound);
		step = Random.Range(1, 4);
	}

	void OnDisable()
	{
		_tokenSource?.Cancel();
		GroundAnimator.FactorChanged -= ChangeSpeed;
	}
	void OnEnable()
	{
		busy = true;
		if (Renderer == null) {
			Renderer = GetComponent<SpriteRenderer> ();
			Animator = GroundAnimator.Instance;
			Particle = GetComponent<ParticleSystem> ();
		}
		GroundAnimator.FactorChanged += ChangeSpeed;
		ChangeSpeed();
	}

	void OnBecameVisible()
	{
		_tokenSource = new CancellationTokenSource();
		ChangeView (_tokenSource.Token);
		InCollision = false;
	}

	void OnBecameInvisible()
	{
		_tokenSource.Cancel();
		InCollision = true;
	}
	void OnTriggerEnter2D(Collider2D collider2D)
	{
		if(InCollision) return;
		InCollision = collider2D.gameObject.layer == PlayerMask;
	}
	void OnTriggerExit2D(Collider2D collider2D)
	{
		InCollision = false;
	}
	async UniTaskVoid ChangeView(CancellationToken token)
	{
		await UniTask.WaitWhile(() => Animator == null);
		while (!token.IsCancellationRequested)
		{
			await UniTask.Delay((int)(time*1000));
			await UniTask.WaitWhile(() => InCollision);
			NowSprite = (NowSprite + step) % bound ;
			time = (MaxSpeed + (float)NowSprite/40 + MinSpeed) / 3;
			Renderer.sprite = Animator.Sets[Animator.NowSet].Set[NowSprite];
		}
	}

	public void Deleting()
	{
		Particle.Play ();
		ImageDesappearance();

	}

	async UniTaskVoid ImageDesappearance()
	{
		var col = Renderer.color;
		var MinusA = col.a / Multi;
		for (var i = 0; i < Multi; i++) {
			await UniTask.Delay((int)((TimeToDead - EndDisappearance) / Multi*1000));
			col.a -= MinusA;
			Renderer.color = col;
		}
		await UniTask.Delay(100);
		PoolManager.putGameObjectToPool(gameObject);
		col.a = 1;
		Renderer.color = col;
		
	}

}
