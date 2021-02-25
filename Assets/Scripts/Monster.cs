using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;

    private bool _hasDied;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldDieFromCollision(collision))
        {
            Die();
        }


    }

    bool ShouldDieFromCollision(Collision2D collision)
    {

        if (_hasDied) {
            return false;
        }


        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
        //bird collides
            return true;

        }

        if (collision.contacts[0].normal.y < -0.5) {
            //falls from top
            return true;
        }

        return false;
    }

    void Die()
    {
        _hasDied = true;
        //setting effect when monster dies
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();

        //gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
