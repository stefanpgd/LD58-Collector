using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Cat,
    Environment,
    UI,
    // so forth //
}

[CreateAssetMenu(fileName = "ClickableData", menuName = "Scriptable Objects/ClickableData")]
public class ClickableData : ScriptableObject
{
    [Header("Default")]
    public ObjectType Type;
    public Sprite DefaultSprite;
    public Sprite ClickedSprite;
    public int ClickHealth = 3;

    [Header("Juice")]
    public bool UseClickSprite = false;
    public float ClickSpriteDuration = 0.1f;

    public bool ShakeOnClick = false;
    public float shakeStrength = 0.1f;
    public float shakeDuration = 0.1f;

    [Header("Audio")]
    public List<AudioClip> hitSFXs = new List<AudioClip>();
    public List<AudioClip> collectSFXs = new List<AudioClip>();
}
