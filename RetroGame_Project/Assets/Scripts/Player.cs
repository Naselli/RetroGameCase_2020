using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [ Header( "Movement" ) ]
    [ SerializeField ] private float      speed;
    [ SerializeField ] private float      throwSpeed;
    [ SerializeField ] private string     player = "1";
    [ SerializeField ] private GameObject hand;
    [ SerializeField ] private int        currentHealth;
    [ SerializeField ] private GameObject enemy;
    
    private Vector2     _moveVector;
    private Rigidbody2D _rb;
    private bool        canBroom = true;

    [ Header( "current state" ) ]
    [ SerializeField ] private GameObject    itemIsHolding;
    [ SerializeField ] private GameObject itemYouAreOnTopOf;
    [ SerializeField ] private Kettle     currentLocation;

    private IEnumerator coroutine = null;

    private void Start(){
        _rb = GetComponent< Rigidbody2D >();
    }

    private void Update(){
        //Maybe change to normal get axis to calculate a more precise move dir for the throw
        _moveVector.x = Input.GetAxisRaw( "Horizontal" + player );
        _moveVector.y = Input.GetAxisRaw( "Vertical" + player );

        if (Input.GetButtonDown("PickUp" + player) )
        {
            if ( itemYouAreOnTopOf != null ){
                DropItem();
                SetHoldingItem( itemYouAreOnTopOf );
            }
            else if ( itemYouAreOnTopOf == null ){
                if( currentLocation != null && itemIsHolding != null){
                    if( itemIsHolding.CompareTag("Ingredient" ) ){
                        currentLocation.AddIngredient(itemIsHolding.GetComponent<Ingredient>());
                        Destroy(itemIsHolding);
                    }
                    else{
                        //Can't put a potion in the kettle, this needs a feedback particle
                    }
                }
                else
                    DropItem();
            }
        }
        if( Input.GetButtonDown( "Attack" + player ) ){
            
            if( itemIsHolding != null ){
                if( itemIsHolding.tag == "Potion" )
                    YeetPotion();
            }
            else if (canBroom){
                DropItem();
                StartCoroutine( DoBroomSpeed() );
            }
        }
    }
    private void FixedUpdate() => _rb.MovePosition(_rb.position + _moveVector * (speed * Time.fixedDeltaTime));

    private void OnTriggerEnter2D( Collider2D other ){
        
        if (other.TryGetComponent< Potion >( out var potion ) )
            itemYouAreOnTopOf = potion.gameObject;
        else if (other.TryGetComponent< Ingredient >( out var ingredient ) )
            itemYouAreOnTopOf = ingredient.gameObject;
        else if (other.TryGetComponent< Kettle >( out var kettle ) )
            currentLocation = kettle;
        
        if( other.gameObject.tag == "Explosion" ){
            switch( other.gameObject.GetComponent<Explosion>().TypeOfExplosion ){
                case Explosion.ExplosionType.Fire:
                    currentHealth -= 10;
                    Debug.Log("DAMAGE");
                    break;
                case Explosion.ExplosionType.Slime:
                    speed /= 2f;
                    break;
                case Explosion.ExplosionType.Poison:
                    coroutine = DamageOverTime();
                    StartCoroutine( coroutine );
                    break;
            }
        }
    }
    private void OnTriggerExit2D( Collider2D other ){
        //if (other.GetComponent<Item>() == itemYouAreOnTopOf)
            itemYouAreOnTopOf = null;
        
        //if (!(itemYouAreOnTopOf is null) && other.GetComponent< Potion >() == itemYouAreOnTopOf )
        //    itemYouAreOnTopOf = null;
        
        if (other.GetComponent< Kettle >() == currentLocation )
            currentLocation = null;
        
        if( other.gameObject.tag == "Explosion" ){
            switch( other.gameObject.GetComponent<Explosion>().TypeOfExplosion ){
                case Explosion.ExplosionType.Fire:
                    break;
                case Explosion.ExplosionType.Slime:
                    speed *= 2;
                    break;
                case Explosion.ExplosionType.Poison:
                    Debug.Log("YEEEEEEET"  );
                    if( coroutine != null ){
                        StopCoroutine( coroutine );
                        coroutine = null;
                    }
                    
                    break;
            }
        }
    }

    private void DropItem(){
        if (itemIsHolding != null){
            itemIsHolding.GetComponent< SpriteRenderer >().sortingOrder--;
            itemIsHolding.transform.parent.DetachChildren();
            itemIsHolding = null;
        }
    }
    private void SetHoldingItem(GameObject v)
    {
        if (itemIsHolding != null)
            DropItem();
        
        itemIsHolding = v;

        if (itemIsHolding != null){
            itemIsHolding.transform.SetParent( hand.transform );
            itemYouAreOnTopOf = null; // MAYBE IDK OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
            itemIsHolding.transform.localPosition = Vector2.zero;
            itemIsHolding.GetComponent<SpriteRenderer>().sortingOrder++;
        }
    }
    private void YeetPotion(){

        Vector2 dir = enemy.transform.position - transform.position;
        Vector2 localDir = transform.InverseTransformDirection( dir );
        itemIsHolding.GetComponent< Rigidbody2D >().isKinematic = false;
        itemIsHolding.GetComponent< Rigidbody2D >().AddForce(localDir * throwSpeed/2 , ForceMode2D.Impulse );
        StartCoroutine( DelayColliderEnable() );

    }
    private IEnumerator DoBroomSpeed(){
        speed *= 2;
        yield return new WaitForSeconds(5f);
        StartCoroutine( BroomCooldown() );
        speed /= 2;
    }
    private IEnumerator BroomCooldown(){
        canBroom = false;
        yield return new WaitForSeconds(10f);
        canBroom = true;
    }
    private IEnumerator DelayColliderEnable(){
        yield return new WaitForSeconds(.5f);
        //itemIsHolding.GetComponent< Potion >().Col.enabled = true;
        DropItem();
    }
    private IEnumerator DamageOverTime(){
        while( true ){
            currentHealth -= 5;
            yield return new WaitForSeconds(1f);
        }
    }
    public int CurrentHealth{
        get => currentHealth;
        set => currentHealth = value;
    }
}
