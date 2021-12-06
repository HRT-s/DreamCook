using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Recipe : MonoBehaviour{
    [SerializeField] protected List<GameObject> elements;
    [SerializeField] protected GameManager gm;
    [SerializeField] protected GameObject dreamTextObj;
    protected bool isSelected;

    private void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        dreamTextObj.SetActive(false);
        isSelected = false;
    }

    public void OnSelected(){
        isSelected = true;
        gm.Sleep();
        dreamTextObj.SetActive(true);
    }

    protected void OpenRefrigerator(){
        if(!isSelected) return;
        gm.ChangePanelDisp();
    }
}
