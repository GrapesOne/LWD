using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

///The formula for a basic superellipse is
///Mathf.Pow(Mathf.Abs(x / a), n) + Mathf.Pow(Mathf.Abs(y / b), n) = 1
[ExecuteInEditMode]
public class SuperellipsePoints : MaskableGraphic
{
    public float xLimits = 1f;
    public float yLimits = 1f;
    [Range(1f, 20)] public float superness = 4f;
    [Range(0f, 1f)] public float MultiplayerSuperness = 4f;

    private float lastXLim;
    private float lastYLim;
    private float lastMultiplayerSuperness;
    private float lastSuper;

    [Space] [Range(1, 1000)] public int levelOfDetail = 4;

    private int lastLoD;
    float realLoD => 1f/levelOfDetail ;

    private List<Vector2> pointList = new List<Vector2>();
    public Vector2 uv = new Vector2();
    public Vector2 uv2 = new Vector2();
    public Vector2 uv3 = new Vector2();
    public Vector4 uv4 = new Vector4();
    
    public List<Vector2> Calculate()
    {
        pointList.Clear();

       
        float i = 0, _y = 0, extraSuper = MultiplayerSuperness * superness;
        for (; i < xLimits; i += realLoD)
        {
            _y = Superellipse(xLimits, yLimits, i, extraSuper);
            pointList.Add(new Vector2(i, _y));
        }
        
      
        pointList.Add(new Vector2(xLimits, 0));
        
        for (i -= realLoD; i > 0; i -= realLoD)
        {
            _y = Superellipse(xLimits, yLimits, i, extraSuper);
            pointList.Add(new Vector2(i, -_y));
        }
        
        _y = Superellipse(xLimits, yLimits, 0, extraSuper);
        pointList.Add(new Vector2(0, -_y));

        for (i += realLoD; i < xLimits; i += realLoD)
        {
            _y = Superellipse(xLimits, yLimits, i, extraSuper);
            pointList.Add(new Vector2(-i, -_y));
        }
        
        pointList.Add(new Vector2(-xLimits, 0));
        
        for (i -= realLoD; i > 0; i -= realLoD)
        {
            _y = Superellipse(xLimits, yLimits, i, extraSuper);
            pointList.Add(new Vector2(-i, _y));
        }

        return pointList;
    }
    

    float Superellipse(float a, float b, float x, float n)
    {
        float alpha = Mathf.Pow((x / a), n);
        float beta = 1 - alpha;
        float y = Mathf.Pow(beta, 1 / n) * b;

        return y;
    }
    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        SetVerticesDirty();
        SetMaterialDirty();
    }
    
    private RectTransform m_rect;
    Vector2 pos ;
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        m_rect = GetComponent<RectTransform>();
    }
#endif
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var tr = new Triangulator(Calculate().ToArray());
        var triangles = tr.Triangulate();
        Vector2 LeftBottom = new Vector2(xLimits,yLimits);
        vh.Clear();
        for (var i = 0; i < pointList.Count; i++) {
            pos.x = m_rect.sizeDelta.x*uv3.x*(pointList[i].x);
            pos.y = m_rect.sizeDelta.y*uv3.y*(pointList[i].y);
            
            vh.AddVert(pos+uv2, color, pointList[i]+uv, Vector2.zero, Vector2.zero, 
                uv4, uv4, uv4 );
        }
        for (var i = 0; i < triangles.Length; i += 3)
            vh.AddTriangle(triangles[i], triangles[i + 1], triangles[i + 2]);
        
    }
    
    [SerializeField]
    Texture m_Texture;
    
    // make it such that unity will trigger our ui element to redraw whenever we change the texture in the inspector
    public Texture texture
    {
        get
        {
            return m_Texture;
        }
        set
        {
            if (m_Texture == value)
                return;
 
            m_Texture = value;
            SetVerticesDirty();
            SetMaterialDirty();
        }
    }

    public override Texture mainTexture
    {
        get { return m_Texture == null ? s_WhiteTexture : m_Texture; }
    }
}
