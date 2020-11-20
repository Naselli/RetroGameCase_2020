using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [ SerializeField ] private GameObject playerOne;
    [ SerializeField ] private GameObject playerTwo;
    
     private Player playerOneScript;
     private Player playerTwoScript;

    private void Start(){
        playerOneScript = playerOne.GetComponent< Player >();
        playerTwoScript = playerTwo.GetComponent< Player >();
    }

    // Update is called once per frame
    void Update()
    {
        if( playerOneScript.CurrentHealth <= 0 ){
            LoadAnotherScene(1);
        }
        
        if( playerTwoScript.CurrentHealth <= 0 ){
            LoadAnotherScene(1);
        }
    }

    private void LoadAnotherScene( int index ){
        SceneManager.LoadScene( index );
    }
}
