using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // implying that instances will be created from these 
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;
    public GameObject fullHealthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        // look into root of the scene at game level and find the first object that is of type canvas 
        gameCanvas = FindObjectOfType<Canvas>();

    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTakesDamage;
        CharacterEvents.characterHealed += CharacterHealed;
        CharacterEvents.characterFullHealth += CharacterHasFullHealth;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTakesDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
        CharacterEvents.characterFullHealth -= CharacterHasFullHealth;
    }

    public void CharacterTakesDamage(GameObject character, int damageReceived)
    {
        // Creating text where character is hit
        // camera.main is the active camera - creating function on this camera
        // world pos of character and turn that into a point on the canvas 
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // this gives copy of game object 
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        // turning int into a string so string can be assigned to text
        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        //TODO
        // Creating text where character is hit
        // camera.main is the active camera - creating function on this camera
        // world pos of character and turn that into a point on the canvas 
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // this gives copy of game object 
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();

        // turning int into a string so string can be assigned to text
        tmpText.text = healthRestored.ToString();
    }

    public void CharacterHasFullHealth(GameObject character)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        // this gives copy of game object 
        TMP_Text tmpText = Instantiate(fullHealthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform)
            .GetComponent<TMP_Text>();
    }


    public void OnExitGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            #if (UNITY_EDITOR || DEVELOPMENT_BUILD)

                        Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);

            #endif

            #if (UNITY_EDITOR)
                        UnityEditor.EditorApplication.isPlaying = false;
            #elif (UNITY_STANDALONE)
                        Application.Quit();
            #elif (UNITY_WEBGL)
                        SceneManager.LoadScene("QuitScene");
            #endif
        }
        }
    }
