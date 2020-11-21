using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    [SerializeField] Slider progressBar;

    [SerializeField] int maxTier   = 6;
    [SerializeField] int value = 0;

  
    private void UpdateProgressBar()
    {
        Debug.Log(value % maxTier);
        progressBar.value = value % maxTier;
    }
    
    public void IncreaseProgress(){
        value++;
        UpdateProgressBar();
    }
    
    public void ResetBar(){
        value = 0;
    }
}
