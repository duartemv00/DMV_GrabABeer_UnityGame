using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;

public class BeginEndGame : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        Debug.Log("Collision working");
        if (col.gameObject.tag == "Player"){
            Debug.Log("Player enters the game");
            if(GameManager.Instance.AskIfTimmerRunning()){
                Debug.Log("Partida acabada");
                GameManager.Instance.StopTimmer();
                GameManager.Instance.SetPauseState(true);
                ScreenManager.Instance.AddScreen(GameScreens.Points);
            } else { 
                Debug.Log("Partida empieza");
                GameManager.Instance.StartTimmer();
            }
        }
    }
}
