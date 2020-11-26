using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;


 public class Buttons : MonoBehaviour
 {

     [ SerializeField ] private GameObject     menu;
     [ SerializeField ] private GameObject     winnerMenu;
     [ SerializeField ] private GameObject     pauseMenu;
     [ SerializeField ] private SpriteRenderer bookSpriteRender;
     [ SerializeField ] private Sprite         controlsSprite;
     [ SerializeField ] private Sprite         normalBook;
     [ SerializeField ] private GameObject     dark;
     [ SerializeField ] private GameObject     pauseMenuShow;


     private void Start(){
        Time.timeScale = 0f;
    }

     private void Update(){
         if( Input.GetKeyDown(KeyCode.Escape) ){
             StartCoroutine( Pause() );
         }
     }

     public void LoadAnotherScene( int index ){
        menu.transform.DOMoveY( -18f , 1f );
        winnerMenu.transform.DOMoveY( -18f , 1f );
         pauseMenuShow.transform.DOMoveY( -18f , 1f );
        dark.SetActive(false);
        Time.timeScale = 1f;
     }

     IEnumerator  Pause(){
         pauseMenuShow.SetActive(true);
         pauseMenuShow.transform.DOMoveY( -4f , 1f );
         yield return new WaitForSeconds(1f);
         Time.timeScale = 0f;
     }

     public IEnumerator  UnPause(){
         Time.timeScale = 1f;
         pauseMenuShow.transform.DOMoveY( -18f , 1f );
         yield return new WaitForSeconds(1f);
         pauseMenuShow.SetActive(false);
         
     }

     public void Unpause(){
         StartCoroutine( UnPause() );
     }
     
    public void Quit(  ){
        Application.Quit();
    }

    public void SwitchControlSprites(){
        if( bookSpriteRender.sprite == normalBook ){
            bookSpriteRender.sprite = controlsSprite;
        }
        else if (bookSpriteRender.sprite == controlsSprite)
            bookSpriteRender.sprite = normalBook;
        
        
    }

     public  void switchMenu(){
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
