using UnityEngine;
using UnityEngine.EventSystems;

public class CursorLogic : MonoBehaviour
{
    [Header("References")]
    public SpriteRenderer cursorSpriteObject;
    public Animator cursorAnim;
    public Sprite[] cursorSprites;

    // follow mouse

    // swap sprite on click

    // grow with x number of clicks

    void Awake()
    {
        // hide default cursor
        Cursor.visible = false;
        cursorSpriteObject.sprite = cursorSprites[0];

    }

    void Update()
    {
        // calc mouseposition in world coordinates
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);

        transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Click();
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
}
