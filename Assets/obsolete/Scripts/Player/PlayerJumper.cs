using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumper : MonoBehaviour {
	public float jumperTime;
	public float jumperUp;
	public float jumperScaler;
	public bool OnStay;
	private SpriteRenderer _renderer;
	void Start()
	{
		_renderer = GetComponent<SpriteRenderer>();
		if (OnStay) Play(true, gameObject.GetComponent<Rigidbody2D>());
		_renderer.size = new Vector2(1,0.8f);
	}
	
	

	private async UniTaskVoid Play(bool Grounded, Rigidbody2D BodyPhysic) {
		while (true)
		{
			float scaler = 0;
			await UniTask.Delay((int)(jumperTime*1000));
			if (Grounded) {
				scaler = jumperScaler;
				/*var vs = _renderer.sprite.vertices;
				for (var i = 0; i<vs.Length;i++)
				{
					vs[i].y += 1f;//new Vector2(0,0.1f);
				}
				_renderer.sprite.OverrideGeometry(vs, _renderer.sprite.triangles);*/
				BodyPhysic.AddRelativeForce (transform.up * jumperUp);
			}
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y - scaler, transform.localScale.z);
			await UniTask.Delay((int)(jumperTime*1000));
			transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y + scaler, transform.localScale.z);
			await UniTask.WaitUntil(() => Grounded);
		}

	}

	
}
