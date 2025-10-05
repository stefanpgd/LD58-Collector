using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorLogic : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer cursorSpriteObject;
    public Animator cursorAnim;
    public Sprite[] cursorSprites;
    public float delay = 0.3f;
    public float cursorSizeMultiplier = 2f;
    int currentClickStrength;
    int savedClickStrength;

    // follow mouse

    // swap sprite on click

    // grow with x number of clicks

    void Awake()
    {
        // hide default cursor
        Cursor.visible = false;
        cursorSpriteObject.sprite = cursorSprites[0];
        currentClickStrength = 0;

    }

    void Update()
    {
        // Check whether dialogue is playing with Cursor focus, if so hide sprite & ignore logic
        if(DialogueSystem.Instance.DialogueHasMouseFocus)
        {
            cursorSpriteObject.enabled = false;
            return;
        }
        else
        {
            cursorSpriteObject.enabled = true;
        }

        // calc mouseposition in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);

        transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Click();

            currentClickStrength = GameManager.Instance.GetClickStrength();

            if (currentClickStrength > savedClickStrength)
            {
                Debug.Log(currentClickStrength + " / " + savedClickStrength);
                StartCoroutine(IncreaseCursorSize(cursorSizeMultiplier, delay));
                
                savedClickStrength ++;
                
            }

            else if (currentClickStrength == savedClickStrength)
                return;

            else if (currentClickStrength < savedClickStrength)
                Debug.LogWarning("Cursor is using a larger clickStrength value than the manager");
        
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ResetCursor();
        }
    }

    void Click()
    {
        cursorSpriteObject.sprite = cursorSprites[1];
        cursorAnim.SetBool("isMouseDown", true);
    }

    void ResetCursor()
    {
        cursorSpriteObject.sprite = cursorSprites[0];
        cursorAnim.SetBool("isMouseDown", false);
    }

    private IEnumerator IncreaseCursorSize(float cursorSizeMultiplier, float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.localScale = new Vector3(transform.localScale.x * cursorSizeMultiplier, transform.localScale.y * cursorSizeMultiplier, transform.localScale.z);

    }
}
