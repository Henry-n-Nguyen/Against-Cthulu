using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneElementBase : MonoBehaviour
{
    protected CutSceneHandler cutsceneHandler;
    
    public float duration;

    private void Start()
    {
        OnInit();
    }

    protected void OnInit()
    {
        cutsceneHandler = GetComponent<CutSceneHandler>();
    }

    public virtual void Excecute() { }

    public virtual void Release() { }

    public IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(duration);
        cutsceneHandler.PlayNextElement();
    }
}
