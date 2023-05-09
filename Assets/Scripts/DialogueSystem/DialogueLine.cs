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
        [SerializeField]
        private float delayBetweenLines;

        [Header("Sound")]
        [SerializeField]
        private AudioClip sound;

        [Header("Character Image")]
        [SerializeField]
        private Sprite characterSprite;
        [SerializeField]
        private Image imageHolder;

        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();
            textHolder.text = "";

            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
        }

        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, textColor, delay, sound, delayBetweenLines));
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        }
    }
}

