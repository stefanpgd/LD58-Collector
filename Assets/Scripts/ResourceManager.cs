using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    // Add int for each type... //
    [SerializeField] private int catsCollected = 0;
    [SerializeField] private int catsWithFishCollected = 0;
    [SerializeField] private int totalCollected = 0;

    public static ResourceManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void AddResource(ClickableType type)
    {
        switch(type)
        {
            case ClickableType.Cat: catsCollected++; break;
            case ClickableType.CatWithFish: catsWithFishCollected++; break;
            default: Debug.LogError("TYPE NOT SETUP!!!"); break;
        }

        totalCollected++;
    }

    public int GetResource(ClickableType type) 
    {
        switch (type)
        {
            case ClickableType.Cat: return catsCollected;
            case ClickableType.CatWithFish: return catsWithFishCollected;
            default: Debug.LogError("TYPE NOT SETUP!!!"); return -1;
        }
    }

    public int TotalResourcesCollected()
    {
        return totalCollected;
    }    
}
