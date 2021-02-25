using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenImage : MonoBehaviour
{
    public Sprite[] images;
    public TextMeshProUGUI loadProgressText;
    public void ChooseRandom() {
        GetComponent<Image>().sprite = images[Random.Range(0, images.Length)];
    }
    public async UniTask StartLoadingProgress(AsyncOperation loadingProgress)
    {
        ChooseRandom();
        while (!loadingProgress.isDone)
        {
            var percentDone = (int)(loadingProgress.progress * 100);
            loadProgressText.text = $"{percentDone}%";
            await UniTask.Yield();
        }
        Destroy(gameObject);
    }
}
