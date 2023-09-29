using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Duarto.GrabABeer.Player{
    public class MoveCamera : MonoBehaviour{
        public Transform cameraPosition;

        void Update(){
            transform.position = cameraPosition.position;
        }
    }
}
