using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

///The formula for a basic superellipse is
///Mathf.Pow(Mathf.Abs(x / a), n) + Mathf.Pow(Mathf.Abs(y / b), n) = 1
[ExecuteInEditMode]
public class SquircleImage : MaskableGraphic
{
    #region Public Properties

    [BoxGroup("EllipseSetting"), HorizontalGroup("EllipseSetting/0"), VerticalGroup("EllipseSetting/0/1"),
     LabelWidth(50)]
    public float xLimits = 0.5f;

    [BoxGroup("EllipseSetting"), HorizontalGroup("EllipseSetting/0"), VerticalGroup("EllipseSetting/0/1"),
     LabelWidth(50)]
    public float yLimits = 0.5f;

    [BoxGroup("EllipseSetting"), HorizontalGroup("EllipseSetting/0"), VerticalGroup("EllipseSetting/0/2")]
    [Range(1f, 20)]
    public float superness = 8;

    [BoxGroup("EllipseSetting"), HorizontalGroup("EllipseSetting/0"), VerticalGroup("EllipseSetting/0/2")]
    [Range(0f, 1f)]
    public float MultiplayerSuperness = 1;

    [BoxGroup("EllipseSetting"), HorizontalGroup("EllipseSetting/0"), VerticalGroup("EllipseSetting/0/2")]
    [Range(1, 1000)]
    public int levelOfDetail = 64;

    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/2"),
     PreviewField(ObjectFieldAlignment.Center), HideLabel,
     Space(5), LabelWidth(45)]
    [SerializeField]
    Texture m_Texture;

    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector2 ReTriangulation = new Vector2(16, 0.2f);

    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector2 falloff = new Vector2(0.01f, 0.01f);
    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector2 UV = new Vector2(0.5f, 0.5f);
    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector2 uv2 = new Vector2();
    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector2 uv3 = new Vector2();
    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector4 uv4 = new Vector4();
    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector2 ExtraPos;

    [BoxGroup("MeshSetting"), HorizontalGroup("MeshSetting/0"), VerticalGroup("MeshSetting/0/1")]
    public Vector2 Scale = new Vector2(1f, 1f);


    public Texture texture
    {
        get => m_Texture;
        set
        {
            if (m_Texture == value) return;
            m_Texture = value;
            SetVerticesDirty();
            SetMaterialDirty();
        }
    }

    public override Texture mainTexture => m_Texture == null ? s_WhiteTexture : m_Texture;

    #endregion

    #region Private Properties

    private float lastXLim, lastYLim, lastMultiplayerSuperness, lastSuper, extraSuper, i, _y;
    private int lastLoD, j, temp;
    float realLoD => 1f / levelOfDetail;
    private List<Vector2> pointList = new List<Vector2>();
    private List<int> _trisOut = new List<int>(), _trisIn = new List<int>();
    private Vector2 pos;
    
    private Triangulator _triangulator = new Triangulator();
    private RectTransform m_rect;

    #endregion


    int[] FindTriangles()
    {
        if (_trisOut.IsNullOrEmpty())
        {
            extraSuper = ReTriangulation.x;
            Calculate();
            _trisOut = new List<int>(_triangulator.Triangulate(pointList.ToArray()));
        }

        if (_trisIn.IsNullOrEmpty())
        {
            extraSuper = ReTriangulation.y;
            Calculate();
            _trisIn = new List<int>(_triangulator.Triangulate(pointList.ToArray()));
        }

        extraSuper = MultiplayerSuperness * superness;
        Calculate();

        return (extraSuper >= 1 ? _trisOut : extraSuper < 0.15f ? new List<int>() : _trisIn).ToArray();
    }

    public List<Vector2> Calculate()
    {
        pointList.Clear();
        if (extraSuper < 0.15f) return pointList;
        pointList.Add(new Vector2(0, yLimits));
        for (i = realLoD; i < xLimits; i += realLoD)
        {
            _y = Superellipse(xLimits, yLimits, i, extraSuper);
            pointList.Add(new Vector2(i, _y));
        }

        temp = pointList.Count;
        pointList.Add(new Vector2(xLimits, 0));
        for (j = temp - 1; j >= 0; j--) pointList.Add(new Vector2(pointList[j].x, -pointList[j].y));
        for (j = 1; j < temp; j++) pointList.Add(new Vector2(-pointList[j].x, -pointList[j].y));
        for (j = temp; j > 0; j--) pointList.Add(new Vector2(-pointList[j].x, pointList[j].y));

        return pointList;
    }


    float Superellipse(float a, float b, float x, float n)
    {
        var alpha = Mathf.Pow((x / a), n);
        var beta = 1 - alpha;
        var y = Mathf.Pow(beta, 1 / n) * b;
        return y;
    }

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        SetVerticesDirty();
        SetMaterialDirty();
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        m_rect = GetComponent<RectTransform>();
        if (levelOfDetail == lastLoD) return;
        _trisIn.Clear();
        _trisOut.Clear();
        lastLoD = levelOfDetail;
    }

    public void HandlOnValidate()
    {
        OnValidate();
    }
#endif
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var triangles = FindTriangles();
        
        
        vh.Clear();
        for (var i = 0; i < pointList.Count; i++)
        {
            pos.x = m_rect.sizeDelta.x * Scale.x * (pointList[i].x);
            pos.y = m_rect.sizeDelta.y * Scale.y * (pointList[i].y);

            vh.AddVert(pos+ExtraPos, color, pointList[i] + UV, uv2, uv3, uv4);
        }

       
        for (var i = 0; i < triangles.Length; i += 3)
            vh.AddTriangle(triangles[i], triangles[i + 1], triangles[i + 2]);
        
    }

    private void F()
    {
        int N = 1, i = 1, mult = 1, s = 0;
        //scanf("%d",&N);
        s = N%2;
        while (i < N) {
            mult *= i + s;
            i += 2;
        }
    }
    
 
    
}
