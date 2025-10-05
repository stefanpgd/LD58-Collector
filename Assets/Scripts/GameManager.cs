using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Todo:
/// - Make it so that when Dialogue is running, the mouse clicks don't work on Clickables
/// 
/// - Have a sprite which follows the mouse 
/// - Make sure focus is caught by the mouse
/// - With every item collected, grow the cursor sprite a bit
/// - Make a click-unclick state for the mouse sprite
/// </summary>

public enum GameState
{
    GameStart,
    CollectFourCats,
    SpawnCatInPond,
}

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This should manage how the player enters onboarding -> Stage 1..2..3 so forth
    /// </summary>
    public static GameManager Instance;
    private GameState currentGameState;

    [SerializeField] private float gameStartTimer = 3.0f;
    [SerializeField] public List<DialogueEvent> onboardingDialogue = new List<DialogueEvent>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentGameState = GameState.GameStart;
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.GameStart:
                gameStartTimer -= Time.deltaTime;
                if (gameStartTimer < 0)
                {
                    for(int i = 0; i < onboardingDialogue.Count; i++)
                    {
                        DialogueSystem.Instance.PushDialogueEvent(onboardingDialogue[i]);
                    }

                    DialogueSystem.Instance.StartDialogue();
                    currentGameState = GameState.CollectFourCats;
                }
                break;

            default:

                break;
        }
    }

    public int GetClickStrength()
    {
        return ResourceManager.Instance.TotalResourcesCollected();
    }

    public void ProcessGameState(GameState state)
    {
        currentGameState = state;

        switch (state)
        {
            case GameState.SpawnCatInPond:
                break;

            default: Debug.LogError("GameState not set up yet.."); break;
        }
    }
}
