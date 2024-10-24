using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight_SubStage : MonoBehaviour
{
    private PolygonCollider2D bossFightBound;
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;

    private void Start()
    {
        bossFightBound = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollideWithPlayer(collision);
    }

    private void CollideWithPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_PLAYER))
        {
            OnFight();
            UIManager.Ins.OpenUI<UI_BossFight>();
        }
    }

    public void OnFight()
    {
        GamePlayManager.Ins.confiner.m_BoundingShape2D = bossFightBound;
        GamePlayManager.Ins.confiner.InvalidateCache();

        leftBorder.SetActive(true);
        rightBorder.SetActive(true);
    }

    public void OnComplete()
    {
        UIManager.Ins.CloseDirectly<UI_BossFight>();

        GamePlayManager.Ins.confiner.m_BoundingShape2D = StageManager.Ins.currentStage.mapBound;
        GamePlayManager.Ins.confiner.InvalidateCache();

        leftBorder.SetActive(false);
        rightBorder.SetActive(false);
    }
}
