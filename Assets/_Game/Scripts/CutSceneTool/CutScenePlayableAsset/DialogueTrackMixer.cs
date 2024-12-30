using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Windows;

public class DialogueTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TMP_Text dialogue = playerData as TMP_Text;

        string currentDialogue = "";

        if (!dialogue) { return; }

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);

            if (inputWeight > 0)
            {
                ScriptPlayable<DialogueBehaviour> inputBehaviour = (ScriptPlayable<DialogueBehaviour>)playable.GetInput(i);

                DialogueBehaviour input = inputBehaviour.GetBehaviour();
                currentDialogue = input.dialogue;
            }
        }

        dialogue.text = currentDialogue;
    }
}
