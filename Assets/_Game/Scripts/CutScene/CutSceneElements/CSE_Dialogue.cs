using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CSE_Dialogue : CutSceneElementBase
{
    // References
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject window;
    [SerializeField] private TMP_Text nameTag;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject endSentenceSign;
    [SerializeField] private float writingSpeed;

    [TextArea][SerializeField] private List<string> dialogues = new List<string>();

    [SerializeField] private bool isCanInteract;

    private Coroutine writingCoroutine;

    private int index = 0;
    private int charIndex = 0;

    private bool started;
    private bool waitForNext;

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (!cutsceneHandler.isDetectedPlayer)
        {
            return;
        }

        if (!started)
        {
            if (!isCanInteract)
            {
                return;
            }

            if (isCanInteract && Input.GetButtonDown("Interact"))
            {
                StartDialogue();
            }
        }

        if (waitForNext && Input.GetButtonDown("Interact"))
        {
            waitForNext = false;
            endSentenceSign.SetActive(waitForNext);

            index++;

            //Check if we are in the scope fo dialogues List
            if (index < dialogues.Count)
            {
                //If so fetch the next dialogue
                GetDialogue(index);
            }
            else
            {
                //If not end the dialogue process
                if (isCanInteract) ToggleIndicator(true);
                EndDialogue();
                
                if (!isCanInteract)
                {
                    cutsceneHandler.ReleaseAllElement();
                }
                
                cutsceneHandler.PlayNextElement();
            }
        }
    }

    public override void Execute()
    {
        waitForNext = false;

        nameTag.text = name;

        if (isCanInteract) ToggleIndicator(true);
        else StartDialogue();
    }

    public override void Release()
    {
        if (writingCoroutine != null) StopCoroutine(writingCoroutine);
        ToggleIndicator(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    private void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    //Start Dialogue
    private void StartDialogue()
    {
        if (started)
        {
            return;
        }

        started = true;

        ToggleWindow(true);
        ToggleIndicator(false);

        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        index = i;
        charIndex = 0;
        dialogueText.text = string.Empty;

        if (writingCoroutine != null) StopCoroutine(writingCoroutine);
        writingCoroutine = StartCoroutine(Writing());
    }

    //End Dialogue
    private void EndDialogue()
    {
        started = false;
        waitForNext = false;

        ToggleWindow(false);
    }

    //Writing logic
    private IEnumerator Writing()
    {
        waitForNext = false;

        string currentDialogue = dialogues[index];
        
        //Make sure you have reached the end of the sentence
        while (charIndex < currentDialogue.Length)
        {
            //Wait x seconds 
            yield return new WaitForSeconds(writingSpeed);
            //Write the character
            dialogueText.text += currentDialogue[charIndex];
            //increase the character index
            charIndex++;
        }

        //End this sentence and wait for the next one
        endSentenceSign.SetActive(waitForNext);
        waitForNext = true;
    }
}
