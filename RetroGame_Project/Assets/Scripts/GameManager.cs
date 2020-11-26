using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [ SerializeField ] private GameObject playerOne;
    [ SerializeField ] private GameObject playerTwo;
    [ SerializeField ] private GameObject healthPlayerTwo;
    [ SerializeField ] private GameObject healthPlayerOne;
    [ SerializeField ] private GameObject dark;
    [ SerializeField ] private GameObject winnerMenu;
    [ SerializeField ] private Text       winner;
    
     private Player playerOneScript;
     private Player playerTwoScript;
    private  bool   isWinner = false;

    private void Start(){
        playerOneScript = playerOne.GetComponent< Player >();
        playerTwoScript = playerTwo.GetComponent< Player >();
    }

    // Update is called once per frame
    void Update()
    {
        if( playerOneScript.CurrentHealth <= 0 && isWinner == false){
            winnerMenu.SetActive(true);
            winnerMenu.transform.DOMoveY( -4f , 1f );
            winner.text = "Player2";
            isWinner = true;
            dark.SetActive(true);
            GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("Witch");
        }
        
        if( playerTwoScript.CurrentHealth <= 0 && isWinner == false){
            winnerMenu.SetActive(true);
            winnerMenu.transform.DOMoveY( -4f , 1f );
            winner.text = "Player1";
            dark.SetActive(true);
            isWinner = true;
            GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>().PlaySound("Witch");
        }
        
        healthPlayerOne.transform.localScale = new Vector3(playerOneScript.CurrentHealth / 100f , 1,1);
        healthPlayerTwo.transform.localScale = new Vector3(playerTwoScript.CurrentHealth / 100f , 1,1);
        
    }

    private void LoadAnotherScene( int index ){
        SceneManager.LoadScene( index );
    }
}
