using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {
    public class IntroScreen : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform logoDuarto0;
        public RectTransform text;
        public RectTransform logoJam;
        public RectTransform logoKenney;

//******************************************************************************************************************************************// 
        public override void ResetPositions(){
            base.ResetPositions();
        }

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            //cambiar posiciones y alpha del logo y titulo
            logoDuarto0.GetComponent<CanvasGroup>().alpha = 0;
            text.GetComponent<CanvasGroup>().alpha = 0;
            logoJam.GetComponent<CanvasGroup>().alpha = 0;
            logoKenney.GetComponent<CanvasGroup>().alpha = 0;
            yield return new WaitForSeconds(1);

            //LOGO DUARTO0
            while(logoDuarto0.GetComponent<CanvasGroup>().alpha < 1) {
                yield return new WaitForEndOfFrame();
                logoDuarto0.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
                text.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
            }
            yield return new WaitForSeconds(1);
            while(logoDuarto0.GetComponent<CanvasGroup>().alpha > 0) {
                yield return new WaitForEndOfFrame();
                logoDuarto0.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
                text.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            }
            //LOGO DE LA JAM Y KENNEY
            while(logoJam.GetComponent<CanvasGroup>().alpha < 1) {
                yield return new WaitForEndOfFrame();
                logoJam.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
                logoKenney.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
            }
            yield return new WaitForSeconds(1);
            while(logoJam.GetComponent<CanvasGroup>().alpha > 0) {
                yield return new WaitForEndOfFrame();
                logoJam.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
                logoKenney.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            }

            ScreenManager.Instance.ChangeScreen(GameScreens.MainMenu,myTypeScreen);
        }

//*****HIDE SCREEN LOGIC********************************************************************************************************************// 
        public override void Hide(){
            isAnimationRunning = true;
            base.Hide();
            StartCoroutine(Co_Hide());
        }

        IEnumerator Co_Hide(){
            yield return new WaitForSeconds(1f);
            isAnimationRunning = false;
            myScreen.SetActive(false);
        }
    }
}
