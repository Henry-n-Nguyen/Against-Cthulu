using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    public GameObject target = null;

    [SerializeField] private Transform cameraTransform;

    private void Start()
    {
        MoveCameraFollowTarget();
    }

    void LateUpdate()
    {
        //MoveCameraFollowTarget();
    }

    private void MoveCameraFollowTarget()
    {
        if (target != null)
        {
            //Vector3 targetPosition = target.transform.position + offset;
            Vector3 targetPosition = offset;
            cameraTransform.position = targetPosition;
        }
    }
}
