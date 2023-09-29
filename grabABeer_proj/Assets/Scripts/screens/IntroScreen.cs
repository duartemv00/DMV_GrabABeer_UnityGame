using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {
    public class IntroScreen : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform logo;
        public RectTransform text;

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
            logo.GetComponent<CanvasGroup>().alpha = 0;
            text.GetComponent<CanvasGroup>().alpha = 0;
            yield return new WaitForSeconds(1);

            //mostrar logo
            while(logo.GetComponent<CanvasGroup>().alpha < 1) {
                yield return new WaitForEndOfFrame();
                logo.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
            }
            while(text.GetComponent<CanvasGroup>().alpha < 1) {
                yield return new WaitForEndOfFrame();
                text.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
            }
            yield return new WaitForSeconds(2);

            //ocultar logo
            while(logo.GetComponent<CanvasGroup>().alpha > 0) {
                yield return new WaitForEndOfFrame();
                logo.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
                text.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            }
            ScreenManager.Instance.ChangeScreen(GameScreens.MainMenu,myTypeScreen);
        }

//*****HIDE SCREEN LOGIC********************************************************************************************************************// 
        public override void Hide(){
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
