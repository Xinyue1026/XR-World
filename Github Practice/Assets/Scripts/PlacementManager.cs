using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacementManager : MonoBehaviour
{
    [Header("Scene Transition Settings")]
    [SerializeField] private string sceneToLoad = "NextScene";
    [SerializeField] private int totalParts = 5;
    [SerializeField] private float delayBeforeTransition = 1f;

    private int placedCount = 0;
    private bool hasTriggered = false;

    private void Start()
    {
        PlacementTarget.OnAnyPlacementChanged += OnPartPlaced;
    }

    private void OnDestroy()
    {
        PlacementTarget.OnAnyPlacementChanged -= OnPartPlaced;
    }

    private void OnPartPlaced(bool placed)
    {
        if (hasTriggered) return;

        if (placed)
        {
            placedCount++;
            Debug.Log($"Right Position! Count£º{placedCount}/{totalParts}");
        }
        else
        {
            placedCount = Mathf.Max(0, placedCount - 1);
            Debug.Log($"Grab! Count:{placedCount}/{totalParts}");
        }

        if (placedCount >= totalParts && !hasTriggered)
        {
            hasTriggered = true;
            Debug.Log("All Complete, Scene Transit...");
            Invoke(nameof(LoadNextScene), delayBeforeTransition);
        }
    }

    private void LoadNextScene()
    {
        if (string.IsNullOrEmpty(sceneToLoad))
        {
            Debug.LogError("No Scene Name");
            return;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}