using UnityEngine;

public class NearHover : MonoBehaviour
{
    [SerializeField] private ClickableObject ClickableObject;
    [SerializeField] private float upOffset;
    [SerializeField] private float mouseDistanceTillMove = 1.0f;
    [SerializeField] private float moveSpeed = 2.0f;

    private float moveTimer;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickableObject.isCollected)
        {
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = (mousePos - new Vector2(startPosition.x, startPosition.y)).magnitude;

        if(distance < mouseDistanceTillMove)
        {
            moveTimer += Time.deltaTime * moveSpeed;
        }
        else
        {
            moveTimer -= Time.deltaTime * moveSpeed;
        }
        moveTimer = Mathf.Clamp01(moveTimer);

        Vector3 targetPos = (transform.up * upOffset) + startPosition;
        Vector3 pos = Vector3.Lerp(startPosition, targetPos, moveTimer);
        transform.position = pos;
    }
}
