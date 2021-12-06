using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour{
    [SerializeField] private GameObject prologue;
    [SerializeField] private GameObject navi;
    [SerializeField] private GameObject recipeTextObj;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject recipeSelect;
    private Light houseLight;
    private TextMeshProUGUI p_text;
    private TextMeshProUGUI n_text;
    private TextMeshProUGUI r_text;
    private Player player;
    private float feedTime = 0.15f;
    private float time = 0f;
    private float wait = 0f;
    private int state = 0;
    private Camera first,main,playerCam;
    private bool isStart = false;
    private bool isWritten = false;
    private Hashtable recipeTable;
    public int State{get{return state;} set{state = value;}}

    private void Start(){
        houseLight = GameObject.Find("Light").GetComponent<Light>();
        p_text = prologue.GetComponent<TextMeshProUGUI>();
        p_text.maxVisibleCharacters = 0;
        n_text = navi.GetComponent<TextMeshProUGUI>();
        r_text = recipeTextObj.GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("Player").GetComponent<Player>();
        first = GameObject.Find("FirstCamera").GetComponent<Camera>();
        main = Camera.main;
        playerCam = GameObject.Find("PlayerCamera").GetComponent<Camera>();
        main.enabled = false;
        playerCam.enabled = false;
        navi.SetActive(false);
        recipeSelect.SetActive(false);
        recipeTable = new Hashtable();
        recipeTable.Add("Fruits","フルーツ盛り合わせ");
        recipeTable.Add("Curry","カレーライス");
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
        }else if(state == 2){
            wait += Time.deltaTime;
            if(wait > 1.0f && Input.GetMouseButtonDown(0)){
                state = 1;
                r_text.text = "";
                ChangeCamera();
                panel.SetActive(false);
                navi.SetActive(true);
                n_text.text = "Navi: おやすみなさい。";
                wait = 0f;
            }
        }
    }

    private void TypeWriter(){
        if(p_text.maxVisibleCharacters < p_text.text.Length){
            time += Time.deltaTime;
            if(time >= feedTime){
                time = 0f;
                p_text.maxVisibleCharacters++;
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
                prologue.SetActive(false);
                panel.SetActive(false);
                navi.SetActive(true);
            }else{
                p_text.maxVisibleCharacters = p_text.text.Length;
            }
        }
    }

    public void ChangePanelDisp(){
        panel.SetActive(!panel.activeSelf);
    }

    public void ChangeCamera(){
        main.enabled = !main.enabled;
        playerCam.enabled = !playerCam.enabled;
    }

    public void GetRecipe(string r_name){
        state = 2;
        ChangeCamera();
        panel.SetActive(true);
        navi.SetActive(false);
        r_text.text = "レシピ「"+recipeTable[r_name]+"」を手に入れた。";
        List<string> recipe = player.Recipe;
        recipe.Add(recipeTable[r_name].ToString());
        player.Recipe = recipe;
    }

    public void BedTouch(){
        state = 3;
        ChangeCamera();
        panel.SetActive(true);
        navi.SetActive(false);
        recipeSelect.SetActive(true);
        DispRecipeButton("カレーライス");
    }

    private void DispRecipeButton(string value){
        if(player.Recipe.Contains(value)){
            Button curryBtn = recipeSelect.GetComponentInChildren<Button>();
            Text curryText = curryBtn.GetComponentInChildren<Text>();
            curryText.text = value;
        }
    }

    public void Sleep(){
        houseLight.intensity = 0.3f;
        player.Reset();
        ChangeCamera();
        panel.SetActive(false);
        recipeSelect.SetActive(false);
        state = 1;
    }
}
