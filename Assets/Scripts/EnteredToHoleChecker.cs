using Core.Signal.Hole;
using UnityEngine;
using Utilities.Constants;
using Zenject;

public class EnteredToHoleChecker : MonoBehaviour
{
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Absorbable) || other.CompareTag(Tags.NonAbsorbable))
        {
            _signalBus.TryFire(new EnteredToHoleSignal(other.tag));
            Destroy(other.gameObject);
        }
    }
}
