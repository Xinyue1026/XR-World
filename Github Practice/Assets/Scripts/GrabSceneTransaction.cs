using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabSceneTransition : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string sceneToLoad = "NextScene";
    [SerializeField] private float transitionDelay = 0.5f;

    [Header("Optional Transition Effects")]
    [SerializeField] private bool fadeTransition = true;
    [SerializeField] private float fadeTime = 1f;

    [Header("Assembly Settings")]
    [SerializeField] private PlacementTarget[] assemblyTargets;

    private bool hasTriggered = false;

    void OnEnable()
    {
        PlacementTarget.OnAnyPlacementChanged += OnAnyPlacementChanged;
    }

    void OnDisable()
    {
        PlacementTarget.OnAnyPlacementChanged -= OnAnyPlacementChanged;
    }

    private void Start()
    {
        TryTransitionIfAllPlaced();
    }

    private void OnAnyPlacementChanged(bool _)
    {
        TryTransitionIfAllPlaced();
    }

    private void TryTransitionIfAllPlaced()
    {
        if (hasTriggered) return;

        if (AllPartsPlaced())
        {
            hasTriggered = true;

            if (transitionDelay > 0f)
                Invoke(nameof(LoadScene), transitionDelay);
            else
                LoadScene();
        }
    }

    private bool AllPartsPlaced()
    {
        if (assemblyTargets == null || assemblyTargets.Length == 0)
        {
            Debug.LogWarning("GrabSceneTransition: No assembly targets assigned!");
            return false;
        }

        foreach (var target in assemblyTargets)
        {
            if (target == null || !target.isPlaced)
                return false;
        }
        return true;
    }

    private void LoadScene()
    {

        var grabAndLocates = FindObjectsOfType<Meta.XR.MRUtilityKit.BuildingBlocks.GrabAndLocate>();
        foreach (var component in grabAndLocates)
        {
            if (component != null) component.enabled = false;
        }

        if (fadeTransition)
            StartCoroutine(FadeAndLoadScene());
        else
            SceneManager.LoadScene(sceneToLoad);
    }

    private System.Collections.IEnumerator FadeAndLoadScene()
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
