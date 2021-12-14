using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    int m,s;

    private void Start(){
        float time = PlayerPrefs.GetFloat("fruits");
        m = (int)(time/60);
        s = (int)(time%60);
    }

    private void Update(){
        timeText.text = "Time: " + m + "分" + s + "秒";
    }
}
