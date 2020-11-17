using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Veggie : MonoBehaviour
{
    public enum Type
    {
        Alive,
        Dead,
        CutInPieces,
    }
    private Type state;

    [ SerializeField ] private float      speed;
    [ SerializeField ] private GameObject player;
    [ SerializeField ] private float      minDis;

    private void Start(){
        state = Type.Alive;
    }

    public Type State{
        get => state;
        set => state = value;
    }
}
