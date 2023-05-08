using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DialogueSystem
{
    // inhereit from DialogueBaseClass to use functions/methods written in class 
    public class DialogueLine : DialogueBaseClass
    {
        // serialized field so it can be edited from unity
        [SerializeField]
        private string input;

        private Text textHolder;

        private void Awake()
        {
            textHolder = GetComponent<Text>();

            StartCoroutine(WriteText(input, textHolder));
        }
    }
}

