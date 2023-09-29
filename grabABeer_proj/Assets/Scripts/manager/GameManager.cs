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
        private bool isGamePaused = true;
        bool gameStart = false;
        public TextMeshProUGUI pointsText;
        int points = 0;
        public float countdownValue = 60;
        public TextMeshProUGUI countdownText;
        bool timmerRunning = false;
        public bool goingFromGameplay;

//*****START METHOD**************************************************************************************************************************// 
        void Awake() {
            Application.targetFrameRate = 60;
            Cursor.visible = false;
        }
        
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

        void Update(){
            if((countdownValue >= 0f)&&timmerRunning&&!isGamePaused) {
                countdownValue -= Time.deltaTime; //update countdown
                countdownText.text = Mathf.Round(countdownValue).ToString();
            }
            if (countdownValue <= 0.1f) { 
                Debug.Log("PERDISTE"); 
                timmerRunning = false;
                isGamePaused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                ScreenManager.Instance.AddScreen(GameScreens.Fail); 
                countdownValue = 60f;
            }

            if(gameStart){
                if(Input.GetButtonDown("Pause")) {
                    if(isGamePaused) {
                        ScreenManager.Instance.RemoveScreen(GameScreens.Pause);
                        isGamePaused = false;
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    } else {
                        ScreenManager.Instance.AddScreen(GameScreens.Pause);
                        isGamePaused = true;
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
            }
        }


//******************************************************************************************************************************************// 
        public void StartGame(){
            isGamePaused = false;
            gameStart = true;
        }

        public bool AskIfGamePaused(){ return isGamePaused; }
        public void SetPauseState(bool value) { isGamePaused = value; }

        public void ModifyPoints(int num){
            points += num;
            if (points < 0) { points = 0; }
            pointsText.text = points.ToString();
        }

//******************************************************************************************************************************************// 
        public void StartTimmer(){
            timmerRunning = true;
            StartCoroutine(Co_TimmerRuns());
        }
        IEnumerator Co_TimmerRuns(){
            if((countdownValue >= 0f)&&timmerRunning) {
                countdownValue -= Time.deltaTime; //update countdown
                countdownText.text = countdownValue.ToString();
                yield return new WaitForEndOfFrame();
            }
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

        public int GetPoints(){
            return points;
        }

        public void ResetGame(){
            SceneManager.LoadScene("secondScene");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked; 
        }
    }
}

