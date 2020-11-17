using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
   public enum LocationType
   {
      CuttingBoard,
      SoupBowl,
   }

   private Veggie veggieOnLocation;

   private LocationType type;

   private void Start(){
      switch( this.gameObject.tag ){
         case "CuttingBoard":
            type = LocationType.CuttingBoard;
            break;
         case "SoupBowl":
            type = LocationType.SoupBowl;
            break;
      }
   }
   public LocationType Type{
      get => type;
      set => type = value;
   }

}
