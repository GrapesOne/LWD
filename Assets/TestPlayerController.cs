using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    private Transform _transform;
    private Transform _player;
    public float levelSize;
    public int _level = -2;
    public static TestPlayerController Main { get; private set; }
    void Start()
    {
        Main = this;
        _transform = transform;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        CheckPos(levelSize);
    }

    void Update()
    {
        _transform.position = _player.position;
    }

    public async UniTaskVoid CheckPos(float y)
    {
        while (true)
        {
            //        Debug.Log(_level + " " + _transform.position.y / y);
            if (_transform.position.y / y < -_level)
            {
                await UniTask.WaitUntil(() => Generator.CanGenerate);
                _level++;
                Generator.OnGenerateEvent();
            }

            for (var i = 3; i != 0; --i) await UniTask.Yield();
            
        }
    }
    
}



