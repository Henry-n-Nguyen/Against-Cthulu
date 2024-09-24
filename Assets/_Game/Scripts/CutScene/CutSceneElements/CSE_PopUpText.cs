using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HuySpace;

public class CSE_PopUpText : CutSceneElementBase
{
    [SerializeField] private TMP_Text popUpText;
    [SerializeField] private Animator anim;

    [TextArea] [SerializeField] private string dialogue;
    [SerializeField] private TextPosition textPosition;

    private bool isTextActive;

    private float timer = 0f;

    private void Update()
    {
        if (isTextActive)
        {
            timer += Time.deltaTime;

            if (timer > duration)
            {
                timer = 0f;

                isTextActive = false;
                anim.Play("FadeOut");

                cutsceneHandler.PlayNextElement();
            }
        }
    }

    public override void Excecute()
    {
        SetTextPosition();

        anim.Play("FadeIn");
        isTextActive = true;
        popUpText.text = dialogue;
    }

    public void SetTextPosition()
    {
        RectTransform rectTransform = popUpText.rectTransform;

        switch (textPosition)
        {
            case TextPosition.Top:
                rectTransform.anchoredPosition = new Vector2(0, 425);
                break;
            case TextPosition.Middle:
                rectTransform.anchoredPosition = new Vector2(0, 0);
                break;
            case TextPosition.Bottom:
                rectTransform.anchoredPosition = new Vector2(0, -425);
                break;
        }
    }
}
