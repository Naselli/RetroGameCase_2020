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

    [ SerializeField ] private SpriteRenderer sR;

    public ExplosionType TypeOfExplosion{
        get => typeOfExplosion;
        set => typeOfExplosion = value;
    }
    
    void Start()
    {
        sR = GetComponent< SpriteRenderer >();
        Color alpha = sR.color;
        alpha.a = .5f;
        
        switch( typeOfExplosion ){
            case ExplosionType.Fire: sR.color = Color.red;
                StartCoroutine( DelayEndExplosion( 1f ) );
                break;
            case ExplosionType.Slime: sR.color = Color.green;
                StartCoroutine( DelayEndExplosion( 3f ) );
                break;
            case ExplosionType.Poison: sR.color = Color.magenta;
                StartCoroutine( DelayEndExplosion( 3f ) );
                break;
        }
    }

    private IEnumerator DelayEndExplosion(float seconds){
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    
        
}
