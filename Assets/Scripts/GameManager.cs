using UnityEngine;

/// <summary>
/// Todo:
/// - Make a DialogueManager that is able to Push a dialogue event
/// - Make it so that when Dialogue is running, the mouse clicks don't work on Clickables
/// 
/// - Have a sprite which follows the mouse 
/// - Make sure focus is caught by the mouse
/// - With every item collected, grow the cursor sprite a bit
/// - Make a click-unclick state for the mouse sprite
/// </summary>

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This should manage how the player enters onboarding -> Stage 1..2..3 so forth
    /// </summary>
   
    public static GameManager Instance;

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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
