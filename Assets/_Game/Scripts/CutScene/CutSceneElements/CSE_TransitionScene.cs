using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using HuySpace;

public class CSE_TransitionScene : CutSceneElementBase
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private Stage stage;

    private SpriteRenderer spriteRenderer;
    private Coroutine transitionCoroutine;

    private bool isActivated = false;
    private bool playerDetected = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (!stage.IsEndStage) { return; }

        if (stage.IsEndStage && !isActivated) Activate();

        if (playerDetected && Input.GetButtonDown("Interact"))
        {
            CutSceneUIManager.Ins.Execute(CS_UIType.TransCam);
            GamePlayManager.Ins.player.TeleportTo(stage.spawnPoint.transform.position);
            transitionCoroutine = StartCoroutine(WaitAndAdvance());
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
            if (stage.IsEndStage) ToggleIndicator(true);
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

    public override void Release()
    {
        StopCoroutine(transitionCoroutine);
        CutSceneUIManager.Ins.Release(CS_UIType.TransCam);
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
