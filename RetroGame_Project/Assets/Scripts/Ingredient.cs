using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using DG.Tweening;

public class Ingredient : MonoBehaviour
{
    public enum Type
    {
        Fire,
        Slime,
        Poison,
    }
    [SerializeField] private Type typeOfIngredient;

    private void Start(){
        transform.DOPunchScale( new Vector3(1,1,1) , .2f  );
        StartCoroutine( scale() );
    }



    public Type TypeOfIngredient{
        get => typeOfIngredient;
        set => typeOfIngredient = value;
    }

    IEnumerator scale(){
        while( true ){
            yield return new WaitForSeconds(2f);
            transform.DOShakeScale( 1f );
        }
    }
}
