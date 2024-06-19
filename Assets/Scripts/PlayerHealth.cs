using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public string _scene;

    public int totalHealth = 3;
    public RectTransform heartUI;

    private int health;
    private float heartSize = 64f;

    private SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        health = totalHealth;
    }

    public void AddDamage(int amount)
    {
        health = health - amount;

        //visual feedback
        StartCoroutine("VisualFeedback");

        //Game Over
        if(health <= 0)
        {
            health = 0;
        }
        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);

        if(health == 0)
        {
            SceneManager.LoadScene(_scene);
        }
    }

    public void AddHealth(int amount)
    {
        health = health + amount;

        //visual feedback2
        StartCoroutine("VisualFeedback2");

        //Max Health
        if (health >= totalHealth)
        {
            health = totalHealth;
        }
        heartUI.sizeDelta = new Vector2(heartSize * health, heartSize);
    }

    private IEnumerator VisualFeedback()
    {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    private IEnumerator VisualFeedback2()
    {
        _renderer.color = Color.green;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

}
