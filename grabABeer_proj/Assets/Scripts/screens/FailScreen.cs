using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;
using TMPro;

namespace Duarto.GrabABeer.Screens {
    public class FailScreen : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform menu;

        //SCREEM COMPONENTS POSITIONS
        Vector2 menuAnchored;

//******************************************************************************************************************************************// 
        public override void SetParameters(){
            //stablish initial positions
            menuAnchored = menu.anchoredPosition;
        }
        public override void ResetPositions(){
            menu.anchoredPosition = menuAnchored;
        }

//*****LOGIC OF THE BUTTONS*****************************************************************************************************************//
        public void Again(){
            GameManager.Instance.ResetGame();
        }
        public void QuitGame(){
            Application.Quit();
        }

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            menu.anchoredPosition = new Vector2(menuAnchored.x,menuAnchored.y + Screen.height * 2);
            yield return new WaitForEndOfFrame();

            //entran titulo y botones
            menu.DOAnchorPosY(menuAnchored.y,0.8f).SetEase(Ease.InOutBack).OnComplete(() => {  
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
            menu.DOAnchorPosY(Screen.height * 2,0.8f).SetEase(Ease.InOutBack).OnComplete(() => {
                isAnimationRunning = false;
                myScreen.SetActive(false);
            });
        }
    }
}
