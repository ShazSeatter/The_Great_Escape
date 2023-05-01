using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    public float speed = 3f;

    [SerializeField]
    public Vector2 direction;

    [SerializeField]
    private string targetTag;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void Setup(Vector2 direction)
    {
        this.direction = direction;
    }
}
