using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove3D : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _running;
    private float _horizontalInput;
    private static float _SPEED = 3f;
    private static float _JUMP_FORCE = 6f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        // 0: no key, -1: left key, +1: right key
        if (
            Mathf.Abs(_rigidbody.velocity.y) < 0.01f &&
            Input.GetButtonDown("Jump")
        )
        {
            _animator.SetTrigger("Jump");
            _rigidbody.AddForce(_JUMP_FORCE * Vector3.up, ForceMode.Impulse);
        }

        if (Mathf.Abs(_horizontalInput) > 0.01f)
        {
            // move character
            transform.rotation = Quaternion.LookRotation(new Vector3(
                -_horizontalInput, 0f, 0f));
            _rigidbody.MovePosition(
                _rigidbody.position - transform.forward * _SPEED * Time.fixedDeltaTime);

            // change animation
            if (!_running)
            {
                _running = true;
                _animator.SetBool("Running", true);
            }
            else if (_running)
            {
                _running = false;
                _animator.SetBool("Running", false);
            }
        }
    }
}
