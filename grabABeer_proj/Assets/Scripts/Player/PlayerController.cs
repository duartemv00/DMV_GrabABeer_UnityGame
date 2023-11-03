using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;
using Duarto.Utilities;

namespace Duarto.GrabABeer.Player {
    public class PlayerController : Singleton<PlayerController> {

        [Header("Movement")] //Movement
        public float speed = 15; //player speed
        public float groundDrag; //Floor resistance to movement

        [Header("Gound Check")] //Check if the player is on the
        public float playerHeight;
        public LayerMask whatIsGround;
        bool grounded;

        [Header("Cam orientation")]
        public Transform orientation;

        float horizontalInput;
        float verticalInput;
        Vector3 moveInput;

        private Rigidbody playerRB; 


//********** START **********//
        void Start(){
            playerRB = GetComponent<Rigidbody>(); //get rigidbody
            playerRB.freezeRotation = true; //stop all rotations on the player parent game object
        }

//********** FIXED UPDATE **********//
        void Update(){
            if(!GameManager.Instance.AskIfGamePaused()) {
                MyInput();
                SpeedControl();
            }
            
        }
    //********** IS GROUNDED? **********//
        void Grounded(){
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
            if(grounded){
                playerRB.drag = groundDrag;
            } else {
                playerRB.drag = 0;
            }
        }

    //********** MY INPUT **********//
        void MyInput(){
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

//********** FIXED UPDATE **********//
        void FixedUpdate(){
            Grounded(); //Detect if the player is touching the ground (Activate/Deactivate drag)
            MovePlayer();
        }

        private void MovePlayer(){
            moveInput = orientation.forward * verticalInput + orientation.right * horizontalInput;
            playerRB.AddForce(moveInput.normalized * speed * 10f, ForceMode.Force);
        }

        private void SpeedControl(){
            Vector3 flatVel = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);
            if (flatVel.magnitude > speed) {
                Vector3 limitedVel = flatVel.normalized * speed;
                playerRB.velocity = new Vector3(limitedVel.x, playerRB.velocity.y,limitedVel.z);
            }
        }
    }
}