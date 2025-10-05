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
    // Onboarding,
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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public int GetClickStrength()
    {
        return ResourceManager.Instance.TotalResourcesCollected();
    }

    public void ProcessGameState(GameState state)
    {
        currentGameState = state;

        switch(state) 
        {
            case GameState.SpawnCatInPond:
                    break;

            default: Debug.LogError("GameState not set up yet.."); break;
        }
    }
}
