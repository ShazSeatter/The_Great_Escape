using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class FinishLine : MonoBehaviour
{
    public List<GameObject> enemies;


    private void Start()
    {
        Cursor.visible = true;
        //enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the thing that collided with this trigger has a tag of player then....
        if(collision.CompareTag("Player"))
        {
            // if after conditional, this returns array with objects with tag, then next conditional will not run
            enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

            if (enemies.Count == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
                
        }
    }
}
