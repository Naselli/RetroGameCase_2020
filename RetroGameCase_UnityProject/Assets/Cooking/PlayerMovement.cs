using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [ SerializeField ] private float       moveSpeed = 5f;
    [ SerializeField ] private Rigidbody2D rb;
    [ SerializeField ] private GameObject  hand;
    [ SerializeField ] private GameObject  isHolding;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if( isHolding != null && !Input.GetButton("PickUp") ){
            isHolding.transform.parent = null;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
    
    private void OnTriggerStay2D( Collider2D other ){
        Veggie script = other.GetComponent< Veggie >();
        if( other.CompareTag("Veggie") && Input.GetButtonDown("Attack") ){
            Debug.Log("veggie is now ded");
            other.GetComponent<SpriteRenderer>().color = Color.red;
            script.State = Veggie.Type.Dead;
        }
        
        if( other.CompareTag("Veggie") && Input.GetButtonDown("PickUp")){
            
            if( script.State == Veggie.Type.Alive ){
                Debug.Log("Can't pick up alive");
                //can't pickup when veggie is alive;
            }
            else if ( script.State == Veggie.Type.Dead || isHolding != null ){
                Debug.Log("Picked up veggie");
                var o = other.gameObject;
                o.transform.SetParent(hand.transform);
                o.transform.position = hand.transform.position;
                isHolding = o;
                o.layer++;
            }
        }
    }
    
}
