using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camouflage : MonoBehaviour {

    /// <summary>
    /// Time after the player has been automatically toggled visible until they can camouflage again
    /// </summary>
    public float cooldownTime;
    /// <summary>
    /// Time the camouflage lasts
    /// </summary>
    public float camouflageTime;
    private MeshRenderer[] meshRenderers;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    private bool isCamouflaging = false;
    /// <summary>
    /// To be affected by cooldown
    /// </summary>
    private bool canCAmouflage = true;
    private float cooldownTimer = 0;
    private float camouflageTimer = 0;

    private CameraCanvas cameraCanvas;

	// Use this for initialization
	void Start () {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        cameraCanvas = FindObjectOfType<CameraCanvas>();
	}
	
	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;
        camouflageTimer -= Time.deltaTime;
        CheckForCamouflageTimer();
        CheckForCamouflageCooldown();

        if (Input.GetButtonDown("Camouflage"))
        {
            if (canCAmouflage)
            {
                ToggleCamouflage(true);
            }
        }

        if (isCamouflaging)
        {
            cameraCanvas.UpdateCamouflageCooldown(0);
        }
        else
        {
            float percentageOfCooldownDone = (cooldownTime - cooldownTimer) / cooldownTime;
            if (cooldownTimer <= 0)
            {
                percentageOfCooldownDone = 1;
            }
            cameraCanvas.UpdateCamouflageCooldown(percentageOfCooldownDone);
        }
	}

    void ToggleCamouflage(bool on)
    {
        isCamouflaging = on;
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = !isCamouflaging;
        }
        foreach (SkinnedMeshRenderer meshRenderer in skinnedMeshRenderers)
        {
            meshRenderer.enabled = !isCamouflaging;
        }

        // If toggled to visible...
        if (!isCamouflaging)
        {
            //... start timer until can camouflage again
            cooldownTimer = cooldownTime;
        }
        else
        {
            camouflageTimer = camouflageTime;
            canCAmouflage = false;
        }
    }

    void CheckForCamouflageTimer()
    {
        if (isCamouflaging && camouflageTimer <= 0)
        {            
            ToggleCamouflage(false);
        }
    }

    void CheckForCamouflageCooldown()
    {
        if (!isCamouflaging && cooldownTimer <= 0)
        {
            canCAmouflage = true;            
        }
    }

    public bool GetIsCamouflaging()
    {
        return isCamouflaging;
    }
}
