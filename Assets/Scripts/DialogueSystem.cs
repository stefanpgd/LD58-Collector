using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image characterImage;
    [SerializeField] private UnityEngine.UI.Image chatBox;

    private bool hasDialogue = false;

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
        chatBox.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (hasDialogue)
        {
            characterImage.gameObject.SetActive(true);
            chatBox.gameObject.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                characterImage.gameObject.SetActive(false);
                chatBox.gameObject.SetActive(false);

                hasDialogue = false;
            }
        }
    }

    public void StartDialogue()
    {
        hasDialogue = true;
    }
}
