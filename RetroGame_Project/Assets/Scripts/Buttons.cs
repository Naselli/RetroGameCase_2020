using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

 public class Buttons : MonoBehaviour
 {

     [ SerializeField ] private GameObject menu;
     [ SerializeField ] private GameObject winnerMenu;
     [ SerializeField ] private SpriteRenderer bookSpriteRender;
     [ SerializeField ] private Sprite controlsSprite;
     [ SerializeField ] private Sprite normalBook;
     
     
     
    
    private void Start(){
        Time.timeScale = 0f;
    }

    public void LoadAnotherScene( int index ){
        menu.transform.DOMoveY( -18f , 1f );
        winnerMenu.transform.DOMoveY( -18f , 1f );
        Time.timeScale = 1f;
     }
    
    public void Quit(  ){
        Application.Quit();
    }

    public void SwitchControlSprites(){
        if( bookSpriteRender.sprite == normalBook ){
            bookSpriteRender.sprite = controlsSprite;
        }
        else if (bookSpriteRender.sprite == controlsSprite){
            bookSpriteRender.sprite = normalBook;
        }
        
    }

     public void switchMenu(){
         //SceneManager.LoadScene( 0 );

         StartCoroutine( Switch() );
     }

     IEnumerator Switch(){
         winnerMenu.transform.DOMoveY( -18f , 1f );
         yield return new WaitForSeconds(1f);
         winnerMenu.SetActive(false);
         menu.transform.DOMoveY( -3.5f , 1f );
         yield return new WaitForSeconds(1f);
         SceneManager.LoadScene( 0 );
     }
}
