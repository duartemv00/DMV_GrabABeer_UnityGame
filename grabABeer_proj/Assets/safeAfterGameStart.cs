using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safeAfterGameStart : MonoBehaviour
{
    public GameObject begGO; //BeginEndGame game object

//**********On Trigger EXIT**********//
    void OnTriggerExit(Collider col){
        if(col.gameObject.tag == "Player"){
            begGO.SetActive(true);
        }
    }
}
