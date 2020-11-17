using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [ SerializeField ] private float       moveSpeed = 5f;
    [ SerializeField ] private Rigidbody2D rb;
    [ SerializeField ] private GameObject  hand;
    [ SerializeField ] private GameObject      objectHolding;
    [ SerializeField ] private GameObject      veggieWeAreOnTopOf;
    [ SerializeField ] private Location    currentLocation;
    [ SerializeField ] private string      player;

    private Vector2 movement;

    public void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal" + player);
        movement.y = Input.GetAxisRaw("Vertical" + player);

        if( Input.GetButtonDown("Attack" +player) ){
            if( veggieWeAreOnTopOf != null && veggieWeAreOnTopOf.GetComponent<Veggie>().State == Veggie.Type.Alive ){
                Veggie script = veggieWeAreOnTopOf.GetComponent< Veggie >();
                script.State = Veggie.Type.Dead;
                script.UpdateSprite();
            }
            else if (objectHolding == null && currentLocation != null){
                if( currentLocation.Type == Location.LocationType.CuttingBoard ){
                    if( currentLocation.transform.GetChild( 0 ).TryGetComponent< Veggie >( out var v ) ){
                        v.State = Veggie.Type.CutInPieces;
                        v.UpdateSprite();
                    }
                }
            }
        }
        if( Input.GetButtonDown("PickUp"+ player) ){
            if( veggieWeAreOnTopOf != null && veggieWeAreOnTopOf.GetComponent<Veggie>().State != Veggie.Type.Alive && currentLocation == null && objectHolding == null){
                SetHoldingItem(veggieWeAreOnTopOf);
            }
            else if ( objectHolding != null && currentLocation == null){
                DropItem();
            }
            else if( currentLocation != null && objectHolding != null  && currentLocation.transform.childCount < 1){
                objectHolding.transform.SetParent( currentLocation.transform );
                objectHolding.transform.localPosition = Vector2.zero;
                objectHolding = null;
            }
            else if (currentLocation != null && objectHolding == null){
                if( currentLocation.transform.GetChild(0) != null ){
                    GameObject g = currentLocation.transform.GetChild( 0 ).gameObject;
                    SetHoldingItem(g);
                }
            }
        }
    }
    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
    private void OnTriggerEnter2D( Collider2D other ){
        if ( other.TryGetComponent<Veggie>(out var groentje) )
            veggieWeAreOnTopOf = groentje.gameObject;
        else if( other.TryGetComponent< Location >( out var location ) )
            currentLocation = location;
    }
    private void OnTriggerExit2D( Collider2D other ){
        if ((veggieWeAreOnTopOf != null) && other.GetComponent<Veggie>().gameObject == veggieWeAreOnTopOf.gameObject)//HELP :((((((((((((((((
            veggieWeAreOnTopOf = null;
        
        if ((currentLocation != null) && other.GetComponent<Location>() == currentLocation)
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
    private void SetHoldingItem(GameObject v)
    {
        if (objectHolding != null)
            DropItem();
        
        objectHolding = v;

        if (objectHolding != null)
        {
            objectHolding.transform.SetParent(hand.transform);
            objectHolding.transform.localPosition = Vector2.zero;
            objectHolding.GetComponent<SpriteRenderer>().sortingOrder++;
        }
    }

    //private void Interact(){
    //    Location script = currentLocation.GetComponent< Location >();
    //    switch( script.Type ){
    //        case Location.LocationType.CuttingBoard:
    //            break;
    //        case Location.LocationType.SoupBowl: 
    //            break;
    //        
    //    }
    //}
}
