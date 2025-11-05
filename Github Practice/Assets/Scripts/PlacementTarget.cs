using UnityEngine;
using System;

public class PlacementTarget : MonoBehaviour
{
    public string matchingTag;
    public bool isPlaced = false;

    public static event Action<bool> OnAnyPlacementChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(matchingTag) && !isPlaced)
        {
            isPlaced = true;
            Debug.Log($"{matchingTag} Right Position!");
            OnAnyPlacementChanged?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(matchingTag) && isPlaced)
        {
            isPlaced = false;
            Debug.Log($"{matchingTag} Grab!");
            OnAnyPlacementChanged?.Invoke(false);
        }
    }
}