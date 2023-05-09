using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    //public GameObject[] enemies;
    public List<GameObject> enemies;

    Damageable enemyDamageable;
    //private void Awake()
    //{
    //    GameObject spider = GameObject.FindGameObjectWithTag("Enemy");

    //}
    private void Start()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        GameObject allEnemies = GameObject.FindGameObjectWithTag("Enemy");
        enemyDamageable = allEnemies.GetComponent<Damageable>();
    }

    private void Update()
    {

        foreach (GameObject enemy in enemies)
        {
            if(enemyDamageable.IsAlive == false)
            {
                enemies.Remove(enemy);
            }
        }

        if (enemies.Count <= 0)
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
