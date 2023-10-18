using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.Utilities;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Duarto.GrabABeer.Manager {

    //This class control all game's sound
    public class AudioManager :  Singleton<AudioManager>{ 
        public AudioSource as_click; //SFX when clicking a button
        public AudioSource as_takeBeer;
        public AudioSource as_cashRegister;
        public List<AudioClip> ac_takeItem = new List<AudioClip>();
        public AudioSource as_takeItem;
        public AudioSource as_passPage;

        //VOLUME SETTINGS
        public AudioMixer mixer;
        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider SFXVolumeSlider;

//************************************************************************************************
        private void Start() {
            if(PlayerPrefs.HasKey("masterVolume")) {

            } else {
                PlayerPrefs.SetFloat("masterVolume",1);
            }
            if(PlayerPrefs.HasKey("musicVolume")) {

            } else {
                PlayerPrefs.SetFloat("musicVolume",1);
            }
            if(PlayerPrefs.HasKey("sfxVolume")) {

            } else {
                PlayerPrefs.SetFloat("sfxVolume",1);
            }
            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
            ChangeMasterVolumenBySlider();
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            ChangeMusicVolumenBySlider();
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            ChangeSFXVolumenBySlider();
            
        }

//************************************************************************************************
        public void Click() { // This function active a click sound when the user do a click in a button
            as_click.Play();
        }

        public void TakeBeer() {
            as_takeBeer.Play();
        }

        public void TakeItem() { // This function active a sound when the card is moved to a side
            as_takeItem.clip = ac_takeItem[Random.Range(0,ac_takeItem.Count)];
            as_takeItem.pitch = Random.Range(1,1.2f);
            as_takeItem.Play();
        }

        public void PassPage() { // This function activates a shuffling sound when the game starts.
            as_passPage.Play();
        }

        public void CashRegister() {
            as_cashRegister.Play();
        }

//************************************************************************************************
        public void ChangeMasterVolumenBySlider() { // Modify the sound volume 
            mixer.SetFloat("MasterVolume",Mathf.Log10(masterVolumeSlider.value)*20);
            PlayerPrefs.SetFloat("masterVolume",masterVolumeSlider.value);
        }

        public void ChangeMusicVolumenBySlider() { // Modify the sound volume 
            mixer.SetFloat("MusicVolume",Mathf.Log10(musicVolumeSlider.value)*20);
            PlayerPrefs.SetFloat("musicVolume",musicVolumeSlider.value);
        }

        public void ChangeSFXVolumenBySlider() { // Modify the sound volume 
            mixer.SetFloat("SFXVolume",Mathf.Log10(SFXVolumeSlider.value)*20);
            PlayerPrefs.SetFloat("sfxVolume",SFXVolumeSlider.value);
        }
    }
}
