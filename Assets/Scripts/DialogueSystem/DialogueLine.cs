using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

namespace DialogueSystem
{
    // inhereit from DialogueBaseClass to use functions/methods written in class 
    public class DialogueLine : DialogueBaseClass
    {
        [Header ("Text Options")]
        // serialized field so it can be edited from unity
        [SerializeField]
        private string input;

        [SerializeField]
        private Color textColor;


        private TMP_Text textHolder;

        [Header("Time parameters")]
        [SerializeField]
        private float delay;

        [Header("Sound")]
        [SerializeField]
        private AudioClip sound;


        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();
            //textColor = textHolder.color;

            StartCoroutine(WriteText(input, textHolder, textColor, delay, sound));
        }
    }
}

