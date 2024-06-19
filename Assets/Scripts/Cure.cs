using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cure : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Collider2D _collider;

    public RectTransform heartUI;
    private float heartSize = 64f;

    public int healthRestoration = 1;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(heartUI.sizeDelta == new Vector2(heartSize, heartSize) || heartUI.sizeDelta == new Vector2(128f, heartSize))
        
            if (collision.CompareTag("Player"))
            {

                collision.SendMessageUpwards("AddHealth", healthRestoration);

                //Desactivar
                _collider.enabled = false;

                //Visual Stuf
                _renderer.enabled = false;


                //Destruir
                Destroy(this.gameObject, 2f);
            }
        
    }
        
}
