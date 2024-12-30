using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class CutSceneUI : MonoBehaviour
{
    public CS_UIType type;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Execute()
    {
        anim.Play("Execute");
    }

    public void Release()
    {
        anim.Play("Release");
    }
}
