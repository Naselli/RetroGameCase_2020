using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum Type
    {
        Fire,
        Slime,
        Poison,
    }
    [SerializeField] private Type typeOfIngredient;

    private SpriteRenderer sR;

    private void Start(){
        sR = GetComponent< SpriteRenderer >();

        switch( TypeOfIngredient ){
            case Type.Fire: sR.color = Color.red;
                break;
            case Type.Slime: sR.color = Color.green;
                break;
            case Type.Poison: sR.color = Color.magenta;
                break;
        }
    }
    
    public Type TypeOfIngredient{
        get => typeOfIngredient;
        set => typeOfIngredient = value;
    }
}
