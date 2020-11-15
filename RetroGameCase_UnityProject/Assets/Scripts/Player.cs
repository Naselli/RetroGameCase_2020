using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

abstract public class Player : MonoBehaviour
{
    private                  float      maxSanity;
    private                  float      currentSanity;
    private                  float      maxStamina;
    private                  float      currentStamina;
    private                  Transform  spawnPos;
    private                  Transform  lastPos;
    private                  GameObject isHolding;
    [SerializeField] private float      speed;
    [SerializeField] private float      dashSpeed;
    float                               timePassed = 0f;


    public Player( float maxSanity , float maxStamina , Transform spawnPos ){
        this.maxSanity = maxSanity;
        this.maxStamina = maxStamina;
        this.spawnPos = spawnPos;
    }

     virtual public void Move(string hInput, string vInput, GameObject p){
         float h = Input.GetAxis( hInput );
         float v = Input.GetAxis( vInput );
         //Debug.Log(string.Format("{0};{1}", h,v));
         transform.Translate(new Vector2( h,v ) * (speed * Time.deltaTime));
         lastPos = transform;
     }

    virtual public void Dash( string dashInput ){
        if(!Input.GetButtonDown(dashInput))
            return;
        Debug.Log("DASH");
        Vector2 localDir = transform.InverseTransformDirection( lastPos.position - transform.position );
        StartCoroutine( dashCoroutine( localDir, .5f ) );
    }

    private IEnumerator dashCoroutine(Vector2 dir, float duration){
        /*    
        while( timePassed < duration ){
            transform.Translate( dir * (dashSpeed * Time.deltaTime));
            timePassed += Time.deltaTime;
        }
        timePassed = 0;
        */
        Vector2 newPos = transform.position * dir * ( speed * Time.deltaTime );
        Vector2.Lerp( transform.position , newPos , 1f );
        yield return null;
    }
    
}
