using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManger : MonoBehaviour
{
    TextMeshProUGUI text;
    float score;

    void Start()
    {
        GameManger.instance.OnScore += OnScore;
        text = GetComponent<TextMeshProUGUI>();
    }

    void OnScore()
    {
        score ++;
        text.text = score.ToString();
    }
}

