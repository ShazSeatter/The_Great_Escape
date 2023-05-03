using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 10;

    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    AudioSource healthPickUpSource;



    private void Awake()
    {
        healthPickUpSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // detects if a damageable character walks into its box collider zone and then it calls the heal function on the damageable component 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
       

        if (damageable.Health < 100)
        {
            if(healthPickUpSource)
                AudioSource.PlayClipAtPoint(healthPickUpSource.clip, gameObject.transform.position, healthPickUpSource.volume);
            // after picking up cookie, it will be removed from game

            Destroy(gameObject);
        } else
        {
            CharacterEvents.characterFullHealth.Invoke(gameObject);
        }
    }

    public void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
