using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum PotionType
    {
        Fire,
        Slime,
        Poison,
    }

    [ SerializeField ] private PotionType typeOfPotion;
    [ SerializeField ] private GameObject poisonExplosion;
    [ SerializeField ] private GameObject fireExplosion;
    [ SerializeField ] private GameObject slimeExplosion;
    [ SerializeField ] private Collider2D col;
    

    private SpriteRenderer sR;
    
    void Start()
    {
        sR = GetComponent< SpriteRenderer >();

        switch( typeOfPotion ){
            case PotionType.Fire: sR.color = Color.red;
                break;
            case PotionType.Slime: sR.color = Color.green;
                break;
            case PotionType.Poison: sR.color = Color.magenta;
                break;
        }
    }

    public Collider2D Col{
        get => col;
        set => col = value;
    }

    private void OnCollisionEnter2D( Collision2D other ){
        switch( typeOfPotion ){
            case PotionType.Fire: FireDamage(); break;
            case PotionType.Poison: PoisonDamage(); break;
            case PotionType.Slime: SlimeDamage(); break;
        }
    }

    private void FireDamage(){
        Instantiate( fireExplosion , transform.position , Quaternion.identity );
        Destroy(gameObject);
    }
    
    private void SlimeDamage(){
        Instantiate( slimeExplosion , transform.position , Quaternion.identity );
        Destroy(gameObject);
    }
    
    private void PoisonDamage(){
        Instantiate( poisonExplosion , transform.position , Quaternion.identity );
        Destroy(gameObject);
    }
    
}
