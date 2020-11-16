using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GunPlayerOne : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private string     inputSuck;
    private Player     playerScript;

    private void Start(){
        playerScript = player.GetComponent< Player >();
    }

    private void OnTriggerStay2D( Collider2D other ){
        if( Input.GetButtonDown(inputSuck) ){
            if( other.CompareTag("Ghost") ){
                Debug.Log(other.gameObject.name);
                other.GetComponent< CircleCollider2D >().enabled = false;
                other.GetComponent< CircleCollider2D >().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(player.transform.position - other.gameObject.transform.position * 2, ForceMode2D.Impulse);
                //StartCoroutine( Sucked( other ) );
                playerScript.AmountCaught++;
                Debug.Log("Caught");
                Destroy(other.gameObject, .5f);
            }
        }
    }

    //private IEnumerator Sucked(Collider2D other){
    //    other.transform.position = Vector2.MoveTowards( player.transform.position, other.transform.position , 1f * Time.deltaTime );
    //    yield return null;
    //}
}
