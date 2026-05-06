using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreviewObjectValidChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private LayerMask invalidLayers;
    public bool IsValid { get; private set; } = true;
    [SerializeField] private HashSet<Collider> _collidingObjects = new HashSet<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"Collider entered to check");
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            _collidingObjects.Add(other);
            IsValid = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Debug.Log($"Collider exited check");
        if (((1 << other.gameObject.layer) & invalidLayers) != 0)
        {
            _collidingObjects.Remove(other);
            IsValid = _collidingObjects.Count <= 0;
        }
    }
}
