using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;
using Duarto.GrabABeer.Player;

public class BeginEndGame : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player"){
            if(GameManager.Instance.AskIfTimmerRunning()){
                GameManager.Instance.StopTimmer();
                GameManager.Instance.SetPauseState(true);
                ScreenManager.Instance.AddScreen(GameScreens.Points);
                PlayerController.Instance.currentSpeed = 0;
            } else { 
                GameManager.Instance.StartTimmer();
                col.gameObject.transform.parent.gameObject.transform.position += new Vector3(0,0,-0.5f);
            }
        }
    }
}
