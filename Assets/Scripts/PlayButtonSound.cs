using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{

    public AudioSource buttonSound;


    public void playSound()
    {
        buttonSound.Play();
    }
}
