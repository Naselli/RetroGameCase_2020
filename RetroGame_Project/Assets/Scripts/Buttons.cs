using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

 public class Buttons : MonoBehaviour
{
    
    static public void LoadAnotherScene( int index ){
        SceneManager.LoadScene( index );
    }
}
