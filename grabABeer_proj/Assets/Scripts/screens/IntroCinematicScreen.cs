using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Duarto.GrabABeer.Manager;
using UnityEngine.SceneManagement;

namespace Duarto.GrabABeer.Screens {
    public class IntroCinematicScreen : ScreenWindow {
        
        //Screen components
        public RectTransform cinematic;
        public RectTransform frame01;
        public RectTransform frame02;
        public RectTransform frame03;
        public RectTransform frame04;
        public RectTransform frame05;
        public RectTransform frame06;
        public RectTransform frame07;
        public RectTransform frame08;
        public RectTransform hintText;
        public RectTransform fadeOutScreen;

        //Screen components anchors
        Vector2 cinematicAnchored;

//********************// 
        public override void SetParameters(){
            cinematicAnchored = cinematic.anchoredPosition;
        }
        public override void ResetPositions(){
            cinematic.anchoredPosition = cinematicAnchored;
        }

//**********UPDATE **********//
        void Update(){
            if(Input.anyKey){ //Skip cutscene
                Hide();
            }
        }

//**********SHOW SCREEN ANIMATION********************// 
        public override void Show(){
            base.Show();
            StartCoroutine(Co_InitSequence());
            StartCoroutine(Co_HintText());
        }
        IEnumerator Co_HintText(){
            while(true) {
                while(hintText.GetComponent<CanvasGroup>().alpha < 0.5) {
                    hintText.GetComponent<CanvasGroup>().alpha += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(1f);
                while(hintText.GetComponent<CanvasGroup>().alpha > 0) {
                    hintText.GetComponent<CanvasGroup>().alpha -= Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                };
            }
        }
        IEnumerator Co_InitSequence(){ // This function activate an animation that only is shown when the user open the app
            isAnimationRunning = true;

            fadeOutScreen.GetComponent<CanvasGroup>().alpha = 0;
            frame02.GetComponent<CanvasGroup>().alpha = 0;
            frame03.GetComponent<CanvasGroup>().alpha = 0;
            frame04.GetComponent<CanvasGroup>().alpha = 0;
            frame05.GetComponent<CanvasGroup>().alpha = 0;
            frame06.GetComponent<CanvasGroup>().alpha = 0;
            frame07.GetComponent<CanvasGroup>().alpha = 0;
            frame08.GetComponent<CanvasGroup>().alpha = 0;
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
            yield return new WaitForSeconds(0.5f);
            frame05.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(0.5f);
            frame06.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(0.5f);
            frame07.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(0.5f);
            frame08.GetComponent<CanvasGroup>().alpha = 1;
            yield return new WaitForSeconds(2f);
            Hide();
        }

//***************HIDE SCREEN ANIMATION********************// 
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
            SceneManager.LoadScene("gameScene");
        }
    }
}