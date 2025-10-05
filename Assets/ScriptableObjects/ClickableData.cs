using System.Collections.Generic;
using UnityEngine;

public enum ClickableType
{
    Cat,
    CatWithFish,
    Barrels,
    Environment,
    UI,
    // so forth //
}

[CreateAssetMenu(fileName = "ClickableData", menuName = "Scriptable Objects/ClickableData")]
public class ClickableData : ScriptableObject
{
    [Header("Default")]
    public ClickableType Type;
    public Sprite DefaultSprite;
    public Sprite ClickedSprite;
    public int ClickHealth = 3;
    public int requiredClickStrength = 0;

    [Header("Dialogue")]
    [SerializeField] public List<DialogueEvent> DialogueEvents = new List<DialogueEvent>();

    [Header("Juice")]
    public bool UseClickSprite = false;
    public bool ShakeOnClick = false;
    public float shakeStrength = 0.1f;
    public float shakeDuration = 0.1f;

    public float collectInitialStrength = 17.0f;
    public float collectGravityDag = 17.5f;
    public float collectRotationSpeed = 90.0f;

    [Header("Audio")]
    [Range(0.0f, 1.0f)] public float ClickSFXVolume = 0.5f;
    [Range(0.0f, 1.0f)] public float CollectSFXVolume = 0.5f;
    public List<AudioClip> clickSFXs = new List<AudioClip>();
    public List<AudioClip> collectSFXs = new List<AudioClip>();
}
