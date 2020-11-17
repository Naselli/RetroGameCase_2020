using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [ SerializeField ] private float       moveSpeed = 5f;
    [ SerializeField ] private Rigidbody2D rb;
    [ SerializeField ] private GameObject  hand;
    [ SerializeField ] private Veggie  objectHolding;
    [ SerializeField ] private Veggie  veggieWeAreOnTopOf;
    [ SerializeField ] private Location    currentLocation;

    private Vector2 movement;

    // Update is called once per frame
    public void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if( Input.GetButtonDown("PickUp") ){
            if( veggieWeAreOnTopOf != null ){
                DropItem();
                SetHoldingItem(veggieWeAreOnTopOf);
            }
            else if( veggieWeAreOnTopOf == null){
                DropItem();
            }
        }
        
    }
    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter2D( Collider2D other ){
        if ( other.TryGetComponent<Veggie>(out var item) )
            veggieWeAreOnTopOf = item;
        else if( other.TryGetComponent< Location >( out var location ) )
            currentLocation = location;
    }

    private void OnTriggerExit2D( Collider2D other ){
        if (other.GetComponent<Veggie>() == veggieWeAreOnTopOf)
            veggieWeAreOnTopOf = null;
        
        if (other.GetComponent<Location>() == currentLocation)
            currentLocation = null;
        
    }

    private void DropItem(){
        if (objectHolding != null)
        {
            objectHolding.GetComponent<SpriteRenderer>().sortingOrder--;
            objectHolding.transform.parent.DetachChildren();
            objectHolding = null;
        }
    }
    private void SetHoldingItem(Veggie v)
    {
        if (objectHolding != null)
            DropItem();
        
        objectHolding = v;

        if (objectHolding != null)
        {
            objectHolding.transform.SetParent(hand.transform);
            objectHolding.transform.localPosition = new Vector3(0, 0, 0);
            objectHolding.GetComponent<SpriteRenderer>().sortingOrder++;
        }
    }
    
}
