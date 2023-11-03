using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Screens {

    public class ScreenWindow : MonoBehaviour
    {
        public bool isAnimationRunning;
        public GameObject myScreen;
        public GameScreens myTypeScreen;
        public bool parametersAreSet;

//******************************************************************************************************************************************// 
        public virtual void SetParameters() { } //Function called the first time that the class is activated to do a set in the different parameters that are necessary, like vectors or recTtransform

//******************************************************************************************************************************************// 
        public virtual void ResetPositions() { } // This function is called always that the class is activated when the new screen is showed

//******************************************************************************************************************************************// 
        public virtual void Show() { // This function is called when the screen is activated. This function has the differents animations and effects that are need to show the elements. When the aniamtions have finished isAnimationActive must to change to false.
            myScreen.SetActive(true);
            if(!GetParametersAreSet()) {
                SetParameters();
                SetParametersAreSet(true);
            }
            ResetPositions();
        }

//******************************************************************************************************************************************// 
        public virtual void Hide() { } // This function is called when the screen hide. This function has the differents animations and effects that are need to hide the elements. When the aniamtions have finished isAnimationActive must to change to false.
        

//******************************************************************************************************************************************// 
        public bool GetParametersAreSet() {
            return parametersAreSet;
        }

//******************************************************************************************************************************************// 
        public void SetParametersAreSet(bool value) {
            parametersAreSet = value;
        }
    }
}
