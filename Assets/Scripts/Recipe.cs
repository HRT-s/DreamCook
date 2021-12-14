using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Recipe : MonoBehaviour{
    [SerializeField] protected List<GameObject> elements;
    [SerializeField] protected GameManager gm;
    [SerializeField] protected Player player;
    [SerializeField] protected GameObject dreamTextObj;
    [SerializeField] protected GameObject elementsTextObj;
    protected List<string> textlist;
    protected TextMeshProUGUI e_text;
    protected bool isSelected;
    protected bool isCompleted;
    protected float time;

    protected void Start() {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        textlist = new List<string>();
        string texts = "";
        foreach(GameObject element in elements){
            element.SetActive(false);
            textlist.Add(element.name+"(0/1)");
            texts += element.name+"(0/1)\n";
        }
        e_text = elementsTextObj.GetComponent<TextMeshProUGUI>();
        e_text.text = texts;
        dreamTextObj.SetActive(false);
        elementsTextObj.SetActive(false);
        isSelected = false;
    }

    protected void Update() {
        time += Time.deltaTime;
        List<GameObject> items = player.Items;
        isCompleted = true;
        foreach(GameObject element in elements){
            if(!items.Contains(element)){
                isCompleted = false;
                break;
            }
        }
        if(isCompleted){
            gm.SetNavi("Navi:キッチンで料理を作ろう。");
        }
    }

    protected void SetText(){
        string texts = "";
        foreach(string text in textlist){
            texts += text+"\n";
        }
        e_text.text = texts;
    }

    public void GetElements(GameObject element){
        textlist[elements.IndexOf(element)] = element.name + "(1/1)";
        SetText();
    }

    public virtual void OnSelected(){
        isSelected = true;
        gm.Sleep();
        dreamTextObj.SetActive(true);
        elementsTextObj.SetActive(true);
        player.SetRecipe(this);
        foreach(GameObject element in elements){
            element.SetActive(true);
        }
        FadeManager.FadeIn();
    }

    public virtual void OpenRefrigerator(){
        if(!isSelected) return;
        gm.State = 2;
        gm.SetR_text("Appleを手に入れた。");
    }

    public virtual void CookToBoul(){

    }
}
