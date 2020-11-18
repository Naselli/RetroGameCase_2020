using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettle : MonoBehaviour
{
    [Header("item counters for debug")]
    [ SerializeField ] private int      fireCount;
    [ SerializeField ] private int      slimeCount;
    [ SerializeField ] private int      poisonCount;
    
    [Space]
    [ SerializeField ] private Collider2D kettleCol;
    [ SerializeField ] private GameObject potionSpawnpoint;
    [ SerializeField ] private bool       canSpawnPotion = true;
    [ SerializeField ] private GameObject brewingSprite;
    
    
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
        switch( i.TypeOfIngredient ){
            case Ingredient.Type.Fire:   fireCount++; break;
            case Ingredient.Type.Slime:  slimeCount++; break;
            case Ingredient.Type.Poison: poisonCount++; break;
        }
    }

    private void ResetCounts(){
        fireCount = 0;
        slimeCount = 0;
        poisonCount = 0;
    }
    
    private IEnumerator SpawnPotion( GameObject potion ){
        
        yield return new WaitForSeconds(3f); //wait for x seconds to brew potion
        GameObject g = Instantiate( potion , potionSpawnpoint.transform.position , Quaternion.identity );
        g.transform.parent = potionSpawnpoint.transform;

    }

    private IEnumerator DisableKettle(){
        kettleCol.enabled = false;
        canSpawnPotion = false;
        //Change to broken sprite
        yield return new WaitForSeconds(5f);
        kettleCol.enabled = true;
        canSpawnPotion = true;
        //Change back to working sprite
    }
    
    
}
