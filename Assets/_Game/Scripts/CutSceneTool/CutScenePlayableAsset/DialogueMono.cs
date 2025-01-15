using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueMono : PlayableAsset
{
    public string nameTag;
    [TextArea] public string dialogueText;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph);

        DialogueBehaviour dialogueBehaviour = playable.GetBehaviour();
        dialogueBehaviour.dialogue = nameTag + " | " + dialogueText;

        return playable;
    }
}
