using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using HuySpace;

public class CSE_TransitionScene : CutSceneElementBase
{
    [SerializeField] private GameObject indicator;

    private SpriteRenderer spriteRenderer;
    private Coroutine transitionCoroutine;

    private bool isActivated = false;
    private bool playerDetected = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        isActivated = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.25f);
    }

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (!StageManager.Ins.currentStage.IsEndStage) { return; }

        if (StageManager.Ins.currentStage.IsEndStage) Activate();

        if (playerDetected && Input.GetButtonDown("Interact"))
        {
            CutSceneUIManager.Ins.Execute(CS_UIType.TransCam);
            StageManager.Ins.EndChapter();
            CutSceneUIManager.Ins.Release(CS_UIType.TransCam);

            if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);

            if (transitionCoroutine != null) transitionCoroutine = StartCoroutine(WaitAndAdvance());
        }
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
            playerDetected = true;
            if (StageManager.Ins.currentStage.IsEndStage) ToggleIndicator(true);
        }
    }

    private void EndCollideWithPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_PLAYER))
        {
            playerDetected = false;
            ToggleIndicator(false);
        }
    }

    public override void Execute()
    {
        isActivated = false;
        playerDetected = false;
    }

    private void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    private void Activate()
    {
        isActivated = true;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }
}
