using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBonus : Counters {
    public ParticleSystem SecondParticle;
    public AudioSource SecondAudio;
    private float pointToleft;
    private float pointToRight;
    private float newPosition;
    float smoothTime = 0.6f;
    float yVelocity = 0.0f;
    int where = 0;
    readonly Vector3 left = new Vector3(-1f, 0, 0);
    readonly Vector3 right = new Vector3(1f, 0, 0);
    public LayerMask WhatIsGround;
    Vector3 pos;
    private CancellationTokenSource _tokenSource;

    
    private void OnDisable()
    {
        _tokenSource.Cancel();
        pos = transform.position;
        yVelocity = 0;
        pointToleft = 0;
        pointToRight = 0;
    }
     private async UniTaskVoid OnEnable()
     {
         await UniTask.Yield();
         await UniTask.Yield();
         _tokenSource = new CancellationTokenSource();
        pos = transform.position;
        var leftpos = pos+left;
         var leftDownpos=leftpos+new Vector3(0, -1f, 0);
         Debug.Log("left" + leftpos+ " " + !Physics2D.OverlapCircle(leftpos, 0.1f, WhatIsGround) + ":" +
                   Physics2D.OverlapCircle(leftDownpos, 0.1f, WhatIsGround) + " "+ (WhatIsGround == LayerMask.NameToLayer("Ground")));
         while (!Physics2D.OverlapCircle(leftpos, 0.05f, WhatIsGround)&& Physics2D.OverlapCircle(leftDownpos, 0.05f, WhatIsGround)){ 
             leftpos += left;
             leftDownpos += left;
            Debug.Log("left" + leftpos);
            Debug.Log("left" + leftDownpos);
            
         }
         
         var rightpos = pos+right;
         var rightDownpos = rightpos+new Vector3(0,-1f,0);
         while (!Physics2D.OverlapCircle(rightpos, 0.1f, WhatIsGround) && Physics2D.OverlapCircle(rightDownpos, 0.1f, WhatIsGround))
         {
             rightpos += right;
             rightDownpos += right;
         }

         
         pointToleft = leftpos.x-left.x;
         pointToRight = rightpos.x-right.x;
         Debug.Log(pointToleft+" "+pointToRight);
         where = Random.Range(0, 2);
         if(Math.Abs(pointToleft - pointToRight) < 0.1f) return;
         Moving(_tokenSource.Token);
     }

    protected override void ActBonus()
    {
        Tink = false;
        _tokenSource.Cancel();
        pos = transform.position;
        yVelocity = 0;
        pointToleft = 0;
        pointToRight = 0;
        TimeManager.Instance.DownTime();
    }

    private async UniTaskVoid Moving(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            newPosition = transform.position.x;
            await UniTask.Yield();
            
            
            if ( Mathf.Abs(pointToleft-transform.position.x) > 0.1f && where == 0)
                newPosition = Mathf.SmoothDamp(transform.position.x, pointToleft, ref yVelocity, smoothTime);
            else
                where = 1;
            if (pointToRight - transform.position.x > 0.1f && where == 1)
                newPosition = Mathf.SmoothDamp(transform.position.x, pointToRight, ref yVelocity, smoothTime);
            else
                where = 0;
            transform.position = new Vector2(newPosition, transform.position.y);
        }
    }
}


///-18 - (-20) >  6*2 - 3
/// 2 > 9
/// 
///