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
    LookingAway,
    Sad,
    Annoyed,
    EmoteCount // use this to easily keep track of the amount of emotes
}

[Serializable]
public struct DialogueEvent
{
    [TextArea] public string Dialogue;
    public CharacterEmote EmoteToUse;
    public bool BlockInput;

    public int requiredAmountOfType;

    public bool UpdateGameState;
    public GameState NewGameState;

    [Header("Effects")]
    public bool characterShakes;
    public float shakeDuration;
    public float shakeStrength;
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private RectTransform dialogueParent;
    [SerializeField] private UnityEngine.UI.Image characterImage;
    [SerializeField] private float openingTransitionSpeed = 2.0f;
    [SerializeField] private float closeTransitionSpeed = 2.0f;
    [SerializeField] private float nextDialogueTransitionSpeed = 3.0f;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Sprite[] myEmotes = new Sprite[(int)CharacterEmote.EmoteCount];

    private Queue<DialogueEvent> myDialogueEvents = new Queue<DialogueEvent>();

    public bool DialogueHasMouseFocus { get; private set; } 

    private bool hasDialogue = false;
    
    // "Animations" //
    private bool inOpeningAnimation = false;
    private float openingTimer = 0.0f;
    private bool inNextDialogue = false;
    private float nextDialogueTimer = 0.0f;
    private bool bobDown = true;
    private bool inClosingAnimation = false;
    private float closingTimer = 0.0f;

    // Shaking //
    private bool isShaking = false;
    private Vector3 characterStartAnchoredPosition;
    private Vector3 dialogueStartAnchoredPosition;

    private float shakeDuration = 0.0f;
    private float shakeStrength = 0.0f;

    private float closeX = -340.0f;
    private float startY = -160.0f; // offscreen
    private float targetY = 18.0f;
    private float bobY = 8.0f;

    public static DialogueSystem Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        dialogueStartAnchoredPosition = dialogueParent.anchoredPosition;
        characterStartAnchoredPosition = characterImage.rectTransform.anchoredPosition;
        dialogueParent.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (GameManager.Instance.currentGameState == GameState.GameStart)
        {
            DialogueHasMouseFocus = true;
        }
    }

    private void Update()
    {
        if (hasDialogue)
        {
            OpenDialoge();
            ProcessShake();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(myDialogueEvents.Count > 0)
                {
                    StartDialogue(true);
                }
                else
                {
                    FinishDialogue();
                }
            }
        }
        else
        {
            CloseDialogue();
        }
    }

    public void PushDialogueEvent(DialogueEvent dialogueEvent)
    {
        myDialogueEvents.Enqueue(dialogueEvent);
    }

    public void StartDialogue(bool isContinuingDialogue = false)
    {
        if(myDialogueEvents.Count < 0)
        {
            Debug.LogError("Trying to start Dialoue without any stored events!");
            return;
        }

        DialogueEvent currenEvent = myDialogueEvents.Dequeue();
        hasDialogue = true;

        // If we've multiple dialogues after each other, instead of the opening
        // animation we should go for a simple bob to switch
        if(isContinuingDialogue)
        {
            inNextDialogue = true;
            bobDown = true;
            nextDialogueTimer = 0.0f;
        }
        else
        {
            inOpeningAnimation = true;
            openingTimer = 0.0f;
        }


        if(currenEvent.characterShakes)
        {
            isShaking = true;
            shakeStrength = currenEvent.shakeStrength;
            shakeDuration = currenEvent.shakeDuration;
        }

        dialogueText.text = currenEvent.Dialogue;
        DialogueHasMouseFocus = currenEvent.BlockInput;

        characterImage.sprite = myEmotes[(int)currenEvent.EmoteToUse];
        dialogueParent.gameObject.SetActive(true);
    }

    public void OpenDialoge()
    {
        if (inOpeningAnimation)
        {
            float currentY = Mathf.SmoothStep(startY, targetY, openingTimer);

            Vector3 currentChatPosition = dialogueStartAnchoredPosition;
            currentChatPosition.y = currentY;
            dialogueParent.anchoredPosition = currentChatPosition;

            openingTimer += openingTransitionSpeed * Time.deltaTime;

            // Done With Opening or Skipped 
            if (openingTimer > 1.0f || Input.GetKeyDown(KeyCode.Space))
            {
                dialogueParent.anchoredPosition = dialogueStartAnchoredPosition;
                inOpeningAnimation = false;
            }
        }

        if(inNextDialogue)
        {
            float currentY = Mathf.SmoothStep(targetY, bobY, nextDialogueTimer);
            
            Vector3 currentChatPosition = dialogueStartAnchoredPosition;
            currentChatPosition.y = currentY;
            dialogueParent.anchoredPosition = currentChatPosition;

            if (bobDown)
            {
                nextDialogueTimer += nextDialogueTransitionSpeed * Time.deltaTime;

                if (nextDialogueTimer >= 1.0f)
                {
                    bobDown = false;
                }
            }
            else
            {
                nextDialogueTimer -= nextDialogueTransitionSpeed * Time.deltaTime;

                if (nextDialogueTimer <= 0.0f)
                {
                    dialogueParent.anchoredPosition = dialogueStartAnchoredPosition;
                    inNextDialogue = false;
                }
            }

            // Skip.. 
            if(Input.GetKeyDown(KeyCode.Space))
            {
                dialogueParent.anchoredPosition = dialogueStartAnchoredPosition;
                inNextDialogue = false;
            }
        }
    }

    private void FinishDialogue()
    {
        hasDialogue = false;
        DialogueHasMouseFocus = false;

        inClosingAnimation = true;
        closingTimer = 0.0f;
    }

    private void CloseDialogue()
    {
       if(inClosingAnimation)
       {
            float startX = dialogueStartAnchoredPosition.x;
            float currentX = Mathf.SmoothStep(startX, closeX, closingTimer);

            Vector3 currentChatPosition = dialogueStartAnchoredPosition;
            currentChatPosition.x = currentX;
            dialogueParent.anchoredPosition = currentChatPosition;

            closingTimer += closeTransitionSpeed * Time.deltaTime;

            if (closingTimer > 1.0f)
            {
                dialogueParent.gameObject.SetActive(false);
                dialogueParent.anchoredPosition = dialogueStartAnchoredPosition;

                inClosingAnimation = false;
            }
        }
    }
    private void ProcessShake()
    {
        if(isShaking && !inOpeningAnimation)
        {
            Vector3 shakePosition = characterStartAnchoredPosition;
            Vector3 shakeDirection = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), 0.0f);
            shakeDirection = shakeDirection.normalized;
            shakePosition += shakeDirection * (shakeStrength * UnityEngine.Random.Range(0.25f, 1.0f));

            characterImage.rectTransform.anchoredPosition = shakePosition;

            shakeDuration -= Time.deltaTime;
            if (shakeDuration <= 0.0f)
            {
                isShaking = false;
                characterImage.rectTransform.anchoredPosition = characterStartAnchoredPosition;
            }
        }
    }
}
