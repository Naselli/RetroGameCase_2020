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

    private SpriteRenderer sR;
    private Rigidbody2D    rB;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().AddTorque(360, ForceMode2D.Impulse);
        sR = GetComponent< SpriteRenderer >();
        rB = GetComponent< Rigidbody2D >();
    }

    private void OnTriggerEnter2D( Collider2D other ){
        if( rB.velocity.magnitude > 0f ){
            GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("explosion");
            switch( typeOfPotion ){
                case PotionType.Fire:   FireDamage(); break;
                case PotionType.Poison: PoisonDamage(); break;
                case PotionType.Slime:  SlimeDamage(); break;
            }
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
