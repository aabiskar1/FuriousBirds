using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 600 ;
    [SerializeField] float _maxDragDistance = 3.5f;


    private Vector2 _startPosition;
    private Rigidbody2D _ridgetbody2D;
    private SpriteRenderer _spriteRenderer;
    void Awake()
    {
        _ridgetbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _ridgetbody2D.position;
        _ridgetbody2D.isKinematic = true;
        
    }

    void OnMouseDown()
    {
        _spriteRenderer.color = Color.red;    
    }
    void OnMouseUp()
    {
        Vector2 currentPosition = _ridgetbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _ridgetbody2D.isKinematic = false;

        _ridgetbody2D.AddForce(direction * _launchForce);


        _spriteRenderer.color = Color.white;
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

      
        //max distance that bird can be dragged
        float distance = Vector2.Distance(desiredPosition,_startPosition);
        if (distance > _maxDragDistance) {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }


        //Clamping the bird to not let the mouse drag bird forward
        if (desiredPosition.x > _startPosition.x)
        {
            desiredPosition.x = _startPosition.x;
        }


        _ridgetbody2D.position = desiredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDeplay());

    }

    IEnumerator ResetAfterDeplay()
    {
        yield return new WaitForSeconds(3);
        _ridgetbody2D.position = _startPosition;
        _ridgetbody2D.isKinematic = true;
        _ridgetbody2D.velocity = Vector2.zero;
    }
}
