using TMPro;
using UnityEngine;

public class Distance : Counters
{
    private TextMeshProUGUI Distancer;
    private Transform Player;
    public static Distance Instance { private set; get; }
    private int mxdstnc;
    private void Awake()
    {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Distancer = GetComponent<TextMeshProUGUI>(); 
    }
    void Update()
    {
        if (Time.frameCount % 3 != 0 ) return;
        if (mxdstnc < -Player.position.y - 6) mxdstnc = (int) -Player.position.y - 6;
        
        Distancer.text = mxdstnc.ToString();
    }

    public void Reset()
    {
        mxdstnc = 0;
    }
    public void SetupDistance()
    {
        SetMaxDistance(mxdstnc);
    }
   /* public void SetupMaxDistance()
    {
        SetNowDistance(mxdstnc);
    }*/
}