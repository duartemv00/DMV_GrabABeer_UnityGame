using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {
    public class GameScreen : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform clock;
        public RectTransform fadeOut;

        //SCREEM COMPONENTS POSITIONS
        Vector2 clockAnchored;
        Vector2 healthAnchored;

//******************************************************************************************************************************************// 
        public override void SetParameters(){
            //stablish initial positions
            clockAnchored = clock.anchoredPosition;
        }
        public override void ResetPositions(){
            clock.anchoredPosition = clockAnchored;
        }

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            //clock.anchoredPosition = new Vector2(clockAnchored.x,clockAnchored.y + Screen.height * 2);

            while(fadeOut.GetComponent<CanvasGroup>().alpha > 0) {
                yield return new WaitForEndOfFrame();
                fadeOut.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();

            isAnimationRunning = false;
            GameManager.Instance.StartGame();

            //entran titulo y botones
            /*clock.DOAnchorPosY(clockAnchored.y,0.8f).SetEase(Ease.InOutBack).OnComplete(() => {   
                isAnimationRunning = false;
                GameManager.Instance.StartGame();
            });*/   
        }

//*****HIDE SCREEN LOGIC********************************************************************************************************************// 
        public override void Hide(){
            StartCoroutine(Co_Hide());
        }
        IEnumerator Co_Hide(){
            isAnimationRunning = true;
            yield return new WaitForEndOfFrame();
            isAnimationRunning = false;
            myScreen.SetActive(false);     
        }
    }
}
