using System.Collections;
using UnityEngine;

public class NPCAction : ObjectAction
{
    [SerializeField]
    string[] dialogueSentence;

    public override IEnumerator CoAction()
    {
        yield return StartCoroutine(base.CoAction());
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogueSentence);

    }
}
