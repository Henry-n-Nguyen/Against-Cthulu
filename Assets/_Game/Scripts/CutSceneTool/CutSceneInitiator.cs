using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneInitiator : MonoBehaviour
{
    private CutSceneHandler cutsceneHandler;

    private void Start()
    {
        cutsceneHandler = GetComponent<CutSceneHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollideWithPlayer(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EndCollideWithPlayer(collision);
    }

    private void CollideWithPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_PLAYER))
        {
            cutsceneHandler.isDetectedPlayer = true;
            cutsceneHandler.PlayNextElement();
        }
    }

    private void EndCollideWithPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_PLAYER))
        {
            cutsceneHandler.isDetectedPlayer = false;
            cutsceneHandler.ReleaseAllElement();
        }
    }
}
