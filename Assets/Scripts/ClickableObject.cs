using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private ClickableData data;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isShaking;
    private float shakeTimer; 

    private bool useClickSprite;
    private Vector3 startPosition;
    private int currentHealth;

    private bool isCollected = false;
    private float timeUntilRemove = 5.0f;
    private Vector3 moveVelocity = Vector3.zero;

    private void Start()
    {
        isShaking = false;
        currentHealth = data.ClickHealth;
        startPosition = transform.position;

        spriteRenderer.sprite = data.DefaultSprite;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(useClickSprite)
            {
                useClickSprite = false;
                spriteRenderer.sprite = data.DefaultSprite;
            }
        }

        if(isShaking)
        {
            Vector3 shakePosition = startPosition;
            Vector3 shakeDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f);
            shakeDirection = shakeDirection.normalized;
            shakePosition += shakeDirection * (data.shakeStrength * Random.Range(0.25f, 1.0f));

            transform.position = shakePosition;

            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0.0f)
            {
                isShaking = false;
                transform.position = startPosition; 
            }
        }

        if(isCollected)
        {
            transform.position += moveVelocity * Time.deltaTime;
            moveVelocity += (new Vector3(0.0f, -1.0f, 0.0f) * data.collectGravityDag) * Time.deltaTime;

            Vector3 currentRot = transform.localEulerAngles;
            currentRot.z += moveVelocity.magnitude * data.collectRotationSpeed * Time.deltaTime; // rotate faster if we move faster 
            transform.localEulerAngles = currentRot;

            timeUntilRemove -= Time.deltaTime;
            if(timeUntilRemove < 0.0f) 
            {
                Destroy(gameObject); 
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("Click Strength: " + GameManager.Instance.GetClickStrength());
        Debug.Log("Required Click Strength: " + data.requiredClickStrength);

        if (GameManager.Instance.GetClickStrength() < data.requiredClickStrength)
        {
            return;
        }

        currentHealth--;
        if (currentHealth <= 0)
        {
            CollectLogic();
            return;
        }

        // VFX & SFX //
        if (data.clickSFXs.Count > 0)
        {
            // Only play audio if we actually have SFXs in our data
            AudioManager.Instance.PlaySFXFromList(data.clickSFXs, data.ClickSFXVolume);
        }

        if(data.UseClickSprite)
        {
            if(data.ClickedSprite != null)
            {
                useClickSprite = true;
                spriteRenderer.sprite = data.ClickedSprite;
            }
            else
            {
                Debug.LogError("Trying to use 'ClickSprite' without having a ClickSprite assigned in the data.");
            }
        }

        if(data.ShakeOnClick)
        {
            isShaking = true;
            shakeTimer = data.shakeDuration;
        }
    }

    void CollectLogic()
    {
        AudioManager.Instance.PlaySFXFromList(data.collectSFXs, data.CollectSFXVolume); // Send message to ResourceManager 
        ResourceManager.Instance.AddResource(data.Type);

        isCollected = true;

        if(data.hasDialogue)
        {
            DialogueSystem.Instance.StartDialogue(data.characterEmote);
        }

        // Setup the move direction to launch our object into
        moveVelocity = new Vector3(0.0f, 1.0f, 0.0f);
        moveVelocity.x = Random.Range(-0.6f, 0.6f);
        moveVelocity = moveVelocity.normalized * data.collectInitialStrength; 
    }
}
