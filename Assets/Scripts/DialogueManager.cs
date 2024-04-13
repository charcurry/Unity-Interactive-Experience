using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueUI;
    public TMP_Text dialogueText;
    public GameObject player;
    public Animator animator;

    public LevelManager levelManager;

    public InteractableObject objectToWinWith;

    public InteractableObject currentInteractableObject;

    private bool isTyping = false;
    private bool cancelTyping = false;

    public bool isActive;

    public float typeSpeed;

    private Queue<string> dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = new Queue<string>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void StartDialogue(string[] sentences, InteractableObject currentObject)
    {
        currentInteractableObject = currentObject;
        dialogue.Clear();
        dialogueUI.SetActive(true);
        isActive = true;
        SuspendPlayerControl();

        foreach (string sentence in sentences) 
        {
            dialogue.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (!isTyping)
        {
            if (dialogue.Count == 0)
            {
                EndDialogue();
                return;
            }
            string currentLine = dialogue.Dequeue();

            StartCoroutine(TextScroll(currentLine));

        }
        else if (isTyping && !cancelTyping)
        {
            cancelTyping = true;
        }
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        dialogueText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            dialogueText.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        dialogueText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    void SuspendPlayerControl()
    {
        player.GetComponent<CharacterController2D>().enabled = false;
        player.GetComponent<Interaction>().enabled = false;
        animator.enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void ResumePlayerControl()
    {
        player.GetComponent<CharacterController2D>().enabled = true;
        player.GetComponent<Interaction>().enabled = true;
        animator.enabled = true;
    }

    public void EndDialogue()
    {
        if (currentInteractableObject == objectToWinWith && currentInteractableObject.parameter && currentInteractableObject.winOnTalk) 
        {
            levelManager.LoadScene("GameWin");
            ResumePlayerControl();
            isActive = false;
            dialogueUI.SetActive(false);
            FindObjectOfType<GameManager>().gameState = GameManager.GameState.GameWin;
        }
        else
        {
            ResumePlayerControl();
            isActive = false;
            dialogueUI.SetActive(false);
            FindObjectOfType<GameManager>().gameState = GameManager.GameState.Gameplay;
        }
    }
}
