using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public enum PotionType
    {
        Fire,
        Slime,
        Poison,
    }

    [SerializeField] private PotionType typeOfPotion;

    private SpriteRenderer sR;
    
    void Start()
    {
        sR = GetComponent< SpriteRenderer >();

        switch( typeOfPotion ){
            case PotionType.Fire: sR.color = Color.red;
                break;
            case PotionType.Slime: sR.color = Color.green;
                break;
            case PotionType.Poison: sR.color = Color.magenta;
                break;
        }
    }
    
    void Update()
    {
        
    }
    
    // 3 function for diff states of potion;
}
