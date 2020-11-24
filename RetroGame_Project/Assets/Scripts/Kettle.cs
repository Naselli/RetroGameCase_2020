using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Kettle : MonoBehaviour
{
    [Header("item counters for debug")]
    [ SerializeField ] private int      fireCount;
    [ SerializeField ] private int      slimeCount;
    [ SerializeField ] private int      poisonCount;
    
    [ SerializeField ] private Collider2D kettleCol;
    [ SerializeField ] private GameObject potionSpawnpoint;
    [ SerializeField ] private bool       canSpawnPotion = true;
    [ SerializeField ] private bool       isSpawning = false;
    [ SerializeField ] private GameObject progressBar;
    
    [Header("Sprite")]
    [ SerializeField ] private SpriteRenderer WoodSprite;
    [ SerializeField ] private SpriteRenderer KettleSprite;
    [Space]
    [ SerializeField ] private Sprite Wood_ON;
    [ SerializeField ] private Sprite     Wood_OFF;
    [ SerializeField ] private Sprite     Broken;
    [ SerializeField ] private Sprite     Red;
    [ SerializeField ] private Sprite     Green;
    [ SerializeField ] private Sprite     Purple;
    [ SerializeField ] private Sprite     White;
    [ SerializeField ] private GameObject light;
    [ SerializeField ] private GameObject particleSys;
    
    [Header("Potion prefabs")]
    [ SerializeField ] private GameObject firePotion;
    [ SerializeField ] private GameObject slimePotion;
    [ SerializeField ] private GameObject poisonPotion;

    private void Update(){

        if( potionSpawnpoint.transform.childCount == 1 || !canSpawnPotion)
            kettleCol.enabled = false;
        else
            kettleCol.enabled = true;
        
        if( (fireCount + poisonCount + slimeCount) == 2 ){
            if( fireCount == 2 ){
                StartCoroutine( SpawnPotion( firePotion ) );
                ResetCounts();
            }
            else if( slimeCount == 2 ){
                StartCoroutine( SpawnPotion( slimePotion ) );
                ResetCounts();
            }
            else if( poisonCount == 2 ){
                StartCoroutine( SpawnPotion( poisonPotion ) );
                ResetCounts();
            }
            else{
                StartCoroutine( DisableKettle() );
                ResetCounts();
            }
        }
    }
    
    public void AddIngredient( Ingredient i ){

        if( !canSpawnPotion || isSpawning){
            return;
        }
        
        transform.DOPunchScale( new Vector3( 0.9f , 0.9f , 0.9f ) , .2f );
        
        switch( i.TypeOfIngredient ){
            case Ingredient.Type.Fire:   
                fireCount++;
                KettleSprite.sprite = Red;
                particleSys.GetComponent<ParticleSystem>().startColor = Color.red;
                break;
            case Ingredient.Type.Slime:  
                slimeCount++; 
                KettleSprite.sprite = Green;
                particleSys.GetComponent<ParticleSystem>().startColor = Color.green;
                break;
            case Ingredient.Type.Poison: 
                poisonCount++; 
                KettleSprite.sprite = Purple;
                particleSys.GetComponent<ParticleSystem>().startColor = Color.magenta;
                break;
        }
    }

    private void ResetCounts(){
        fireCount = 0;
        slimeCount = 0;
        poisonCount = 0;
    }
    
    private IEnumerator SpawnPotion( GameObject potion ){
        StartCoroutine( ProgressBar(6) );
        isSpawning = true;
        yield return new WaitForSeconds(6f);
        isSpawning = false;
        KettleSprite.sprite = White;
        particleSys.GetComponent<ParticleSystem>().startColor = Color.white;
        GameObject g = Instantiate( potion , potionSpawnpoint.transform.position , Quaternion.identity );
        g.transform.parent = potionSpawnpoint.transform;
        //StartCoroutine( TooLate( g ) );
    }

    private IEnumerator DisableKettle(){
        ResetCounts();
        kettleCol.enabled = false;
        canSpawnPotion = false;
        WoodSprite.sprite = Wood_OFF;
        KettleSprite.sprite = Broken;
        light.SetActive( false );
        particleSys.SetActive( false );
        yield return new WaitForSeconds(5f);
        kettleCol.enabled = true;
        canSpawnPotion = true;
        WoodSprite.sprite = Wood_ON;
        KettleSprite.sprite = White;
        
        light.SetActive( true );
        particleSys.SetActive( true );
        particleSys.GetComponent<ParticleSystem>().startColor = Color.white;
    }

    private IEnumerator TooLate( GameObject g ){
        yield return new  WaitForSeconds(10f);
        Destroy( g );
        StartCoroutine( DisableKettle() );
    }

    private IEnumerator ProgressBar(int steps){
        progressBar.SetActive(true);
        BarScript script = progressBar.GetComponent< BarScript >();
        for( int i = 0 ; i < steps ; i++ ){
            yield return new WaitForSeconds(1f);
            Debug.Log("FILL");
            script.IncreaseProgress();
            
        }
        progressBar.SetActive(false);
        script.ResetBar();
    }
}
