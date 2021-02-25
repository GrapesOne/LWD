using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour {
	[Space]
	public bool Grounded;
	private bool wasPlayed, checkGround;
	public int Force;
	[Space]
	public LayerMask WhatIsGround;
	public Transform GroundCheck;
	public float GroundRadius;
	[Space]
	private Rigidbody2D BodyPhysic;
	private Transform ObjectTransform;
	private Trajectory Way;
	public GameObject Count;
	Counters CountManager;
	[Space]
	ParticleSystem  Particle;
	[Space]
	AudioSource Audio;
	public VolumedAudioClip step, jump, nothing, fall;
	[Space] 
	private SpriteRenderer _renderer;

	private Vector3 ReturnPos=new Vector3(0,6.5f,0);
	public delegate UniTaskVoid PlayerHandler();
	public static event PlayerHandler ReturnEvent;
	private bool FirstAction, SecondAction, SecondActionHandl,Return;
	private int simulationSpeed = 10;

	async UniTaskVoid Start () {
		
		BodyPhysic = GetComponent<Rigidbody2D> ();
		Way = GetComponent<Trajectory> ();
		Particle = transform.GetChild (0).GetComponent<ParticleSystem> ();
		Audio = GetComponent<AudioSource> ();
		_renderer = GetComponent<SpriteRenderer>();
		
	}

	public static void EventReturn()
	{
		ReturnEvent?.Invoke();
	}
	void OnEnable()
	{
		ReturnEvent += ToStart;
	}
	void OnDisable()
	{
		ReturnEvent -= ToStart;
	}
	async UniTaskVoid ToStart()
	{
		Return = true;
		await UniTask.WaitWhile(() => FirstAction || SecondAction);
		gameObject.transform.position = ReturnPos;
		Return = false;
	}

	
	void FixedUpdate ()
	{
		Grounded = Physics2D.OverlapCircle (GroundCheck.position, GroundRadius, WhatIsGround);
		PlayEffects();
		
		checkGround = Grounded;
	}


	void PlayEffects()
	{
		if (!Grounded) wasPlayed = false;
		if (wasPlayed) return;
		if (!Grounded) return;
		Particle.Play();
		AudioSet(fall);
		wasPlayed = true;
	}

	public void Jump(int vector)
	{
		
		Move(vector);
	}
	public void MoveToSide(int x)
	{
		
		Move(x > 0 ? 4 : 5);
	}
	async UniTaskVoid Move(int j)
	{
		if (SecondAction) return;
		if (FirstAction)
		{
			SecondAction = true;
			await UniTask.WaitWhile(() => FirstAction);
		}

		if (j < 4)
		{
			if (UpFree(1, j))
				j = j > 0 ? 0 : 1;
			else if (UpFree(2, j))
				j = j > 0 ? 2 : 3;
			else
			{
				if (Grounded) BodyPhysic.AddRelativeForce(transform.up * Force * 2);
				FirstAction = false;
				SecondAction = false;
				return;
			}
		}	
		
		if (Return) {
			FirstAction = false;
			SecondAction = false;
			return;
		}
		

		if (!Grounded && !SecondAction) return;
		SecondActionHandl = SecondAction;
		FirstAction = true;

		if (!Physics2D.OverlapPoint
		(new Vector2(GroundCheck.position.x + Way.Datas[j].FinalX, GroundCheck.position.y + Way.Datas[j].FinalY),
			WhatIsGround))
		{
			BodyPhysic.simulated = false;
			if(j>=4) AudioSet (step);
			else AudioSet (jump);
			 _renderer.flipX = j % 2 == 0;
			for (int i = 0; i < Way.Datas[j].Steps; i++)
			{
				await UniTask.Yield();
				transform.position = new Vector3(transform.position.x + Way.Datas[j].XPoint[i],
					transform.position.y + Way.Datas[j].YPoint[i],
					transform.position.z);
			}
				
			BodyPhysic.simulated = true;
		}
		else//если позиция куда должен сдвинуться игрок занята, то он немного подпрыгивает, чтобы было понятно, что кнопка нажалась
		if (Grounded)
		{
			AudioSet (nothing);
			BodyPhysic.AddRelativeForce(transform.up * Force * 2);
		}

		if (SecondActionHandl) SecondAction = false;
		SecondActionHandl = false;
		FirstAction = false;
	}

	bool UpFree(int up, int vector){
		return 	(!Physics2D.OverlapPoint
					(new Vector2 (GroundCheck.position.x, GroundCheck.position.y + 1),
					WhatIsGround)  &&
				!Physics2D.OverlapPoint
					(new Vector2 (GroundCheck.position.x, GroundCheck.position.y + up),
					WhatIsGround)  &&
				!Physics2D.OverlapPoint
					(new Vector2 (GroundCheck.position.x + vector, GroundCheck.position.y + up),
					WhatIsGround) 
				);
	}
	
	void AudioSet(VolumedAudioClip vac)
	{
		Audio.volume = vac.volume;
		Audio.clip = vac.clip;
		Audio.Play ();
	}
}