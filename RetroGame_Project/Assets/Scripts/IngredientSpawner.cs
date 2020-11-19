using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IngredientSpawner : MonoBehaviour
{

    [ SerializeField ] private bool isSpawning;
    
    [ SerializeField ] private GameObject fireIngredient;
    [ SerializeField ] private GameObject slimeIngredient;
    [ SerializeField ] private GameObject poisonIngredient;
    
    
    private void Start(){
        switch( Random.Range(1,4) ){
            case 1: SpawnIngredient(fireIngredient); break;
            case 2: SpawnIngredient(slimeIngredient);break;
            case 3: SpawnIngredient(poisonIngredient);break;
        }
    }

    private void Update(){
        if( transform.childCount != 1 && !isSpawning){
            Debug.Log("Start growing new one");
            StartCoroutine( WaitForSpawn() );
        }
    }

    private IEnumerator WaitForSpawn(){
        isSpawning = true; 
        yield return new WaitForSeconds( 10f );
        switch( Random.Range(1,4) ){
            case 1: SpawnIngredient(fireIngredient); break;
            case 2: SpawnIngredient(slimeIngredient);break;
            case 3: SpawnIngredient(poisonIngredient);break;
        }
        isSpawning = false;
    }

    private void SpawnIngredient(GameObject i){
        GameObject g = Instantiate( i , transform.position , Quaternion.identity );
        g.transform.parent = transform;
    }
}
