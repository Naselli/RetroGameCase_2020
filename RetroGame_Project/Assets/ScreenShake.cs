using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShake : MonoBehaviour
{
    public void Shake(float duration){
        transform.DOShakePosition( duration );
    }
}
