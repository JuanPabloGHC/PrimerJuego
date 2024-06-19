using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float livingTime = 3f;
    public float speed = 2f;
    public Vector2 direction;

    public Color initialColor = Color.white;
    public Color finalColor;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private float startingTime;

    private bool _returning = false;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Momento que aparece la bala
        startingTime = Time.time;

        //Destruir la bala
        Destroy(this.gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento del objeto
        /*Vector2 movement = direction.normalized * speed  * Time.deltaTime;
        transform.Translate(movement);*/
        //Solo para mover como teletransporte

        //Cambio de color
        float timeSinceStarted = Time.time - startingTime;
        float percentageCompleted = timeSinceStarted / livingTime;

        _renderer.color = Color.Lerp(initialColor, finalColor, percentageCompleted);
    }

    private void FixedUpdate()
    {
        Vector2 movement = direction.normalized * speed;
        _rigidbody.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_returning == false && collision.CompareTag("Player"))
        {
            //Send Message
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(this.gameObject);
        }

        if(_returning == true && collision.CompareTag("Enemy"))
        {
            collision.SendMessageUpwards("AddDamage");
            Destroy(this.gameObject);
        }
    }

    public void AddDamage()
    {
        _returning = true;
        direction = direction * -1f;
    }

}
