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
    Button skipBtn;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        skipBtn.onClick.AddListener(DisplayNextSentence);
    }

    public void StartDialogue(string[] dialogueSentence)
    {
        dialogueObj.SetActive(true);

        sentences.Clear();

        foreach(string sentence in dialogueSentence)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueText.text = "";
        dialogueObj.SetActive(false);
    }
}
