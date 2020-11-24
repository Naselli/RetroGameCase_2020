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

    public Type TypeOfIngredient{
        get => typeOfIngredient;
        set => typeOfIngredient = value;
    }
}
