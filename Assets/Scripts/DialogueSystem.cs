using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using System;

public enum CharacterEmote
{
    Smile,
    Suspicious,
    Flabbergasted,
    EmoteCount // use this to easily keep track of the amount of emotes
}

[Serializable]
public struct DialogueEvent
{
    [TextArea] public string Dialogue;
    public CharacterEmote EmoteToUse;
    public bool BlockInput;

    public int requiredAmountOfType;
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image characterImage;
    [SerializeField] private float openingTransitionSpeed = 2.0f;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private Sprite[] myEmotes = new Sprite[(int)CharacterEmote.EmoteCount];

    public bool DialogueHasMouseFocus { get; private set; } 

    private bool hasDialogue = false;
    private bool inOpeningAnimation = false;

    private float startY = -160.0f; // offscreen
    private float targetY = 18.0f;
    private float openingTimer = 0.0f;

    public static DialogueSystem Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        characterImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (hasDialogue)
        {
            OpenDialoge();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CloseDialogue();

                // Process Dialogue
                // if no more dialogue, finish dialogue
                // if done finishing dialogue, 
            }
        }
    }

    public void StartDialogue(DialogueEvent dialogueEvent)
    {
        hasDialogue = true;
        inOpeningAnimation = true;
        openingTimer = 0.0f;

        dialogueText.text = dialogueEvent.Dialogue;
        DialogueHasMouseFocus = dialogueEvent.BlockInput;

        characterImage.sprite = myEmotes[(int)dialogueEvent.EmoteToUse];
        characterImage.gameObject.SetActive(true);
    }

    public void OpenDialoge()
    {
        if (inOpeningAnimation)
        {
            float currentY = Mathf.SmoothStep(startY, targetY, openingTimer);

            Vector3 currentChatPosition = characterImage.rectTransform.anchoredPosition;
            currentChatPosition.y = currentY;
            characterImage.rectTransform.anchoredPosition = currentChatPosition;

            openingTimer += openingTransitionSpeed * Time.deltaTime;

            // Done With Opening or Skipped 
            if (openingTimer > 1.0f || Input.GetKeyDown(KeyCode.Space))
            {
                currentChatPosition.y = targetY;
                characterImage.rectTransform.anchoredPosition = currentChatPosition;

                inOpeningAnimation = false;
            }
        }
    }

    private void FinishDialogue()
    {
        
    }

    private void CloseDialogue()
    {
        hasDialogue = false;
        DialogueHasMouseFocus = false;

        characterImage.gameObject.SetActive(false);
    }

    private float EaseIn(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 +1.0f;

        return 1.0f + c3 * Mathf.Pow(x - 1.0f, 3.0f) + c1 * Mathf.Pow(x - 1.0f, 2.0f);
    }
}
