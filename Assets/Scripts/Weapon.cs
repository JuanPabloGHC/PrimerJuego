using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shooter;
    private Transform firePoint;

    //public GameObject explosionEffect;
    //public LineRenderer lineRenderer;

    void Awake()
    {
        //Encontrar el firepoint
        firePoint = transform.Find("FirePoint");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        //Comprobar que exiatn los objetos
        if (bullet != null && firePoint != null && shooter != null)
        {
            GameObject mybullet = Instantiate(bullet, firePoint.position, Quaternion.identity) as GameObject;

            Bullet bulletComponent = mybullet.GetComponent<Bullet>();
            
            if(shooter.transform.localScale.x < 0f)
            {
                //Atacar a la izquierda
                bulletComponent.direction = Vector2.left;
            }
            else
            {
                //Atacar a la derecha
                bulletComponent.direction = Vector2.right;
            }
        }
    }

    /*public IEnumerator ShootWithRaycast()
    {
        if(explosionEffect != null && lineRenderer != null)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

            if (hitInfo)
            {
                //Set Line Renderer
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);

                Instantiate(explosionEffect, hitInfo.point, Quaternion.identity);

            }
            else
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100);
            }

            lineRenderer.enabled = true;
            yield return null;
            lineRenderer.enabled = false;
        }
    }*/

}
