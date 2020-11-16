using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private GameObject[] listOfLights;
    
    [ SerializeField ] private float changeLightTimer;

    private void Start(){
        listOfLights = GameObject.FindGameObjectsWithTag( "Light" );
        foreach( GameObject l in listOfLights ){
            l.SetActive(false);
        }
        StartCoroutine( BlinkLight() );
    }

    private IEnumerator BlinkLight(){

        while( true ){
            int g = Random.Range( 0 , 5 );
            listOfLights[g].SetActive(true);
            yield return new WaitForSeconds( changeLightTimer );
            listOfLights[g].SetActive(false);
        }
        
    }
}
