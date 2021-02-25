using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

public class Generator : IHaveTypeHolder
{
    
    public SettingsEnum.Difficulties nowDifficulties;
    public SettingsEnum.Environment nowEnvironment;

    private LinkedList<GameObject> Borders = new LinkedList<GameObject>();
    private Dictionary<int, LinkedList<GameObject>> ObsOnLevel = new Dictionary<int, LinkedList<GameObject>>();
    private static int _nowLevel, _group;
    private static bool _genDone = true, _resetRequested = false;

    public static bool GenDone => _genDone;
    public static bool ResetRequested => _resetRequested;

    public delegate UniTask<bool> Handler(SettingsEnum.Difficulties difficulties, SettingsEnum.Environment environment);
    public static event Handler GenerateEvent;
    public delegate UniTask HandlerDestory();
    public static event HandlerDestory DestroyEvent;
    public static Generator Instance { private set; get; }

    void Awake()
    {
        Instance = this;
        BorderGenerate(6);
    }
    void OnEnable()
    {
        GenerateEvent += Generate;
        DestroyEvent += DestroyObs;
    } 
    void OnDisable() {
        GenerateEvent -= Generate;
        DestroyEvent -= DestroyObs;
    } 
    Vector3 v;
    public async UniTask<bool> Generate(SettingsEnum.Difficulties difficulties, SettingsEnum.Environment environment)
    {
        nowDifficulties = difficulties;
        nowEnvironment = environment;
        var @base = GetBase();
        //Debug.Log(_nowLevel + " LEVEL" );
        if (!@base.Init()) return false;
        //var id = @base.name + _nowLevel++;
        if(!ObsOnLevel.ContainsKey(_nowLevel)) ObsOnLevel[_nowLevel] = new LinkedList<GameObject>();
        v.x = -LevelBase.size.x*_genObSize.x/2f;
        var startX = v.x;
        x = LevelBase.size.x;
        y = LevelBase.size.y - 1;

        _group = Random.Range(0, 3);
        
        for (var i = y; i >= 0; i--)
        {
            for (var j = -1; j <= x; j++)
            {
                var inst = _holders[j == -1 || j == x ? 1 : @base.level[j, i]].CreateOb(_genOb, v, _group);
                if(inst != null) ObsOnLevel[_nowLevel].AddFirst(inst);
                v.x += _genObSize.x;
            }

            await UniTask.Yield();
            v.x = startX;
            v.y -= _genObSize.y;
            /*var nv = Camera.main.GetComponent<CameraMover>().mainPos;
            nv.y = v.y+10;
            Camera.main.GetComponent<CameraMover>().mainPos = nv;*/
        }
        BorderGenerate(1);
        _nowLevel++;
        return true;
    }

    private void BorderGenerate(int height)
    {
        v.x = -LevelBase.size.x*_genObSize.x/2f;
        var startX = v.x;
        x = LevelBase.size.x+1;
        for (var i = height-1; i >= 0; i--)
        {
            var inst = _holders[1].CreateOb(_genOb, v);
            if(inst != null) Borders.AddFirst(inst);
            v.x += _genObSize.x*x;
            inst = _holders[1].CreateOb(_genOb, v);
            if(inst != null) Borders.AddFirst(inst);
            v.x = startX;
            v.y -= _genObSize.y;
        }

        if (Borders.Count <= 20) return;
        var last = Borders.Last;
        PoolManager.putGameObjectToPool(last.Value);
        Borders.Remove(last);
        last = Borders.Last;
        PoolManager.putGameObjectToPool(last.Value);
        Borders.Remove(last);
    }

    private void ClearBorder()
    {
        foreach (var border in Borders) PoolManager.putGameObjectToPool(border);
        Borders.Clear();
    }
    
    public LevelBase GetBase()
    {
        if (!_hasBases) {
            LoadBases();
            _hasBases = _bases.Count > 0;
        }
        if (!_hasBases) return null;
        var bases = _bases.FindAll(@base => @base.difficult == nowDifficulties 
                                            && @base.environment == nowEnvironment);
        return bases[Random.Range(0, bases.Count)];
    }

    
    public async UniTask DestroyObs()
    {
        if (_nowLevel < 5) return;
        await InternalDestroy(_nowLevel- 5);
    }

    private async UniTask InternalDestroy(int id)
    {
        var tic = 32;
        foreach (var ob in ObsOnLevel[id])
        {
            if (tic-- <= 0)
            {
                tic = 32;
                await UniTask.Yield();
            }

            PoolManager.putGameObjectToPool(ob);
        }
        ObsOnLevel[id].Clear();
    }
    private async UniTaskVoid DestroyTask(int id)
    {
        if (ObsOnLevel.ContainsKey(id) && ObsOnLevel[id].Count != 0) await InternalDestroy(id);
        _taskCount--;
    }


    private int _taskCount;
    [Button]
    public async UniTaskVoid Reset()
    {
        await UniTask.WaitUntil(() => !_resetRequested);
        _resetRequested = true;
        await UniTask.WaitUntil(() => _genDone);
        for (var id = 0; id < ObsOnLevel.Count; id++)
        {
            _taskCount++;
            DestroyTask(id);
        }
        
        v = Vector3.zero;
        TestPlayerController.Main.transform.position = new Vector3(0,-30,0);
        TestPlayerController.Main._level = -2;
        _nowLevel = 0;
        _targetLevel = -2;
        ClearBorder();
        await UniTask.Yield();
        BorderGenerate(6);
        await UniTask.WaitUntil(() => _taskCount == 0);
        await UniTask.Yield();
        _genDone = true;
        _resetRequested = false;
    }

    private static int _targetLevel = -2;
    public static async UniTaskVoid OnGenerateEvent()
    {
        await UniTask.WaitUntil(() => CanGenerate);
        _genDone = false;
        await UniTask.WaitUntil(() => _targetLevel < _nowLevel);
        _targetLevel++;
        if (!await GenerateEvent(SettingsEnum.Difficulties.easy, SettingsEnum.Environment.stone))
            Debug.Log("Generate Error");
        await DestroyEvent();
        
        _genDone = true;
    }

    public static bool CanGenerate => _genDone && !_resetRequested;

}
