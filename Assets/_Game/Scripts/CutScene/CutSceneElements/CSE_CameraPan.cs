using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CSE_CameraPan : CutSceneElementBase
{
    [SerializeField] private Vector3 distanceToMove;

    private CinemachineVirtualCamera vCam;
    private Coroutine panCameraCoroutine;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    public override void Excecute()
    {
        vCam = cutsceneHandler.vCam;
        vCam.Follow = null;
        panCameraCoroutine = StartCoroutine(PanCamera());
    }

    private IEnumerator PanCamera()
    {
        originalPosition = vCam.transform.position;
        targetPosition = originalPosition + distanceToMove;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            vCam.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        vCam.transform.position = targetPosition;

        cutsceneHandler.PlayNextElement();
    }

    public override void Release()
    {
        vCam.Follow = GamePlayManager.instance.player.characterTF;
        if (panCameraCoroutine != null) StopCoroutine(panCameraCoroutine);
    }
}
