using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {
    public class HUD : ScreenWindow {
        
        //SCREEN COMPONENTS
        public RectTransform banner;

//*****SHOW SCREEN LOGIC********************************************************************************************************************// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            yield return new WaitForEndOfFrame();

            isAnimationRunning = false;
            GameManager.Instance.StartGame();
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
