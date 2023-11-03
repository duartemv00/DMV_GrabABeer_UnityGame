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
        public RectTransform mesa;
        public RectTransform hand;
        public RectTransform title;
        public RectTransform playBtn;
        public RectTransform settingsBtn;
        public RectTransform quitBtn;


        //SCREEN COMPONENTS POSITIONS
        Vector2 logoAnchored;
        Vector2 mesaAnchored;
        Vector2 handAnchored;
        Vector2 titleAnchored;
        Vector2 btnPlayAnchored;
        Vector2 btnSettingsAnchored;
        Vector2 btnQuitAnchored;

//**********// 
        public override void SetParameters(){
            //stablish initial positions
            titleAnchored = title.anchoredPosition;
            btnPlayAnchored = playBtn.anchoredPosition;
            btnSettingsAnchored = settingsBtn.anchoredPosition;
            logoAnchored = logo.anchoredPosition;
            btnQuitAnchored = quitBtn.anchoredPosition;
            mesaAnchored = mesa.anchoredPosition;
            handAnchored = hand.anchoredPosition;
        }
        public override void ResetPositions(){
            title.anchoredPosition = titleAnchored;
            playBtn.anchoredPosition = btnPlayAnchored;
            settingsBtn.anchoredPosition = btnSettingsAnchored;
            logo.anchoredPosition = logoAnchored;
            quitBtn.anchoredPosition = btnQuitAnchored;
            mesa.anchoredPosition = mesaAnchored;
            hand.anchoredPosition = handAnchored;
        }

//********** LOGIC OF THE BUTTONS **********//
        public void StartGame(){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            ScreenManager.Instance.ChangeScreen(GameScreens.Cinematic,myTypeScreen);
        }
        public void OpenSettings(){
            ScreenManager.Instance.AddScreen(GameScreens.Settings);
        }
        public void QuitGame(){
            Application.Quit();
        }

//********** SHOW MainMenu ANIMATION **********// 
        public override void Show(){
            base.Show();
            Cursor.visible = true;
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            title.anchoredPosition = new Vector2 (titleAnchored.x,titleAnchored.y + Screen.height * 2);
            logo.anchoredPosition = new Vector2 (logoAnchored.x,logoAnchored.y + Screen.height * 2);
            mesa.anchoredPosition = new Vector2 (mesaAnchored.x,mesaAnchored.y - Screen.height * 2);
            hand.anchoredPosition = new Vector2(handAnchored.x + Screen.width * 2, handAnchored.y);
            playBtn.anchoredPosition = new Vector2(btnPlayAnchored.x - Screen.width * 2,btnPlayAnchored.y);
            quitBtn.anchoredPosition = new Vector2(btnQuitAnchored.x - Screen.width * 2,btnQuitAnchored.y);
            settingsBtn.anchoredPosition = new Vector2(btnSettingsAnchored.x - Screen.width * 2,btnSettingsAnchored.y);
            yield return new WaitForEndOfFrame();

            //entran titulo y botones
            logo.DOAnchorPosY(logoAnchored.y,0.5f);
            mesa.DOAnchorPosY(mesaAnchored.y,0.3f);
            title.DOAnchorPosY(titleAnchored.y,0.8f);
            playBtn.DOAnchorPosX(btnPlayAnchored.x,0.8f).SetDelay(0.3f);
            settingsBtn.DOAnchorPosX(btnSettingsAnchored.x,0.8f).SetDelay(0.5f);
            quitBtn.DOAnchorPosX(btnQuitAnchored.x,0.8f).SetDelay(0.7f).OnComplete(() => {  
                isAnimationRunning = false;
            });
        }

//********** HIDE MainMenu ANIMATION **********// 
        public override void Hide(){
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            isAnimationRunning = true;
            yield return new WaitForEndOfFrame();
            title.DOAnchorPosY(Screen.height * 2,0.8f);
            playBtn.DOAnchorPosX(-Screen.width * 2,0.8f).SetDelay(0.3f);
            settingsBtn.DOAnchorPosX(-Screen.width * 2,0.8f).SetDelay(0.5f);
            quitBtn.DOAnchorPosX(-Screen.width * 2,0.8f).SetDelay(0.7f);

            hand.DOAnchorPosX(handAnchored.x,0.8f);
            yield return new WaitForSeconds(1f);
            logo.DOAnchorPosX(Screen.width * 2,0.5f);
            mesa.DOAnchorPosY(-Screen.height * 2,0.3f);
            hand.DOAnchorPosX(Screen.width * 2,0.8f).OnComplete(() => {
                isAnimationRunning = false;
                myScreen.SetActive(false);
            });     
        }
    }
}
