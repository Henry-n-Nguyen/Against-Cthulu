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

    [TextArea][SerializeField] private List<string> dialogues;

    [SerializeField] private bool isCanInteract;

    private Coroutine writingCoroutine;

    private int index = 0;
    private int charIndex;

    private bool started;
    private bool waitForNext;

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (!started)
        {
            if (!isCanInteract) return;

            if (Input.GetButtonDown("Interact")) StartDialogue();
        }

        endSentenceSign.SetActive(waitForNext);

        if (waitForNext && Input.GetButtonDown("Interact"))
        {
            waitForNext = false;
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
            }
        }
    }

    public override void Excecute()
    {
        ToggleWindow(false);
        ToggleIndicator(false);

        nameTag.text = name;
        if (isCanInteract) ToggleIndicator(true);
    }

    public override void Release()
    {
        ToggleIndicator(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }
    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    //Start Dialogue
    public void StartDialogue()
    {
        if (started)
            return;

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
        writingCoroutine = StartCoroutine(Writing());
    }

    //End Dialogue
    public void EndDialogue()
    {
        started = false;
        waitForNext = false;

        StopCoroutine(writingCoroutine);

        ToggleWindow(false);

        cutsceneHandler.PlayNextElement();
    }

    //Writing logic
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        //Write the character
        dialogueText.text += currentDialogue[charIndex];
        //increase the character index
        charIndex++;
        //Make sure you have reached the end of the sentence
        if (charIndex < currentDialogue.Length)
        {
            //Wait x seconds 
            yield return new WaitForSeconds(writingSpeed);
            //Restart the same process
            StartCoroutine(Writing());
        }
        else
        {
            //End this sentence and wait for the next one
            waitForNext = true;
        }
    }
}
