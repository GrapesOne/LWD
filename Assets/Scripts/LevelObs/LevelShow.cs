#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelShow : IHaveTypeHolder
{
    public int ColorType { get; private set; }
   
    private CellOb[,] grid;
    private bool _created;
    public static LevelShow Main { get; private set; }

    void Start()
    {
        Main = this;
        LoadBases();
        Restart();
    }
    void OnEnable()
    {
        Main = this;
    }
    [GUIColor(0.8f, 0.8f,1f)]
    [BoxGroup("LevelBase"), HorizontalGroup("LevelBase/1"), VerticalGroup("LevelBase/1/1")][SerializeField] 
    public SettingsEnum.Difficulties difficult;
    [BoxGroup("LevelBase"), HorizontalGroup("LevelBase/1"), VerticalGroup("LevelBase/1/1")
     ,GUIColor(0.8f, 0.8f,1f)][SerializeField] 
    public SettingsEnum.Environment environment;

    [BoxGroup("LevelBase"), HorizontalGroup("LevelBase/1"), VerticalGroup("LevelBase/1/2"),GUIColor(0.8f, 0.8f,1f)]
    [Button("Create",38)]
    public void CreateLevelBase()
    {
        base.LoadBases();
        
        var count = 1+_bases.FindAll(@base => 
            @base.difficult == difficult && @base.environment == environment).Count;
        var name = "l" + count + SettingsEnum.DifficultiesToString[difficult] 
                   + SettingsEnum.EnvironmentToString[environment] ;
        
        var stream = new FileStream(Paths.ToLevel(name), FileMode.OpenOrCreate, FileAccess.Write);
        var writer = new BinaryWriter(stream);
        for (var i = 0; i < x; i++) for (var j = 0; j < y; j++) writer.Write((char)EncodeFormat.slf); 
        writer.Close();
        stream.Close();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        
        var asset = ScriptableObject.CreateInstance<LevelBase>();
        asset.tex = Resources.Load(Paths.ResourcesLevel(name)) as TextAsset;
        
        asset.difficult = difficult;
        asset.environment = environment;
        
        AssetDatabase.CreateAsset(asset, Paths.ToBase(name));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [Space] public int Clock;
    public int Sphere, Crystal, Board;
    [Space]
    [ShowInInspector]
    public static int gridSizeX = 4, gridSizeY = 8;
    [ColorPalette("Cells"),ShowInInspector]
    public static Color Cells;
    public List<Fill> fills = new List<Fill>();
    public class Fill
    {
        [TableMatrix(DrawElementMethod = "DrawCell")]
        public int[,] CustomCellDrawing = new int[gridSizeX, gridSizeY];

        private static int DrawCell(Rect rect, int value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                GUI.changed = true;
                Event.current.Use();
                var rnd = Random.Range(0, 100);
                if (Cells.Equals(Color.green)) value = rnd < 70 ? 1 : 2;
                else if (Cells.Equals(Color.red)) value = rnd < 70 ? 3 : 4;
                else if (Cells.Equals(Color.grey)) value = 0;

                EditorGUI.DrawRect(rect.Padding(1), Cells);
            }

            if (value==1||value==2)
               EditorGUI.DrawRect(rect.Padding(1),Color.green);
            else if (value==3||value==4)
                EditorGUI.DrawRect(rect.Padding(1), Color.red);
            else EditorGUI.DrawRect(rect.Padding(1), Color.grey);
            return value;
            }
    }
    [Button]
    public void ClearTypes()
    {
        var tr = GameObject.Find("Types").transform;
        for (var i = tr.childCount-1; i>=0; i--)
            DestroyImmediate(tr.GetChild(i).gameObject);
    }

    [Button]
    public void SetColors()
    {
        var _setter = GameObject.Find("Types").GetComponent<TypesSetter>();
        _setter.SetColorsToChildren(_holders);
    }
    public void SetColorType(int t) => ColorType = t;
    public async UniTaskVoid Restart()
    {
        if(_levelBase==null) return;
        x = LevelBase.size.x;
        y = LevelBase.size.y;
        CreateGrid();
        await UniTask.Yield();
        FillGridFromLevel();
    }
    public void Clean()
    {
        var tex = ClearTexture();
        for (var i = 0; i < x; i++) for (var j = 0; j < y; j++)
        {
            _levelBase.level[i, j] = EncodeFormat.From(tex[i * y + j]);
            grid[i, j]._type = _levelBase.level[i, j];
            grid[i, j].SetColor();
        }
    }
    
    public void Reset() => FillGridFromLevel();
    public void Save() {
        FillLevelFromGrid();
        var tex = "";
        char ch;
        for (var i = 0; i < x; i++)
        for (var j = 0; j < y; j++)
        {
            ch = EncodeFormat.To(_levelBase.level[i, j]);
            tex += ch;
        }

        SaveTexture(tex);
    }

    public override void LoadBases()
    {
        base.LoadBases();
        Restart();
    }
    public void NextBase()
    {
        if(_nowBase+1<_bases.Count) _levelBase = _bases[++_nowBase];
        Restart();
    }
    public void PreviousBase()
    {
        if(_nowBase-1>=0) _levelBase = _bases[--_nowBase];
        Restart();
    }
    
    

    private void CreateGrid()
    {
        if (_created) return;
        _created = true;
        grid = new CellOb[x,y];
        for(var i = 0; i < x; i++) for (var j = 0; j < y; j++)
        {
            var ob
                = Instantiate(_genOb, new Vector3(i * _genObSize.x, j * _genObSize.y), Quaternion.identity);
            ob.transform.parent = transform;
            grid[i, j] = ob.GetComponent<CellOb>();
        }
    }
    private void FillGridFromLevel()
    {
        string tex;
        if (_levelBase.tex == null) ClearTexture();
        _levelBase.tex = Resources.Load(Paths.ResourcesLevel(_levelBase.name)) as TextAsset;
        if (_levelBase.tex == null) return;
        tex = _levelBase.tex.text;
        _levelBase.Init();
        for (var i = 0; i < x; i++) for (var j = 0; j < y; j++)
        {
            _levelBase.level[i, j] = EncodeFormat.From(tex[i * y + j]);
            grid[i, j]._type = _levelBase.level[i, j];
            grid[i, j].SetColor();
        }
    }

    private void FillLevelFromGrid() {
        for (var i = 0; i < x; i++) for (var j = 0; j < y; j++)
            _levelBase.level[i, j] = grid[i, j]._type;
    }
    

    string ClearTexture()
    {
        var tex = "";
        for (var i = 0; i < x; i++) for (var j = 0; j < y; j++)
            tex += EncodeFormat.slf;
        SaveTexture(tex);
        return tex;
    }
    void SaveTexture(string tga) {
        var stream = new FileStream(Paths.ToLevel(_levelBase.name), FileMode.OpenOrCreate, FileAccess.Write);
        var writer = new BinaryWriter(stream);
        foreach (var t in tga) writer.Write(t);
        writer.Close();
        stream.Close();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    [Button]
    public void FillGridFromEditor()
    {
        int rnd;
        for (var k = 0; k < 16 ; k++) for (var l = 0; l < 16 ; l++) {
            rnd = Random.Range(0, Board);
            var rndType = Random.Range(0, 100);
            grid[k, l]._type = rnd < Clock ? rndType < 70 ? 8 : 9
                : rnd < Clock + Sphere ? rndType < 70 ? 7 : 6
                : rnd < Clock + Sphere + Crystal ? 5
                : 0;
        }

        int kk, ll;
        for (var k = 0; k < x / gridSizeX; k++)
            for (var l = 0; l < y / gridSizeY; l++) {
                rnd = Random.Range(0, fills.Count);
                for (var i = 1; i <= gridSizeX; i++)
                for (var j = 1; j <= gridSizeY; j++)
                {
                    kk = k * gridSizeX + i - 1;
                    ll = l * gridSizeY + j - 1;
                    grid[kk, ll]._type = fills[rnd].CustomCellDrawing[i - 1, gridSizeY - j] == 0 ? 
                        grid[kk, ll]._type : fills[rnd].CustomCellDrawing[i - 1, gridSizeY - j];
                }
                    
            }
    
        for (var k = 0; k < x ; k++) for (var l = 0; l < y ; l++) grid[k, l].SetColor();
    }
}
#endif