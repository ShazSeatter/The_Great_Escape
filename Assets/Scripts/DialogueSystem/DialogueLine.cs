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
        // serialized field so it can be edited from unity
        [SerializeField]
        private string input;

        public TMP_Text textHolder;

       

        private void Awake()
        {
            textHolder = GetComponent<TMP_Text>();

            StartCoroutine(WriteText(input, textHolder));
        }
    }
}

