using UnityEngine;

namespace Prototype_Ghostbuster.Scripts
{
    public static class ExtensionMethods
    {
        public static Vector2 ConvertTovector2( Vector3 v ) => new Vector2(v.x, v.y);
    
    }
}
