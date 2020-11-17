using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veggie : MonoBehaviour
{
    public enum Type {
        Alive,
        Dead,
        CutInPieces,
    }
    private Type state;

    [ SerializeField ] private float       speed;
    [ SerializeField ] private GameObject  player;
    [ SerializeField ] private float       minDis;
    [ SerializeField ] private Rigidbody2D rB;

    private void Start(){
        //StartCoroutine( AddForceDelay() );
        state = Type.Alive;
        UpdateSprite();
        //rB = GetComponent< Rigidbody2D >();
    }

    public Type State{
        get => state;
        set => state = value;
    }

    public void UpdateSprite(){
        switch( State ){
            case Type.Alive: 
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case Type.Dead: 
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case Type.CutInPieces:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
    }

    //private IEnumerator AddForceDelay(){
    //    while( true ){
    //        rB.AddForce(Vector2.left + Vector2.up * 8, ForceMode2D.Impulse );
    //        yield return new WaitForSeconds( 5f );
    //    }
    //}
}
