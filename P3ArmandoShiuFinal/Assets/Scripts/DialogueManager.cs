using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour

{
    public GameObject dialogueBoxUI;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button agreeButton;

    private Queue<string> sentences;
    private bool isTyping = false;
    private string currentSentence;
    public float typingSpeed = 0.05f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sentences = new Queue<string>();
        dialogueBoxUI.SetActive(false);
        agreeButton.gameObject.SetActive(false);
    }
    void Update()
    {
        if (dialogueBoxUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = currentSentence;
                isTyping = false;
            }
            else if (!agreeButton.gameObject.activeSelf)
            {
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue(string npcName, string[] dialogueLines)
    {
        dialogueBoxUI.SetActive(true);
        nameText.text = npcName;
        sentences.Clear();
        agreeButton.gameObject.SetActive(false);
        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {


        if (sentences.Count == 0)
        {
            EndDialoguePrompt();


            return;
        }
        currentSentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(currentSentence));

        if (sentences.Count == 0 && !isTyping)
        {
            agreeButton.gameObject.SetActive(true);
            agreeButton.onClick.RemoveAllListeners();
            agreeButton.onClick.AddListener(AgreeToQuest);
        }
        else
        {
            agreeButton.gameObject.SetActive(false);
        }
        IEnumerator TypeSentence(string sentence)
        {
            isTyping = true;
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTyping = false;

        }

        if (sentences.Count == 0)
        {
            agreeButton.gameObject.SetActive(true);
            agreeButton.onClick.RemoveAllListeners();
            agreeButton.onClick.AddListener(AgreeToQuest);
        }
    }
      
    public void EndDialoguePrompt()
    {
    isTyping = false;
    agreeButton.gameObject.SetActive(true);
    agreeButton.onClick.RemoveAllListeners();
    agreeButton.onClick.AddListener(AgreeToQuest);
}

    public void EndDialogue()
    {
        dialogueBoxUI.SetActive(false);
    agreeButton.gameObject.SetActive(false);
    Debug.Log("Dialogue system closed.");
}
private void AgreeToQuest()
    {
        Debug.Log("Quest Accepted!");
        EndDialogue();
    }
    // Update is called once per frame
    
}

