using UnityEngine;
using Utilities.Constants;

namespace Helper
{
    public class LayerChanger : MonoBehaviour
    {
        [SerializeField] private string _defaultLayer;
        [SerializeField] private string _changedLayer;
    
        private void OnTriggerEnter(Collider other)
        {
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
}
