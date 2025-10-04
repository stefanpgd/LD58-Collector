using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private ClickableData data;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isShaking;
    private float shakeTimer; 

    private bool useClickSprite;
    private float clickSpriteTimer;

    private Vector3 startPosition;
    private int currentHealth;

    private void Start()
    {
        isShaking = false;
        currentHealth = data.ClickHealth;
        startPosition = transform.position;

        spriteRenderer.sprite = data.DefaultSprite;
    }

    private void OnMouseDown()
    {
        currentHealth--;

        // Click SFX //
        AudioManager.Instance.PlaySFXFromList(data.hitSFXs);
        
        if(currentHealth <= 0)
        {
            // Collect //
            // Send message to ResourceManager 
            Destroy(gameObject);
        }
    }
}
