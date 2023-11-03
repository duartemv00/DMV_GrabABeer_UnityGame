using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Duarto.Utilities;
using Duarto.GrabABeer.Player;
using UnityEngine.SceneManagement;


namespace Duarto.GrabABeer.Manager {

    public class GameManager : Singleton<GameManager>
    {
        bool isGamePaused = true;
        bool gameStart = false;
        public bool goingFromGameplay;
        
        int points = 0;
        bool timmerRunning = false;
        [Header("Countdown")]
        public float countdownValue = 60;
        public TextMeshProUGUI countdownText;
        [Header("Points")]
        public TextMeshProUGUI pointsText;
        

//**********AWAKE**********// 
        void Awake() {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            Cursor.visible = false;
        }

//**********START**********// 
        void Start() {
            if(goingFromGameplay) {
                ScreenManager.Instance.ChangeScreen(GameScreens.Game,GameScreens.None);
            } else {
                ScreenManager.Instance.ChangeScreen(GameScreens.Intro,GameScreens.None);
            }
            ScreenManager.Instance.ChangeScreen(GameScreens.Intro,GameScreens.None);
            
            pointsText.text = points.ToString();
            countdownText.text = countdownValue.ToString();
        }

//**********UPDATE**********// 
        void Update(){
            if(Application.targetFrameRate != 60) { Application.targetFrameRate = 60; }
             
            //Pause and Unpause game with esc key
            if(gameStart){
                if(Input.GetButtonDown("Pause")) {
                    if(isGamePaused) { //Game is already paused
                        UnpauseGame();
                    } else if (!ScreenManager.Instance.isAnim) { //Game is running
                        PauseGame();
                    }
                }
            }

            if((countdownValue >= 0f) && timmerRunning && !isGamePaused) {
                countdownValue -= Time.deltaTime; //update countdown
                countdownValue = Round(countdownValue,2);
                countdownText.text = countdownValue.ToString();

                if(countdownValue == 10f){
                    countdownText.color = Color.red;
                    countdownText.fontSize = 150;
                    countdownText.rectTransform.anchoredPosition = new Vector3(-75,0,0);
                    countdownText.gameObject.GetComponent<CanvasGroup>().alpha = 0.2f;
                }
                
                if(countdownValue < 0) {
                    PlayerController.Instance.speed = 0; //STOP THE PLAYER
                    timmerRunning = false;
                    isGamePaused = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    ScreenManager.Instance.AddScreen(GameScreens.Fail); 
                    countdownValue = 60f;
                    AudioManager.Instance.StopMusic();
                    AudioManager.Instance.SadMusic();
                }
            }
        }

//**********START GAME**********// 
        public void StartGame(){
            isGamePaused = false;
            gameStart = true;
        }

//**********PAUSE METHODS**********// 
        public bool AskIfGamePaused(){ return isGamePaused; }
        public void SetPauseState(bool value) { isGamePaused = value; }

        public void PauseGame(){
            PlayerController.Instance.speed = 0; //STOP THE PLAYER
            ScreenManager.Instance.AddScreen(GameScreens.Pause);
            isGamePaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void UnpauseGame(){
            PlayerController.Instance.speed = 15; //UNSTOP THE PLAYER
            ScreenManager.Instance.RemoveScreen(GameScreens.Pause);
            isGamePaused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

//**********TIMMER METHODS**********// 
        public void StartTimmer(){
            timmerRunning = true;  
        }
        public void StopTimmer(){
            timmerRunning = false;
        }
        public bool AskIfTimmerRunning(){
            return timmerRunning;
        }
        public float GetTimeLeft(){
            return countdownValue;
        }

//**********PUNTUATION**********// 
        public int GetPoints(){
            return points;
        }

        public void ModifyPoints(int num){
            points += num;
            if (points < 0) { points = 0; }
            pointsText.text = points.ToString();
        }

        public static float Round(float value, int digits)
        {
            float mult = Mathf.Pow(10.0f, (float)digits);
            return Mathf.Round(value * mult) / mult;
        }

//*********RESET GAME**********// 
        public void ResetGame(){
            SceneManager.LoadScene("gameScene");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        }
    }
}

