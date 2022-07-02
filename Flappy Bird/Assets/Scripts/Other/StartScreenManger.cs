using System.Collections;
using UnityEngine;

public class StartScreenManger : MonoBehaviour
{
    CanvasGroup canvas;

    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        GameManger.instance.OnGameStart += OnGameStart;
    }

    void OnGameStart()
    {
        // StartCoroutine(ReduceAlpha());
        Destroy(gameObject);
    }

    IEnumerator ReduceAlpha()
    {
        while(canvas.alpha > .1f)
        {
           canvas.alpha -= Time.deltaTime * 1.5f;
           yield return null;
        }

        Destroy(gameObject);
    }
}
