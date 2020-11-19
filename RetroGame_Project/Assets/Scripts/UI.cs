using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [ SerializeField ] private GameObject playerOne;
    [ SerializeField ] private GameObject playerTwo;

    [ SerializeField ] private Text playerOneHealth;
    [ SerializeField ] private Text playerTwoHealth;

    private void Update(){
        playerOneHealth.text = playerOne.GetComponent< Player >().CurrentHealth.ToString();
        playerTwoHealth.text = playerTwo.GetComponent< Player >().CurrentHealth.ToString();
    }
}
