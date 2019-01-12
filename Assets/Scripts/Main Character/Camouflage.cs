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
    private SkinnedMeshRenderer[] meshRenderers;
    private bool isCamouflaging = false;
    /// <summary>
    /// To be affected by cooldown
    /// </summary>
    private bool canCAmouflage = true;
    private float camouflageCooldownTimer = 0;
    private float camouflageTimer = 0;

	// Use this for initialization
	void Start () {
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        camouflageCooldownTimer -= Time.deltaTime;
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
	}

    void ToggleCamouflage(bool on)
    {
        isCamouflaging = on;
        foreach (SkinnedMeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = !isCamouflaging;
        }

        // If toggled to visible...
        if (!isCamouflaging)
        {
            //... start timer until can camouflage again
            camouflageCooldownTimer = cooldownTime;
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
        if (!isCamouflaging && camouflageCooldownTimer <= 0)
        {
            canCAmouflage = true;            
        }
    }
}
