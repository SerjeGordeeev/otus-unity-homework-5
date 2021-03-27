using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Collider2D _wallsCollider;
    [SerializeField] private Collider2D _groundCollider;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public LayerMask ground;

    private bool _moveBlocked = false;
    private bool _isGrounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");
        _isGrounded = _groundCollider.IsTouchingLayers(ground);


        if (!_moveBlocked)
        {
            if (hDirection < 0.0f)
            {
                _rigidbody2D.velocity = new Vector2(-5, _rigidbody2D.velocity.y);
                _spriteRenderer.flipX = true;
                _animator.SetBool("running", true);
            }
            else if (hDirection > 0.0f)
            {
                _rigidbody2D.velocity = new Vector2(5, _rigidbody2D.velocity.y);
                _spriteRenderer.flipX = false;
                _animator.SetBool("running", true);
            }
            else
            {
                if (_isGrounded) {
                    _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                }

                _animator.SetBool("running", false);
            }
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_groundCollider.IsTouchingLayers(ground))
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 10);
            } else if (_wallsCollider.IsTouchingLayers(ground))
            {
                if (_spriteRenderer.flipX)
                {
                    // --->
                    Debug.Log("Right");
                   
                    _rigidbody2D.velocity = new Vector2(5, 10);
                } else
                {
                    // <---
                    Debug.Log("Left");
                    _rigidbody2D.velocity = new Vector2(-5, 10);
                }

                _spriteRenderer.flipX = !_spriteRenderer.flipX;
                StartCoroutine(BlockMovement(.5f));
            } 
        }

        
    }

    IEnumerator BlockMovement(float seconds)
    {
        _moveBlocked = true;

        yield return new WaitForSeconds(seconds);

        _moveBlocked = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bonus"))
        {
            // LayerMask.NameToLayer("Default") = 0
            // LayerMask.GetMask("Default") = 1             // 000000001   // (1 << index)

            // LayerMask.NameToLayer("TransparentFX") = 1
            // LayerMask.GetMask("TransparentFX") = 2       // 000000010   // (1 << index)

            int layer = LayerMask.NameToLayer("Player");
            int mask = Physics2D.GetLayerCollisionMask(layer);
            mask |= LayerMask.GetMask("Crate");
            // mask &= ~LayerMask.GetMask("Crate");   // remove bit
            Physics2D.SetLayerCollisionMask(layer, mask);

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Platform"))
        {
            transform.parent = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Platform"))
        {
            transform.parent = null;
        }
    }
}
