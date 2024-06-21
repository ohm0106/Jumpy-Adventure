using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueObj;
    [SerializeField]
    TMP_Text dialogueText;
    [SerializeField]
    TMP_Text nameText;
    [SerializeField]
    Button skipBtn;

    bool isDialogue;
   
    private Queue<Conversation> conversationQ;

    private void Start()
    {
        conversationQ = new Queue<Conversation>();
        skipBtn.onClick.AddListener(DisplayNextSentence);
        EndDialogue();
    }

    public void StartDialogue(Conversation[] conversations)
    {
        if (isDialogue)
            return;
        isDialogue = true;

        dialogueObj.SetActive(true);

        conversationQ.Clear();

        foreach(var conversation in conversations)
        {
            conversationQ.Enqueue(conversation);
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (conversationQ.Count == 0)
        {
            EndDialogue();
            return;
        }

        Conversation conversation = conversationQ.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(conversation));
    }

    IEnumerator TypeSentence(Conversation conversation)
    {
        dialogueText.text = "";
        nameText.text = conversation.name;
        foreach (char letter in conversation.talk.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        nameText.text = "";
        dialogueText.text = "";
        dialogueObj.SetActive(false);
        isDialogue = false;
    }

   public bool GetIsDialogue()
    {
        return isDialogue;
    }
}
