using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsHelper : MonoBehaviour
{
    public static ButtonsHelper Instance { private set; get; }
    public Image ls, rs, lu, ru;
    public float time, delay;
    [Required]
    private Color Mask1 = new Color(0,0,0,1), 
        Mask2 = new Color(1,1,1,0), 
        Mask3 = new Color(0,0,0,0.05f);

    private CancellationTokenSource _tokenSource;
    public async UniTaskVoid Start()
    {
        Instance = this;
        _tokenSource = new CancellationTokenSource();
        ShowIfNoClick(_tokenSource.Token);
    }
    // Update is called once per frame
    public void ButtonClicked(Image ob)
    {
        _tokenSource.Cancel();
        ls.color -= ls.color * Mask1;
        rs.color -= rs.color * Mask1;
        lu.color -= lu.color * Mask1;
        ru.color -= ru.color * Mask1;
        ShowOnClick(ob);
        _tokenSource = new CancellationTokenSource();
        ShowIfNoClick(_tokenSource.Token);
    }

    public void ButtonClicked(int i)
    {
        var ob = i switch
        {
            0 => ru,
            1 => lu,
            2 => rs,
            3 => ls,
            _ => ru
        };
        _tokenSource.Cancel();
        ls.color -= ls.color * Mask1;
        rs.color -= rs.color * Mask1;
        lu.color -= lu.color * Mask1;
        ru.color -= ru.color * Mask1;
        ShowOnClick(ob);
        _tokenSource = new CancellationTokenSource();
        ShowIfNoClick(_tokenSource.Token);
    }
    public void HelperClicked()
    {
        ls.color = ls.color * Mask2 + Mask3;
        rs.color = rs.color * Mask2 + Mask3;
        lu.color = lu.color * Mask2 + Mask3;
        ru.color = ru.color * Mask2 + Mask3;
    }
    
    public async UniTask ShowOnClick(Image ob)
    {
        ob.color = ob.color * Mask2 + Mask3;
        await UniTask.Delay((int)(time*100));
        ob.color -= ob.color * Mask1;
    }
    
    public async UniTask ShowIfNoClick(CancellationToken token)
    {
        await UniTask.Delay((int)(delay*100));
        if(token.IsCancellationRequested) return;
        
       
        
        HelperClicked();

        while (true)
        {
            await UniTask.Delay(300);
            ls.transform.localScale *= 0.97f;
            rs.transform.localScale *= 0.97f;
            lu.transform.localScale *= 0.97f;
            ru.transform.localScale *= 0.97f;
            
            if (token.IsCancellationRequested)
            {
                ls.transform.localScale = Vector3.one;
                rs.transform.localScale = Vector3.one;
                lu.transform.localScale = Vector3.one;
                ru.transform.localScale = Vector3.one;
                return;
            }
            await UniTask.Delay(300);
            ls.transform.localScale /= 0.97f;
            rs.transform.localScale /= 0.97f;
            lu.transform.localScale /= 0.97f;
            ru.transform.localScale /= 0.97f;
            if (token.IsCancellationRequested)
            {
                ls.transform.localScale = Vector3.one;
                rs.transform.localScale = Vector3.one;
                lu.transform.localScale = Vector3.one;
                ru.transform.localScale = Vector3.one;
                return;
            }
        }
       
        
    }
    
}
