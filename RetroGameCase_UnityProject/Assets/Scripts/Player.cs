using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

abstract public class Player : MonoBehaviour
{
    
    
    private                    float       maxStamina;
    private                    float       currentStamina;
    private                    Transform   spawnPos;
    private                    Transform   lastPos;
    private                    GameObject  isHolding;
    [ SerializeField ] private float       currentSanity = 100;
    [ SerializeField ] private float       speed;
    [ SerializeField ] private float       dashSpeed;
    [ SerializeField ] private Text        sanity;
    [ SerializeField ] private float       maxSanity;
    private                    float       timePassed = 0f;
    private                    bool        isDashing;
    private                    float       sanityTimer       = 1f;
    private                    float       sanityTimerPassed = 0f;
    private                    IEnumerator CoroutineAddSanity;
    private                    IEnumerator CoroutineDepleteSanity;


    public Player( float maxSanity , float maxStamina , Transform spawnPos ){
        this.maxSanity = maxSanity;
        this.maxStamina = maxStamina;
        this.spawnPos = spawnPos;
    }


    virtual public void Start(){
        currentSanity = 100;
        sanity.text = currentSanity.ToString();
        CoroutineAddSanity = AddSanity();
        CoroutineDepleteSanity = DepleteSanity();
        StartCoroutine( CoroutineDepleteSanity );
    }

    virtual public void Update(){
        sanity.text = currentSanity.ToString();
    }

    public float CurrentSanity{
        get => currentSanity;
        set => currentSanity = value;
    }

     virtual public void Move(string hInput, string vInput, GameObject p){
         if( isDashing )
             return;
         
         float h = Input.GetAxis( hInput );
         float v = Input.GetAxis( vInput );
         //Debug.Log(string.Format("{0};{1}", h,v));
         GetComponent<Rigidbody2D>().velocity = new Vector2( h,v ) * (speed * Time.fixedDeltaTime); 
         lastPos = transform;
     }

    virtual public void Dash( string dashInput ){
        if(!Input.GetButtonDown(dashInput))
            return;
        //Debug.Log("DASH");
        StartCoroutine( dashCoroutine( .5f ) );
    }

    private IEnumerator dashCoroutine( float duration){
        isDashing = true;
        Vector2 localDir = GetComponent< Rigidbody2D >().velocity;
        GetComponent<Rigidbody2D>().AddForce(localDir.normalized * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        isDashing = false;
    }

    public IEnumerator DepleteSanity(){
        while( true ){
            currentSanity -= 1;
            //Debug.Log("Deplete Sanity");
            if( currentSanity > maxSanity )
                currentSanity = maxSanity;
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator AddSanity(){
        while( true ){
            currentSanity += 2;
            //Debug.Log("Add Sanity");
            if( currentSanity > maxSanity )
                currentSanity = maxSanity;
            yield return new WaitForSeconds(1f);
        }
    }
    
    virtual public void OnTriggerEnter2D( Collider2D other ){
        //if( !other.CompareTag( "Light" ) ) ; 
        //return;
        if( other.CompareTag( "Light" ) ){
            StopCoroutine( CoroutineDepleteSanity);
            CoroutineDepleteSanity = null;
            
            if (CoroutineAddSanity == null) 
                CoroutineAddSanity = AddSanity();
            
            StartCoroutine( CoroutineAddSanity );
        }
    }

    virtual public void OnTriggerExit2D( Collider2D other ){
        //if ( !other.CompareTag( "Light" ) ) 
        //return;
        if( other.CompareTag( "Light" ) ){
            StopCoroutine( CoroutineAddSanity );
            CoroutineAddSanity = null;    
            
            if (CoroutineDepleteSanity == null) 
                CoroutineDepleteSanity = DepleteSanity();
            StartCoroutine( CoroutineDepleteSanity );
        }
    }

    
}
