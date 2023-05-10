using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FinishLine : MonoBehaviour
{
    
    private void Start()
    {
        Cursor.visible = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //    if the thing that collided with this trigger has a tag of player then....
        if (collision.CompareTag("Player"))
        {
            // if after conditional, this returns array with objects with tag, then next conditional will not run

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);

        }
        
    }
}
