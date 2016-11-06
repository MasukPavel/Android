﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Mindblower.Core;
using System.Collections.Generic;

namespace Mindblower.Menu
{
    public class LevelStarter : MonoBehaviour
    {
       
        [SerializeField]
        private string levelId;
 bool sound = false;
        private LevelsLoader levelsLoader;

        [SerializeField]
        private List<SpriteRenderer> stars;

        void Awake()
        {
            levelsLoader = GameObject.FindGameObjectWithTag("CoreController").GetComponent<LevelsLoader>();
        }

        void Start()
        {
            if(AudioListener.volume==1)
            sound = true;
            int result = PlayerPrefs.GetInt(levelId, 0);
            stars.ForEach(x => x.enabled = false);
            for (int i = 0; i < result; ++i)
                stars[i].enabled = true;

        }

        void OnMouseDown()
        {
            PlayerPrefs.SetFloat("Camera_Position_Y", Camera.main.transform.position.y);
            levelsLoader.LoadLevel(levelId);
        }
        public void BackToMap() {
            Application.LoadLevel("MapLevel");
                }
        public void Settings() {
            SceneManager.LoadScene("Settings");
            
          GUI.Button(new Rect(25, 25, 100, 30), "Sound");
        }
        void OnGUI()
        {
            if (sound)
            {
               GUI.Label(new Rect(500, 250, 200, 100), "Sound is On");
                    

            }
        }
        public void Rating() {
            SceneManager.LoadScene("Rating");
        }
        public void BackToMenu()
        {
            SceneManager.LoadScene("MenuLevel");
        }
        public void Sound(bool b) {
            if (!sound) { 
                AudioListener.volume = 1;
            sound = true; }
            else{
                AudioListener.volume = 0;
                sound = false; }
        }
       
    }
}

