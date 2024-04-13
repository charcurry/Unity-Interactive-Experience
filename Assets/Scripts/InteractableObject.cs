using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    //private int pickups = 0;

    public bool parameter;
    public InteractableObject parameterGameObject;
    public InteractableObject nextObject;
    public bool isPickedUp = false;

    public bool winOnTalk;

    public string scene;
    public bool active = false;

    //private GameObject scoreText;
    private GameObject characterText;
    public string text;

    public LevelManager levelManager;

    public string[] sentencesIncomplete;
    public string[] sentencesComplete;

    public enum ObjectType
    {
        Pickup,
        Info,
        Dialogue
    }

    public void Start()
    {
        //scoreText = GameObject.Find("Score Text");
        characterText = GameObject.Find("Character Text");
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void Update()
    {
        if (parameterGameObject != null)
        {
            if (this.type == ObjectType.Dialogue && parameterGameObject.isPickedUp)
            {
                parameter = true;
            }
        }
    }

    public ObjectType type;

    public void Activate()
    {
        if (type == ObjectType.Info)
        {
            Info();
        }
        if (type == ObjectType.Pickup)
        {
            Pickup();
        }
        if (type == ObjectType.Dialogue)
        {
            Dialogue();
        }
    }

    private void Info()
    {
        characterText.SetActive(true);
        characterText.GetComponent<TextMeshProUGUI>().text = text;
        StartCoroutine(DeleteText());
    }

    private void Pickup()
    {
        //pickups++;
        //scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + pickups.ToString();
        //Debug.Log("Picked Up Item!");
        isPickedUp = true;
        active = false;
        gameObject.SetActive(false);
    }

    private void Dialogue()
    {
        FindObjectOfType<GameManager>().gameState = GameManager.GameState.Dialogue;
        if (nextObject != null) 
        {
            if (parameter)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(sentencesComplete, this);
                nextObject.active = true;
            }
            else if (!parameter)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(sentencesIncomplete, this);
            }
        }
        if (parameter && winOnTalk)
        {
            UnityEngine.Debug.Log("startWinDialogue");
            FindObjectOfType<DialogueManager>().StartDialogue(sentencesComplete, this);

        }
        else if (!parameter && winOnTalk)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(sentencesIncomplete, this);
        }
    }

    IEnumerator DeleteText()
    {
        yield return new WaitForSeconds(2.5f);
        characterText.GetComponent<TextMeshProUGUI>().text = null;
        characterText.SetActive(false);
    }
}
