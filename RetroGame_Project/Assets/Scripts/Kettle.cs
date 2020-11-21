using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [ SerializeField ] private GameObject progressBar;
    
    
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
        StartCoroutine( ProgressBar(6) );
        yield return new WaitForSeconds(6f);
       //wait for x seconds to brew potion NEEDS FEEDBACK
        GameObject g = Instantiate( potion , potionSpawnpoint.transform.position , Quaternion.identity );
        g.transform.parent = potionSpawnpoint.transform;
        StartCoroutine( TooLate( g ) );
    }

    private IEnumerator DisableKettle(){
        kettleCol.enabled = false;
        canSpawnPotion = false;
        //Change to broken sprite
        ResetCounts();
        yield return new WaitForSeconds(5f);
        kettleCol.enabled = true;
        canSpawnPotion = true;
        //Change back to working sprite
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
