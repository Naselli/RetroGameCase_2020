using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public enum ExplosionType
    {
        Fire,
        Slime,
        Poison,
    }

    [ SerializeField ] private ExplosionType typeOfExplosion;
    public ExplosionType TypeOfExplosion{
        get => typeOfExplosion;
        set => typeOfExplosion = value;
    }
    
    void Start()
    {
        switch( typeOfExplosion ){
            case ExplosionType.Fire: 
                StartCoroutine( DelayEndExplosion( 1f ) );
                break;
            case ExplosionType.Slime: 
                StartCoroutine( DelayEndExplosion( 3f ) );
                break;
            case ExplosionType.Poison:
                StartCoroutine( DelayEndExplosion( 5f ) );
                break;
        }
    }

    private IEnumerator DelayEndExplosion(float seconds){
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    
        
}
