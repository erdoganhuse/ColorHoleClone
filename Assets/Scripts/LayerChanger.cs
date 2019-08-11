using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Constants;

public class LayerChanger : MonoBehaviour
{
    [SerializeField] private string _defaultLayer;
    [SerializeField] private string _changedLayer;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name}");
        
        if (other.CompareTag(Tags.Absorbable) || other.CompareTag(Tags.NonAbsorbable))
        {
            other.gameObject.layer = LayerMask.NameToLayer(_changedLayer);   
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Absorbable) || other.CompareTag(Tags.NonAbsorbable))
        {
            other.gameObject.layer = LayerMask.NameToLayer(_defaultLayer);
        }
    }
}
