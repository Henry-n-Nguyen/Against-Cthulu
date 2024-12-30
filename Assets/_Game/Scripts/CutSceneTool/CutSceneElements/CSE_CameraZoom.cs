using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CSE_CameraZoom : CutSceneElementBase
{
    //[SerializeField] private float targetSize; // another way to zoom
    [SerializeField] private float targetFOV;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private CinemachineVirtualCamera vCam;
    private Coroutine zoomCameraCoroutine;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    
    private float originalFOV;

    public override void Execute()
    {
        vCam = cutsceneHandler.vCam;
        vCam.Follow = null;
        zoomCameraCoroutine = StartCoroutine(ZoomCamera());
    }

    private IEnumerator ZoomCamera()
    {
        originalPosition = vCam.transform.position;
        targetPosition = target.position + offset;

        originalFOV = vCam.m_Lens.FieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            vCam.m_Lens.FieldOfView = Mathf.Lerp(originalFOV, targetFOV, t);
            vCam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        vCam.m_Lens.FieldOfView = targetFOV;
        vCam.transform.position = targetPosition;

        cutsceneHandler.PlayNextElement();
    }

    public override void Release()
    {
        vCam.m_Lens.FieldOfView = originalFOV;
        vCam.Follow = GamePlayManager.Ins.player.characterTF;
        if (zoomCameraCoroutine != null) StopCoroutine(zoomCameraCoroutine);
    }
}
