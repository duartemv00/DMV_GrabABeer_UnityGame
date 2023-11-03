using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.Utilities;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Duarto.GrabABeer.Manager {

    //This class control all game's sound
    public class AudioManager :  Singleton<AudioManager>{

        [Header("Configuration components")] //Volume settings
        public AudioMixer mixer;
        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider SFXVolumeSlider;
        
        [Header("Audio Sources")]
        [Header("SFX")]
        public AudioSource as_click; //SFX when clicking a button
        public AudioSource as_back; //SFX when clicking a button back
        public AudioSource as_takeBeer;
        public AudioSource as_cashRegister;
        public List<AudioClip> ac_takeItem = new List<AudioClip>();
        public AudioSource as_takeItem;
        
        [Header("Ambience")]
        public AudioSource as_ambience;

        [Header("Music")]
        public AudioSource as_music;
        public AudioSource as_sadMusic;

//**********START**********//
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

//**********PLAY/STOP AUDIO**********//
        //MUSIC
        public void StartMusic() { //Supermarket music
            as_music.Play();
        }
        public void SadMusic() { //Sad (losing) music
            as_sadMusic.Play();
        }
        public void StopMusic() {
            as_music.Stop();
        }
        //NIGHT AMBIENCE
        public void StopAmbience() {
            as_ambience.Stop();
        }
        //UI SFX
        public void Click() {
            as_click.Play();
        }

        public void Back() {
            as_back.Play();
        }
        //INGAME SFX
        public void TakeBeer() {
            as_takeBeer.Play();
        }
        public void TakeItem() { //This function take a random sound for every time you take an item
            as_takeItem.clip = ac_takeItem[Random.Range(0,ac_takeItem.Count)];
            as_takeItem.pitch = Random.Range(1,1.2f);
            as_takeItem.Play();
        }
        public void CashRegister() {
            as_cashRegister.Play();
        }

//**********AUDIO SETTINGS**********//
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
