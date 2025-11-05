using UnityEngine;

public class DisablePassthroughAndFixSkybox : MonoBehaviour
{
    public Material skyboxMaterial;

    void Start()
    {
        var passthrough = FindObjectOfType<OVRPassthroughLayer>();
        if (passthrough != null)
        {
            passthrough.enabled = false;
            Debug.Log("Passthrough disabled ¡ª should now see Skybox.");
        }

        var oldRig = GameObject.Find("XR Origin") ?? GameObject.Find("OVRCameraRig");
        if (oldRig != null)
        {
            Destroy(oldRig);
            Debug.Log("Old XR/AR rig destroyed.");
        }

        var cam = Camera.main;
        if (cam != null)
        {
            cam.clearFlags = CameraClearFlags.Skybox;
        }

        if (skyboxMaterial != null)
        {
            RenderSettings.skybox = skyboxMaterial;
            DynamicGI.UpdateEnvironment();
        }

        Debug.Log("VR Scene Skybox restored successfully.");
    }
}