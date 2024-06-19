using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Disabled : MonoBehaviour
{
    public string _scene;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(_scene);
        }
        collision.gameObject.SetActive(false);
    } 
}
