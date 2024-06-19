using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 1f;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;
    public float playerAware = 3f;
    public float aimingTime = 0.5f;
    public float shootingTime = 1.5f;

    //public float minX;
    //public float maxX;
    //public float waitingTime = 2f;

    //private GameObject target;
    private Animator _animator;
    private Weapon _weapon;
    private Rigidbody2D _rigidbody;

    //Movement
    private Vector2 movement;
    private bool _facingRight;

    private bool _isAttacking;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x < 0f)
        {
            _facingRight = false;
        } else if (transform.localScale.x > 0f)
        {
            _facingRight = true;
        }
        //UpdateTarget();
        //StartCoroutine("PatrolToTarget");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Vector2.right;

        if (_facingRight == false)
        {
            direction = Vector2.left;
        }
        if (_isAttacking == false)
        {
            if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer))
            {
                Flip();
            }
        }
    }

    /*private void UpdateTarget()
    {
        //Primera vez creando target
        if(target == null)
        {
            target = new GameObject("Target");
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;        
        }

        //Si esta en la izquierda, cambiar a la derecha
        if(target.transform.position.x == minX)
        {
            target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
            return;
        }

        //Si esta en la derecha, cambiar a la izquierda
        if(target.transform.position.x == maxX)
        {
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }
    }*/

    /*private IEnumerator PatrolToTarget()
    {
        //Comprobar distancia del enemigo al target
        while(Vector2.Distance(transform.position, target.transform.position) > 0.05f)
        {

            //Update animator
            _animator.SetBool("Idle", false);

            //Moverse al target
            Vector2 direction = target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);

            //Repetir desde inicion esdta funcion, hata que sea falso
            yield return null;
        }

        //LLegaste al target
       
        transform.position = new Vector2(target.transform.position.x, transform.position.y);
        UpdateTarget();

        //Update animator
        _animator.SetBool("Idle", true);

        _animator.SetTrigger("Shoot");

        //Esperar
        yield return new WaitForSeconds(waitingTime);

        //Actualizar target y moverse
        
        StartCoroutine("PatrolToTarget");
    }

    public void CanShoot()
    {
        if(_weapon != null)
        {
            _weapon.Shoot();
        }
    }*/

    private void FixedUpdate()
    {
        float horizontalVelocity = speed;
        if(_facingRight == false)
        {
            horizontalVelocity = horizontalVelocity * -1;
        }
        if (_isAttacking)
        {
            horizontalVelocity = 0f;
        }
        _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(_isAttacking == false && collision.CompareTag("Player"))
        {
            StartCoroutine("AimAndShoot");
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator AimAndShoot()
    {
        float speedBackup = speed;
        speed = 0f;

        _isAttacking = true;

        yield return new WaitForSeconds(aimingTime);

        _animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(aimingTime);

        _isAttacking = false;
        speed = speedBackup;
    }

    void CanShoot()
    {
        if(_weapon != null)
        {
            _weapon.Shoot();
        }
    }

    private void OnEnable()
    {
        _isAttacking = false;
    }

    private void OnDisable()
    {
        StopCoroutine("AimAndShoot");
        _isAttacking = false;
        speed = 1;
    }

}
