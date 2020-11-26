using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    [ Header( "Sprites" ) ] 
    [ SerializeField ] private SpriteRenderer playerSprite;
    [ SerializeField ] private Sprite broomSprite;
    [ SerializeField ] private Sprite idleSprite;
    [ SerializeField ] private SpriteRenderer speedIcon;
    [ SerializeField ] private Sprite speedOn;
    [ SerializeField ] private Sprite speedOff;
    [ SerializeField ] private GameObject healthbar;

    private Vector2     _moveVector;
    private Rigidbody2D _rb;
    private bool        canBroom  = true;
    private IEnumerator coroutine = null;
    private Animator    anim;
    
    [ Header( "current state" ) ]
    [ SerializeField ] private GameObject    itemIsHolding;
    [ SerializeField ] private GameObject     itemYouAreOnTopOf;
    [ SerializeField ] private Kettle         currentLocation;
    private static readonly    int            IsWalking = Animator.StringToHash( "isWalking" );
    public                     ParticleSystem dust;

    private void Start(){
        _rb = GetComponent< Rigidbody2D >();
        anim = GetComponent< Animator >();
    }

    void CreateDust()
    {
        dust.Play();
    }
    
    private void Update(){
        //Maybe change to normal get axis to calculate a more precise move dir for the throw
        _moveVector.x = Input.GetAxisRaw( "Horizontal" + player );
        _moveVector.y = Input.GetAxisRaw( "Vertical" + player );

        if( (_moveVector.y + _moveVector.x) != 0){
            anim.SetBool(IsWalking, true);
            CreateDust();
        }
        else{
            anim.SetBool(IsWalking, false);
        }
        
        if( _moveVector.x > 0 ){
            playerSprite.flipX = true;            
        }
        if( _moveVector.x < 0 ){
            playerSprite.flipX = false;            
        }
        
        
        if (Input.GetButtonDown("PickUp" + player) )
        {
            if ( itemYouAreOnTopOf != null ){
                DropItem();
                SetHoldingItem( itemYouAreOnTopOf );
            }
            else if ( itemYouAreOnTopOf == null ){
                if( currentLocation != null && itemIsHolding != null){
                    if( itemIsHolding.CompareTag("Ingredient" ) ){
                        GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("Water");
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
            
            Camera.main.gameObject.GetComponent<ScreenShake>().Shake(.5f);
            GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("glass");
            
            switch( other.gameObject.GetComponent<Explosion>().TypeOfExplosion ){
                case Explosion.ExplosionType.Fire:
                    currentHealth -= 20;
                    healthbar.transform.DOPunchScale( new Vector3( 1 , 1 , 1 ) , .2f );
                    Debug.Log("DAMAGE");
                    break;
                case Explosion.ExplosionType.Slime:
                    StartCoroutine( slowingDown() );
                    break;
                case Explosion.ExplosionType.Poison:
                    StartCoroutine( DamageOverTime() );
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
        itemIsHolding.GetComponent< Rigidbody2D >().AddForce(localDir * 3 , ForceMode2D.Impulse );
        GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("Woosh");
        StartCoroutine( DelayColliderEnable() );

    }
    
    private IEnumerator DoBroomSpeed(){
        GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("Bezem");
        StartCoroutine( BroomCooldown() );
        speed +=5;
        anim.SetBool("Broom", true);
        playerSprite.sprite = broomSprite;
        yield return new WaitForSeconds(5f);
        anim.SetBool("Broom", false);
        playerSprite.sprite = idleSprite;
        speed -= 5;
    }
    private IEnumerator BroomCooldown(){
        canBroom = false;
        speedIcon.sprite = speedOff;
        yield return new WaitForSeconds(15f);
        canBroom = true;
        speedIcon.sprite = speedOn;
        GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("Ready");
        speedIcon.transform.DOPunchScale( new Vector3( 1 , 1 , 1 ) , .2f );
    }
    private IEnumerator DelayColliderEnable(){
        yield return new WaitForSeconds(.5f);
        //itemIsHolding.GetComponent< Potion >().Col.enabled = true;
        DropItem();
    }
    private IEnumerator DamageOverTime(){
        for( int i = 0 ; i < 4 ; i++ ){
            currentHealth -= 5;
            healthbar.transform.DOPunchScale( new Vector3( 1 , 1 , 1 ) , .2f );
            yield return new WaitForSeconds(1f);
        }
           
        
    }
    private IEnumerator slowingDown(){
        speed /= 2f;
        yield return new WaitForSeconds(3f);
        speed *= 2;
    }
    
    public int CurrentHealth{
        get => currentHealth;
        set => currentHealth = value;
    }
}
