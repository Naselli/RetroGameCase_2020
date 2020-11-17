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

    private void Update(){
        //float range = Vector2.Distance( transform.position , player.transform.position );
        //if( range > minDis )
        //    transform.position = Vector2.MoveTowards( transform.position , player.transform.position , speed * Time.deltaTime );
        //if( range < minDis )
        //    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -1 * speed * Time.deltaTime);
    }

    public Type State{
        get => state;
        set => state = value;
    }
}
