using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;

   

    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = 50;
    }

 



}
