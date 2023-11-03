using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;
using Duarto.GrabABeer.Player;

public class BeginEndGame : MonoBehaviour
{

//**********On Trigger ENTER**********//
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player"){
            if(GameManager.Instance.AskIfTimmerRunning()){ //Stop the game (the timmer)
                StopTheGame();
            } else { //Start the game (the timmer)
                this.gameObject.SetActive(false);
                StartTheGame();
            }
        }
    }

//**********START THE GAME**********//
    void StartTheGame(){
        GameManager.Instance.StartTimmer(); //Start timmer
        ScreenManager.Instance.AddScreen(GameScreens.HUD); //Add the HUD (puntuation and time left info)
        //Audio changes
        AudioManager.Instance.StopAmbience();
        AudioManager.Instance.StartMusic();      
    }

//**********STOP THE GAME**********//
    void StopTheGame(){
        GameManager.Instance.StopTimmer(); //Stop timmer
        //Stop game
        GameManager.Instance.SetPauseState(true);
        PlayerController.Instance.speed = 0;
        ScreenManager.Instance.ChangeScreen(GameScreens.Points, GameScreens.HUD); //Add the points window   
        AudioManager.Instance.CashRegister(); //Audio changes
    }
}
