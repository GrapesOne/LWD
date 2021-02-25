using System.Threading;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[SerializeField] Transform[] target = new Transform[0];
	[Space]
	[Range(0,6)] public int nowTarget;

	public float damping = 1, lookAheadFactor = 3, lookAheadReturnSpeed = 0.5f, lookAheadMoveThreshold = 0;
	private float offsetZ;
	private Vector3 lastTargetPosition;
	private Vector3 currentVelocity;
	private Vector3 lookAheadPos;
	public static CameraManager Instance { private set; get; }
	public float time = 5;
	[Space]
	[SerializeField] Color[] BackgroundColor = new Color[0];
	[SerializeField] int ColorSteps = 5;
	Color nowColor;
	private Camera camera;
	private bool ChangeColorNow;
    private void Awake()
    {
	    camera = Camera.main;
		Instance = this;
    }

    void Start () {
		lastTargetPosition = target[nowTarget].position;
		offsetZ = (transform.position - target[nowTarget].position).z;

	}
    public void OnButtonTap(int num)
    {
		nowTarget = num;
    }
    void Update () {

		if (target != null) {
			nowColor = camera.backgroundColor;
			ChangeColor();
			float yMoveDelta = (target[nowTarget].position - lastTargetPosition).y;
			lookAheadPos = Vector3.MoveTowards (lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);

			Vector3 aheadTargetPos = target[nowTarget].position + lookAheadPos + Vector3.forward * offsetZ;
			Vector3 newPos = Vector3.SmoothDamp (transform.position, aheadTargetPos, ref currentVelocity, damping);
			transform.position = newPos;
			lastTargetPosition = transform.position;

		} else 
			Debug.Log ("CameraManage Target not found");
		
	}

	private async UniTaskVoid ChangeColor()
	{
		if (ChangeColorNow) return;
		ChangeColorNow = true;
		var currTime = 0f;
		var delta = Time.deltaTime*Time.deltaTime;
		var delay = 1200 / ColorSteps;
		while (currTime <= 1f/ColorSteps) 
		{
			camera.backgroundColor = Color.Lerp(nowColor, BackgroundColor[nowTarget], currTime*ColorSteps);
			currTime += delta;
			await UniTask.Delay(delay); 
		} 
		ChangeColorNow = false;
	}
	
		/*ChangeColorNow = true;
		Vector4 vec = new Vector4 (BackgroundColor [nowTarget].r - camera.backgroundColor.r, 
			BackgroundColor [nowTarget].g - camera.backgroundColor.g,
			BackgroundColor [nowTarget].b - camera.backgroundColor.b, 
			BackgroundColor [nowTarget].a - camera.backgroundColor.a);
		vec = new Vector4 (vec.x / (float)ColorSteps, vec.y / (float)ColorSteps, vec.z / (float)ColorSteps , vec.w  / (float)ColorSteps);
		await UniTask.Delay(100);
		if (token.IsCancellationRequested)
		{
			previousTarget = nowTarget;
			ChangeColorNow = false;
			return;
		}
		for (int i = 0; i < ColorSteps; i++) {
			Camera.main.backgroundColor += new Color (vec.x, vec.y, vec.z, vec.w);
			await UniTask.Delay(10);
			if (!token.IsCancellationRequested) continue;
			previousTarget = nowTarget;
			ChangeColorNow = false;
			return;
		}
		
		previousTarget = nowTarget;
		ChangeColorNow = false;
	*/	
	
}
