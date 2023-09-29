using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {
    public class IntroCinematicScreen : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform cinematic;
        public RectTransform frame01;
        public RectTransform frame02;
        public RectTransform frame03;
        public RectTransform frame04;
        public RectTransform fadeOutScreen;

        //SCREEN COMPONENTS POSITIONS
        Vector2 cinematicAnchored;

//******************************************************************************************************************************************// 
        public override void SetParameters(){
            cinematicAnchored = cinematic.anchoredPosition;
        }
        public override void ResetPositions(){
            cinematic.anchoredPosition = cinematicAnchored;
        }

//*****UPDATE METHOD***********************************************************************************************************************//
        void Update(){
            if(Input.GetButtonDown("Attack")){ //SALTAR CINEMATICA
                ScreenManager.Instance.ChangeScreen(GameScreens.Game,myTypeScreen);
            }
        }

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            fadeOutScreen.GetComponent<CanvasGroup>().alpha = 0;
            frame02.GetComponent<CanvasGroup>().alpha = 0;
            frame03.GetComponent<CanvasGroup>().alpha = 0;
            frame04.GetComponent<CanvasGroup>().alpha = 0;
            cinematic.anchoredPosition = new Vector2(cinematicAnchored.x,cinematicAnchored.y+Screen.height*2);
            yield return new WaitForEndOfFrame();

            //cinematica
            cinematic.DOAnchorPosY(cinematicAnchored.y,0.8f).SetEase(Ease.InOutBack);
            yield return new WaitForSeconds(3f);
            frame02.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(3f);
            frame03.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(3f);
            frame04.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(3f);
            ScreenManager.Instance.ChangeScreen(GameScreens.Game,myTypeScreen);
        }

//*****HIDE SCREEN LOGIC********************************************************************************************************************// 
        public override void Hide(){
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            while(fadeOutScreen.GetComponent<CanvasGroup>().alpha < 1) {
                yield return new WaitForEndOfFrame();
                fadeOutScreen.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
            }
            yield return new WaitForSeconds(1f);
            isAnimationRunning = false;
            myScreen.SetActive(false);
        }
    }
}