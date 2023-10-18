using System.Collections;
using System.Collections.Generic;
using Duarto.GrabABeer.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Duarto.GrabABeer.Player{
    public class PlayerCam : MonoBehaviour
    {
        //SENSITIVITY
        public float sens;
        public Slider sensSlider;

        public Transform orientation;

        float xRotation;
        float yRotation;

        void Start(){
            if(PlayerPrefs.HasKey("sens")) {

            } else {
                PlayerPrefs.SetFloat("sens",1);
            }
            sensSlider.value = PlayerPrefs.GetFloat("sens");
            ChangeSens();
        }

        void Update()
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

            yRotation += mouseX;
            xRotation -=  mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            if(!GameManager.Instance.AskIfGamePaused()) {
                transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
                orientation.rotation = Quaternion.Euler(0, yRotation, 0);
            }
        }

        public void ChangeSens(){
            sens = sensSlider.value;
            PlayerPrefs.SetFloat("sens",sens);
        }
    }
} 

