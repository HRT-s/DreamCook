using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour{
    [SerializeField] private GameObject textObj;
    [SerializeField] private GameObject canvas;
    private TextMeshProUGUI text;
    private float feedTime = 0.15f;
    private float time = 0f;
    private int state = 0;
    private Camera first,main,playerCam;
    private bool isStart = false;
    private bool isWritten = false;
    public int State{get{return state;} set{state = value;}}

    private void Start(){
        text = textObj.GetComponent<TextMeshProUGUI>();
        text.maxVisibleCharacters = 0;
        first = GameObject.Find("FirstCamera").GetComponent<Camera>();
        main = Camera.main;
        playerCam = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        main.enabled = false;
        playerCam.enabled = false;
        Invoke("WaitStart",3.0f);
    }

    private void WaitStart(){
        isStart = true;
    }

    private void Update(){
        if(!isStart) return;
        if(state == 0){
            TypeWriter();
            MouseClicked();
        }else if(state == 1){
            if(Input.GetMouseButtonDown(0)){
                main.enabled = !main.enabled;
                playerCam.enabled = !playerCam.enabled;
            }
        }
    }

    private void TypeWriter(){
        if(text.maxVisibleCharacters < text.text.Length){
            time += Time.deltaTime;
            if(time >= feedTime){
                time = 0f;
                text.maxVisibleCharacters++;
            }
        }else{
            isWritten = true;
        }
    }

    private void MouseClicked(){
        if(Input.GetMouseButtonDown(0)){
            if(isWritten){
                state = 1;
                first.enabled = false;
                main.enabled = true;
                canvas.SetActive(false);
            }else{
                text.maxVisibleCharacters = text.text.Length;
            }
        }
    }
}
