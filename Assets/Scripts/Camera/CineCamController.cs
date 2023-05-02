using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineCamController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerVCam;
    private float minCamZoom = 40.0f;
    private float maxCamZoom = 60.0f;

    void LateUpdate()
    {
        // Camera Zoom Control
        if (Input.mouseScrollDelta.y > 0 && playerVCam.m_Lens.FieldOfView >= minCamZoom)
        {
            playerVCam.m_Lens.FieldOfView -= 1 * Time.deltaTime * 200; // Zoom In
        }
        else if (Input.mouseScrollDelta.y < 0 && playerVCam.m_Lens.FieldOfView <= maxCamZoom)
        {
            playerVCam.m_Lens.FieldOfView += 1 * Time.deltaTime * 200; // Zoom Out
        }
    }
}
