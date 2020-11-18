using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [ Header( "Movement" ) ]
    [ SerializeField ] private float speed;
    [ SerializeField ] private string     player = "1";
    [ SerializeField ] private GameObject hand;
    
    private Vector2     _moveVector;
    private Rigidbody2D _rb;

    [ Header( "current state" ) ]
    [ SerializeField ] private GameObject    itemIsHolding;
    [ SerializeField ] private GameObject itemYouAreOnTopOf;
    [ SerializeField ] private Kettle     currentLocation;

    private void Start(){
        _rb = GetComponent< Rigidbody2D >();
    }

    private void Update(){
        _moveVector.x = Input.GetAxisRaw( "Horizontal" + player );
        _moveVector.y = Input.GetAxisRaw( "Vertical" + player );
        
        if (Input.GetButtonDown("PickUp" + player) )
        {
            if ( itemYouAreOnTopOf != null ){
                DropItem();
                SetHoldingItem( itemYouAreOnTopOf );
            }
            else if ( itemYouAreOnTopOf == null ){
                if( currentLocation != null && itemIsHolding != null){
                    if( itemIsHolding.CompareTag("Ingredient" ) ){
                        currentLocation.AddIngredient(itemIsHolding.GetComponent<Ingredient>());
                        Destroy(itemIsHolding);
                    }
                    else{
                        //Can't put a potion in the kettle, this needs a feedback particle
                    }
                }
                else
                    DropItem();
            }
        }
    }
    private void FixedUpdate() => _rb.MovePosition(_rb.position + _moveVector * (speed * Time.fixedDeltaTime));

    private void OnTriggerEnter2D( Collider2D other ){
        if (other.TryGetComponent< Potion >( out var potion ) )
            itemYouAreOnTopOf = potion.gameObject;
        else if (other.TryGetComponent< Ingredient >( out var ingredient ) )
            itemYouAreOnTopOf = ingredient.gameObject;
        else if (other.TryGetComponent< Kettle >( out var kettle ) )
            currentLocation = kettle;
        
    }
    private void OnTriggerExit2D( Collider2D other ){
        //if (other.GetComponent<Item>() == itemYouAreOnTopOf)
            itemYouAreOnTopOf = null;
        if (other.GetComponent< Kettle >() == currentLocation )
            currentLocation = null;
    }
    
    private void DropItem(){
        if (itemIsHolding != null){
            itemIsHolding.GetComponent< SpriteRenderer >().sortingOrder--;
            itemIsHolding.transform.parent.DetachChildren();
            itemIsHolding = null;
        }
    }
    private void SetHoldingItem(GameObject v)
    {
        if (itemIsHolding != null)
            DropItem();
        
        itemIsHolding = v;

        if (itemIsHolding != null){
            itemIsHolding.transform.SetParent( hand.transform );
            itemIsHolding.transform.localPosition = Vector2.zero;
            itemIsHolding.GetComponent<SpriteRenderer>().sortingOrder++;
        }
    }
}
