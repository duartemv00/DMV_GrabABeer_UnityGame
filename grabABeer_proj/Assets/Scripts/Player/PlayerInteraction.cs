using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Player {
    public class PlayerInteraction : MonoBehaviour {

        [Header("Interact")] //Interact
        public LayerMask interactableLayerMask;
        public Transform camPosition;
        public float AtractorSpeed;
        RaycastHit hit;

        private void Update()
        {
            if(!GameManager.Instance.AskIfGamePaused()) { //if the game isn't paused
                CreateRay();
                if((Input.GetButtonDown("Interact")) || ((Input.GetKeyDown(KeyCode.Mouse0)))){
                    TakeItem();
                }
            }   
        }

        void CreateRay(){
            if(Physics.Raycast(camPosition.position, camPosition.TransformDirection(Vector3.forward), out hit, 5f, interactableLayerMask)){
                //codigo del outline
            }  
        }

        void TakeItem(){
            if (hit.collider.tag == "item") {
                AudioManager.Instance.TakeItem();
                GameManager.Instance.ModifyPoints(-1);
                    StartCoroutine(Co_MoveItemTowardsPlayer(hit));
            } else if (hit.collider.tag == "beer"){
                AudioManager.Instance.TakeBeer();
                GameManager.Instance.ModifyPoints(1);
                StartCoroutine(Co_MoveItemTowardsPlayer(hit));
            }
        }

        IEnumerator Co_MoveItemTowardsPlayer(RaycastHit item) {
            while(item.collider.gameObject.transform.position != transform.position) {
                yield return new WaitForEndOfFrame();
                item.collider.gameObject.transform.position = Vector3.MoveTowards(item.collider.gameObject.transform.position,transform.position,AtractorSpeed * Time.deltaTime);
            }
            item.collider.gameObject.SetActive(false);
        }
    }
}

