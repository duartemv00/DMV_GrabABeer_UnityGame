using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Player {
    public class PlayerInteraction : MonoBehaviour {

        [Header("Interact")] //Interact
        public LayerMask interactableLayerMask;
        public Transform camPosition;

        private void Update()
        {
            if(!GameManager.Instance.AskIfGamePaused()) {
                if(Input.GetButtonDown("Interact")){
                CreateRay();
                }
            }
            
        }

        void CreateRay(){
            RaycastHit hit;
            /*if(Physics.Raycast(camPosition.position, camPosition.TransformDirection(Vector3.forward), out hit, 5f, interactableLayerMask)){
                if (hit.collider.TryGetComponent(out IInteractable interactableObject)) {
                    interactableObject.Interact();
                }
            }*/

            if(Physics.Raycast(camPosition.position, camPosition.TransformDirection(Vector3.forward), out hit, 5f, interactableLayerMask)){
                if (hit.collider.tag == "item") {
                    GameManager.Instance.ModifyPoints(-1);
                    hit.collider.gameObject.SetActive(false);
                } else if (hit.collider.tag == "beer"){
                    GameManager.Instance.ModifyPoints(1);
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
    }
}

