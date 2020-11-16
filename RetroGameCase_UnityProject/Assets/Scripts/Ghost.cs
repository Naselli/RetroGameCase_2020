using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [ SerializeField ] private float       distanceToPlayer;
    [ SerializeField ] private GameObject  player;
    [ SerializeField ] private float       speed;
    private                    Rigidbody2D rB2D;

    void Start(){
        rB2D = GetComponent< Rigidbody2D >();
        player = GameObject.FindWithTag("PlayerOne");
    }

    void FixedUpdate(){

        if( Vector2.Distance(transform.position, player.transform.position) < distanceToPlayer ){
            transform.position = Vector2.MoveTowards( transform.position , player.transform.position , speed * Time.fixedDeltaTime );
        }

    }
}
