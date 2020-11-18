using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [ Header( "Movement" ) ]
    [ SerializeField ] private float speed;
    [ SerializeField ] private string player = "1";
    
    private Vector2     _moveVector;
    private Rigidbody2D _rb;

    [ Header( "current state" ) ]
    [ SerializeField ] private GameObject    itemIsHolding;
    [ SerializeField ] private GameObject     itemYouAreOnTopOf;
    [ SerializeField ] private Kettle currentLocation;

    private void Start(){
        _rb = GetComponent< Rigidbody2D >();
    }

    private void Update(){
        _moveVector.x = Input.GetAxisRaw( "Horizontal" + player );
        _moveVector.y = Input.GetAxisRaw( "Horizontal" + player );
    }

    private void FixedUpdate(){
        _rb.MovePosition(_rb.position + _moveVector * (speed * Time.fixedDeltaTime));
    }
    
    private void OnTriggerEnter2D( Collider2D other ){
        if (other.TryGetComponent<Potion>(out var potion))
            itemYouAreOnTopOf = potion.gameObject;
        else if (other.TryGetComponent<Ingredient>(out var ingredient))
            itemYouAreOnTopOf = ingredient.gameObject;
        else if (other.TryGetComponent<Kettle>(out var kettle))
            currentLocation = kettle;
        
    }
    private void OnTriggerExit2D( Collider2D other ){
        //if (other.GetComponent<Item>() == itemYouAreOnTopOf)
            itemYouAreOnTopOf = null;
        if (other.GetComponent<Kettle>() == currentLocation)
            currentLocation = null;
        
    }
}
