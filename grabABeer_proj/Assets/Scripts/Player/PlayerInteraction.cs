using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;

namespace Duarto.GrabABeer.Player {
    public class PlayerInteraction : MonoBehaviour {

        //LayerMasks
        LayerMask interactableLM;
        LayerMask highLightLM;

        [Header("Raycast")] //Raycast
        public Transform camPosition;
        RaycastHit hit;
        GameObject currentTarget;
        
        [Header("Magnet effect")] //Magnet effect
        public float AtractorSpeed;
        
        //Input buffer
        private List<ActionItem> inputBuffer = new List<ActionItem>();

//**********AWAKE**********//
        void Awake(){
            interactableLM = LayerMask.NameToLayer("Interactable");
            highLightLM = LayerMask.NameToLayer("Highlight");
        }

//**********UPDATE**********//
        private void Update()
        {
            if(!GameManager.Instance.AskIfGamePaused()) { //if the game isn't paused
                CreateRay();
                AddInput();
                if(hit.collider != null){
                    CanDoAction();
                }
            }   
        }

//**********CREATE RAYCAST**********//
        void CreateRay(){ //Select the item to interact with
            if(Physics.Raycast(camPosition.position, camPosition.TransformDirection(Vector3.forward), out hit, 5f, LayerMask.GetMask("Interactable","Highlight"))){
                
                GameObject target = hit.collider.gameObject;
                GameObject lastTarget;

                if(currentTarget != target) {
                    if(currentTarget != null){
                        lastTarget = currentTarget;
                        currentTarget = target;
                        currentTarget.layer = highLightLM;
                        lastTarget.layer = interactableLM;
                    } else {
                        currentTarget = target;
                        currentTarget.layer = highLightLM;
                    }
                }
    
            } else if(currentTarget != null) {
                currentTarget.layer = interactableLM;
                currentTarget = null;
            }
        }

//**********ADD INPUT (to Input Buffer)**********//
        private void AddInput(){
            if(Input.GetButtonDown("Interact")){
                inputBuffer.Add(new ActionItem(Time.time));
            }
        }

//**********CHECK IS THE ACTION HAS EXPIRED**********//
        void CanDoAction() {
            if(inputBuffer!=null){
                foreach (ActionItem ai in inputBuffer.ToArray()){
                    inputBuffer.Remove(ai);
                    if(ai.CheckIfValid()){
                        TakeItem();
                        break;
                    }
                }
            }
        }

//**********ACTION OF TAKING THE ITEM (diference between beer and other items)**********//
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

        //MAGNET EFFECT
        IEnumerator Co_MoveItemTowardsPlayer(RaycastHit item) { 
            while(item.collider.gameObject.transform.position != transform.position) {
                yield return new WaitForEndOfFrame();
                item.collider.gameObject.transform.position = Vector3.MoveTowards(item.collider.gameObject.transform.position,transform.position,AtractorSpeed * Time.deltaTime);
            }
            item.collider.gameObject.SetActive(false);
        }
    }

//************************************************************************************************************************//
    //CLASS ACTION ITEM
    public class ActionItem {
        public float time;
        public static float timeBeforeExpire = 0.2f;

        public ActionItem(float stamp) {
            time = stamp;
        }

        public bool CheckIfValid(){
            if(time + timeBeforeExpire >= Time.time) {
                return true;
            }
            return false;
        }
    }
//************************************************************************************************************************//
}