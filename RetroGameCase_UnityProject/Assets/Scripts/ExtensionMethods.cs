using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 ConvertTovector2( Vector3 v ) => new Vector2(v.x, v.y);
    
}
