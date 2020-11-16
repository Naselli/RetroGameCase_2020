using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private GameObject[] listOfLights;
    private GameObject[] listOfSpawnpoints;
    
    [ SerializeField ] private float      changeLightTimer;
    [ SerializeField ] private float      spawnGhostTimer;
    [ SerializeField ] private GameObject ghostPrefab;

    private void Start(){
        listOfLights = GameObject.FindGameObjectsWithTag( "Light" );
        listOfSpawnpoints = GameObject.FindGameObjectsWithTag( "Spawnpoint" );
        foreach( GameObject l in listOfLights ){
            l.SetActive(false);
        }
        StartCoroutine( BlinkLight() );
        StartCoroutine( SpawnGhost() );
    }

    private IEnumerator BlinkLight(){

        while( true ){
            int g = Random.Range( 0 , 5 );
            listOfLights[g].SetActive(true);
            yield return new WaitForSeconds( changeLightTimer );
            listOfLights[g].SetActive(false);
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator SpawnGhost(){
        while( true ){
            int i = Random.Range( 0 , 7 );
            Instantiate( ghostPrefab , listOfSpawnpoints[ i ].transform.position , Quaternion.identity );
            yield return new WaitForSeconds(spawnGhostTimer);
        }
       
    }
}
