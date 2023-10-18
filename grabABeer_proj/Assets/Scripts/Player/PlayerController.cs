using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.GrabABeer.Manager;
using Duarto.Utilities;

namespace Duarto.GrabABeer.Player {
    public class PlayerController : Singleton<PlayerController> {

        [Header("Movement")] //Movement
        public float currentSpeed = 5;
        public float groundDrag;

        [Header("Gound Check")] //Gound Check
        public float playerHeight;
        public LayerMask whatIsGround;
        bool grounded;

        public Transform orientation;

        float horizontalInput;
        float verticalInput;
        Vector3 moveInput;

        private Rigidbody playerRB; 


/********************************************************************/
        void Start(){
            playerRB = GetComponent<Rigidbody>();
            playerRB.freezeRotation = true;
        }

/********************************************************************/
        void Update(){
            if(!GameManager.Instance.AskIfGamePaused()) {
                MyInput();
                SpeedControl();

                grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
                if(grounded){
                    playerRB.drag = groundDrag;
                } else {
                    playerRB.drag = 0;
                }
            }
            
        }

        void MyInput(){
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        private void MovePlayer(){
            moveInput = orientation.forward * verticalInput + orientation.right * horizontalInput;
            playerRB.AddForce(moveInput.normalized * currentSpeed * 10f, ForceMode.Force);
        }

        void FixedUpdate(){
            MovePlayer();
        }

        private void SpeedControl(){
            Vector3 flatVel = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

            if (flatVel.magnitude > currentSpeed) {
                Vector3 limitedVel = flatVel.normalized * currentSpeed;
                playerRB.velocity = new Vector3(limitedVel.x, playerRB.velocity.y,limitedVel.z);
            }
        }
    }
}