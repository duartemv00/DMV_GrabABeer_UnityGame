using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.Utilities;
using Duarto.GrabABeer.Player;
using Duarto.GrabABeer.Screens;

namespace Duarto.GrabABeer.Manager {

public enum GameScreens {None,Intro,MainMenu,Settings,Cinematic,Game,HUD,Pause,Points,Fail}

    public class ScreenManager : Singleton<ScreenManager>
    {
        public GameScreens typeScreen;
        [System.NonSerialized] public bool isAnim;
        public GameObject raycastShield; //avoid player from clicking on screen
        public List<ScreenWindow> screens = new List<ScreenWindow>();

//**********AWAKE**********//
        void Awake() {
            foreach(ScreenWindow i in screens) { //deactivate all screens at the begining
                i.gameObject.SetActive(false);
            }
        }

//**********SCREEN SWAP**********//
        public void ChangeScreen(GameScreens newScreen, GameScreens oldScreen){
            StartCoroutine(WaitBeforeNewScreen(newScreen, oldScreen));
        }
        IEnumerator WaitBeforeNewScreen(GameScreens newScreen, GameScreens oldScreen) {
            raycastShield.SetActive(true);

            ScreenWindow newScreenGO = GetScreen(newScreen);
            ScreenWindow oldScreenGO = GetScreen(oldScreen);

            if(oldScreenGO != null) {
                oldScreenGO.Hide();
                while (oldScreenGO.isAnimationRunning) {
                    raycastShield.SetActive(true);
                    isAnim = true;
                    yield return new WaitForEndOfFrame();
                }
            }

            if(newScreenGO != null) {
                newScreenGO.Show();
                while (newScreenGO.isAnimationRunning) {
                    raycastShield.SetActive(true);
                    yield return new WaitForEndOfFrame();
                }
                isAnim = false;
                raycastShield.SetActive(false);
                
            }        
        }

//**********ADD SCREEN**********//
        public void AddScreen(GameScreens newScreen){
            StartCoroutine(WaitBeforeAddScreen(newScreen));
        }
        IEnumerator WaitBeforeAddScreen(GameScreens newScreen) {
            raycastShield.SetActive(true);

            ScreenWindow newScreenGO = GetScreen(newScreen);

            if(newScreenGO != null) {
                newScreenGO.Show();
                while (newScreenGO.isAnimationRunning) {
                    isAnim = true;
                    yield return new WaitForEndOfFrame();
                } isAnim = false;
            }
            raycastShield.SetActive(false);   
        }

//**********REMOVE SCREEN**********//
        public void RemoveScreen(GameScreens oldScreen){
            StartCoroutine(WaitBeforeRemoveScreen(oldScreen));
        }
        IEnumerator WaitBeforeRemoveScreen(GameScreens oldScreen) {
            raycastShield.SetActive(true);

            ScreenWindow oldScreenGO = GetScreen(oldScreen);

            if(oldScreenGO != null) {
                oldScreenGO.Hide();
                while (oldScreenGO.isAnimationRunning) {
                    isAnim = true;
                    yield return new WaitForEndOfFrame();
                }
                isAnim = false;
            }

            raycastShield.SetActive(false);   
        }



//**********GET SCREEN**********//
        public ScreenWindow GetScreen(GameScreens typeScreen){
            foreach(ScreenWindow i in screens){
                if(typeScreen == i.myTypeScreen) {
                    return i;
                }
            }
            return null;
        }

    }

}

