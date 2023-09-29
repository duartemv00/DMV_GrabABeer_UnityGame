using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {
    public class MainMenu : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform logo;
        public RectTransform title;
        public RectTransform playBtn;
        public RectTransform settingsBtn;
        public RectTransform quitBtn;

        //SCREEM COMPONENTS POSITIONS
        Vector2 logoAnchored;
        Vector2 titleAnchored;
        Vector2 btnPlayAnchored;
        Vector2 btnSettingsAnchored;
        Vector2 btnQuitAnchored;

//******************************************************************************************************************************************// 
        public override void SetParameters(){
            //stablish initial positions
            titleAnchored = title.anchoredPosition;
            btnPlayAnchored = playBtn.anchoredPosition;
            btnSettingsAnchored = settingsBtn.anchoredPosition;
            logoAnchored = logo.anchoredPosition;
            btnQuitAnchored = quitBtn.anchoredPosition;
        }
        public override void ResetPositions(){
            title.anchoredPosition = titleAnchored;
            playBtn.anchoredPosition = btnPlayAnchored;
            settingsBtn.anchoredPosition = btnSettingsAnchored;
            logo.anchoredPosition = logoAnchored;
            quitBtn.anchoredPosition = btnQuitAnchored;
        }

//*****LOGIC OF THE BUTTONS*****************************************************************************************************************//
        public void StartGame(){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ScreenManager.Instance.ChangeScreen(GameScreens.Cinematic,myTypeScreen);
        }
        public void OpenSettings(){
            //ScreenManager.Instance.ChangeScreen(GameScreens.Settings,myTypeScreen);
        }
        public void QuitGame(){
            Application.Quit();
        }

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            Cursor.visible = true;
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            logo.GetComponent<CanvasGroup>().alpha = 0;
            title.anchoredPosition = new Vector2(titleAnchored.x,titleAnchored.y + Screen.height * 2);
            playBtn.anchoredPosition = new Vector2(btnPlayAnchored.x - Screen.width * 2,btnPlayAnchored.y);
            quitBtn.anchoredPosition = new Vector2(btnQuitAnchored.x - Screen.width * 2,btnQuitAnchored.y);
            settingsBtn.anchoredPosition = new Vector2(btnSettingsAnchored.x - Screen.width * 2,btnSettingsAnchored.y);
            yield return new WaitForEndOfFrame();

            //mostrar logo
            while(logo.GetComponent<CanvasGroup>().alpha < 1) {
                yield return new WaitForEndOfFrame();
                logo.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
            }

            //entran titulo y botones
            title.DOAnchorPosY(titleAnchored.y,0.8f).SetEase(Ease.InOutBack);
            playBtn.DOAnchorPosX(btnPlayAnchored.x,0.8f).SetDelay(0.3f).SetEase(Ease.InOutBack);
            settingsBtn.DOAnchorPosX(btnSettingsAnchored.x,0.8f).SetDelay(0.5f).SetEase(Ease.InOutBack);
            quitBtn.DOAnchorPosX(btnQuitAnchored.x,0.8f).SetDelay(0.7f).SetEase(Ease.InOutBack).OnComplete(() => {  
                isAnimationRunning = false;
            });     
        }

//*****HIDE SCREEN LOGIC********************************************************************************************************************// 
        public override void Hide(){
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            isAnimationRunning = true;
            yield return new WaitForEndOfFrame();
            title.DOAnchorPosY(Screen.height * 2,0.8f).SetEase(Ease.InOutBack);
            playBtn.DOAnchorPosX(-Screen.width * 2,0.8f).SetDelay(0.3f).SetEase(Ease.InOutBack);
            settingsBtn.DOAnchorPosX(-Screen.width * 2,0.8f).SetDelay(0.5f).SetEase(Ease.InOutBack);
            quitBtn.DOAnchorPosX(-Screen.width * 2,0.8f).SetDelay(0.7f).SetEase(Ease.InOutBack).OnComplete(() => {
                isAnimationRunning = false;
                myScreen.SetActive(false);
            });     
        }
    }
}
