using UnityEngine;
using System.Collections.Generic;

public enum CharacterEmote
{
    Smile,
    Suspicious,
    EmoteCount // use this to easily keep track of the amount of emotes
}

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image characterImage;
    [SerializeField] private float openingTransitionSpeed = 2.0f;

    [SerializeField] private Sprite[] myEmotes = new Sprite[(int)CharacterEmote.EmoteCount];

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

    public void StartDialogue(CharacterEmote emoteToUse)
    {
        hasDialogue = true;
        inOpeningAnimation = true;
        openingTimer = 0.0f;

        characterImage.sprite = myEmotes[(int)emoteToUse];
        characterImage.gameObject.SetActive(true);
    }

    public void OpenDialoge()
    {
        if (inOpeningAnimation)
        {
            float currentY = Mathf.SmoothStep(startY, targetY, openingTimer);
            Debug.Log("Opening Timer: " + openingTimer + " & Lerp Result: " + currentY);

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

        characterImage.gameObject.SetActive(false);
    }

    private float EaseIn(float x)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 +1.0f;

        return 1.0f + c3 * Mathf.Pow(x - 1.0f, 3.0f) + c1 * Mathf.Pow(x - 1.0f, 2.0f);
    }
}
