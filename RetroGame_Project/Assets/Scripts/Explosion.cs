using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Explosion : MonoBehaviour
{
    public enum ExplosionType
    {
        Fire,
        Slime,
        Poison,
    }

    [ SerializeField ] private ExplosionType typeOfExplosion;
    [ SerializeField ] private ParticleSystem part;
    public ExplosionType TypeOfExplosion{
        get => typeOfExplosion;
        set => typeOfExplosion = value;
    }
    
    void Start(){
        transform.DOPunchScale( new Vector3(1,1,1) , .2f  );
        part.Play();
        switch( typeOfExplosion ){
            case ExplosionType.Fire: 
                StartCoroutine( DelayEndExplosion( 1f ) );
                break;
            case ExplosionType.Slime: 
                StartCoroutine( DelayEndExplosion( 1f ) );
                break;    
            case ExplosionType.Poison:
                StartCoroutine( DelayEndExplosion( 1f ) );
                break;
        }
    }

    private IEnumerator DelayEndExplosion(float seconds){
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    
        
}
