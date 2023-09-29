using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.Utilities;
using Duarto.GrabABeer.Player;
using Duarto.GrabABeer.Screens;

namespace Duarto.GrabABeer.Manager {

public enum GameScreens {None,Intro,MainMenu,Settings,Cinematic,Game,Pause,Points,Fail}

    public class ScreenManager : Singleton<ScreenManager>
    {
        public GameScreens typeScreen;
        public GameObject raycastShield;
        public List<ScreenWindow> screens = new List<ScreenWindow>();

//*****AWAKE METHOD*************************************************************************************************************************//
        void Awake() {
            foreach(ScreenWindow i in screens) { //deactivate all screens at the begining
                i.gameObject.SetActive(false);
            }
        }

//*****SCREEN SWAP LOGIC********************************************************************************************************************//
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
                    yield return new WaitForEndOfFrame();
                }
            }

            if(newScreenGO != null) {
                newScreenGO.Show();
                while (newScreenGO.isAnimationRunning) {
                    yield return new WaitForEndOfFrame();
                }
            }

            raycastShield.SetActive(false);   
        }
        ScreenWindow GetScreen(GameScreens typeScreen){
            foreach(ScreenWindow i in screens){
                if(typeScreen == i.myTypeScreen) {
                    return i;
                }
            }
            return null;
        }


        public void AddScreen(GameScreens newScreen){
            StartCoroutine(WaitBeforeAddScreen(newScreen));
        }
        IEnumerator WaitBeforeAddScreen(GameScreens newScreen) {
            raycastShield.SetActive(true);

            ScreenWindow newScreenGO = GetScreen(newScreen);

            if(newScreenGO != null) {
                newScreenGO.Show();
                while (newScreenGO.isAnimationRunning) {
                    yield return new WaitForEndOfFrame();
                }
            }

            raycastShield.SetActive(false);   
        }


        public void RemoveScreen(GameScreens oldScreen){
            StartCoroutine(WaitBeforeRemoveScreen(oldScreen));
        }
        IEnumerator WaitBeforeRemoveScreen(GameScreens oldScreen) {
            raycastShield.SetActive(true);

            ScreenWindow oldScreenGO = GetScreen(oldScreen);

            if(oldScreenGO != null) {
                oldScreenGO.Hide();
                while (oldScreenGO.isAnimationRunning) {
                    yield return new WaitForEndOfFrame();
                }
            }

            raycastShield.SetActive(false);   
        }
    }

}

