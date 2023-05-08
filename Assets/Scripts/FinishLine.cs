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
        // if the thing that collided with this trigger has a tag of player then....
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
