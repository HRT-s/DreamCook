using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitsRecipe : Recipe{
    public override void OnSelected(){
        base.OnSelected();
        elements[0].SetActive(false);
    }

    public override void OpenRefrigerator(){
        base.OpenRefrigerator();
        gm.ChangeCamera();
        if(!player.Items.Contains(elements[0])){
            player.Items.Add(elements[0]);
            elements[0].transform.position = new Vector3(0,-20,0);
            GetElements(elements[0]);
        }
    }

    public override void CookToBoul(){
        if(!isCompleted) return;
        PlayerPrefs.SetFloat("fruits",time);
        SceneManager.LoadScene(1);
    }
}
