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

   private LocationType locationType;

   private void Start(){
      switch( this.gameObject.tag ){
         case "CuttingBoard":
            locationType = LocationType.CuttingBoard;
            break;
         case "SoupBowl":
            locationType = LocationType.SoupBowl;
            break;
      }
   }
   
   
}
