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
        if (collision.gameObject.CompareTag(S_Constant.TAG_PLAYER))
        {
            cutsceneHandler.PlayNextElement();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_PLAYER))
        {
            cutsceneHandler.ReleaseAllElement();
        }
    }
}
