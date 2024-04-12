using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public InteractableObject currentInteractable;
    private DialogueManager dialogueManager;

    public string objectTag = "Interactable";
    public GameObject buttonPrompt;

    // Start is called before the first frame update
    void Start()
    {
        buttonPrompt.SetActive(false);
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInteractable != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentInteractable.Activate();
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
       if (collision.CompareTag(objectTag) == true)
       {
            buttonPrompt.SetActive(!dialogueManager.isActive);
            currentInteractable = collision.GetComponent<InteractableObject>();
       }     
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        buttonPrompt.SetActive(false);
        currentInteractable = null;
    }
}
