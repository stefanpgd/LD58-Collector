using UnityEngine;
using System;
using System.Collections;

public class ScreenMovement : MonoBehaviour
{
    [Header("References")]
    public GameObject cam;
    [SerializeField] public Animator upArrowAnim;
    [SerializeField] public Animator downArrowAnim;
    [SerializeField] public Animator leftArrowAnim;
    [SerializeField] public Animator rightArrowAnim;

    [Header("Config")]
    public Vector2 screenResolution;
    public float cameraSpeed = 2;
    public int edgeRangeForMovement = 2;
    public int edgeBoundaryRange = 2;

    [Header("Camera Boundaries")]
    public float XBoundaryLeft = -9.5f;
    public float XBoundaryRight = 7.5f;
    public float YBoundaryUp = 3.8f;
    public float YBoundaryDown = -5.8f;

    float newCameraPosX;
    float newCameraPosY;

    void Awake()
    {
        screenResolution = new Vector2(22, 12);    
    }

    void Update()
        {

            // get the correct mouseposition                        
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);    
            float multiplier = cameraSpeed * Time.deltaTime;

            
            CalculateCameraPosX(mousePosition, multiplier);
            CalculateCameraPosY(mousePosition, multiplier);
        

        } 

    void CalculateCameraPosX(Vector3 mousePosition, float multiplier)
        {
            newCameraPosX = cam.transform.position.x;

            if (mousePosition.x >= cam.transform.position.x + screenResolution.x - edgeRangeForMovement && cam.transform.position.x <= XBoundaryRight)
            {
                newCameraPosX = cam.transform.position.x + 1 * multiplier;
                rightArrowAnim.SetBool("IsMovingRight", true);
            }
            else if (mousePosition.x <= cam.transform.position.x - screenResolution.x + edgeRangeForMovement && cam.transform.position.x >= XBoundaryLeft)
            {
                newCameraPosX = cam.transform.position.x - 1 * multiplier;
                leftArrowAnim.SetBool("IsMovingLeft", true);
            }
            else
            {
                rightArrowAnim.SetBool("IsMovingRight", false);
                leftArrowAnim.SetBool("IsMovingLeft", false);
            }

            cam.transform.position = new Vector3(newCameraPosX, cam.transform.position.y, cam.transform.position.z);
        }

        void CalculateCameraPosY(Vector3 mousePosition, float multiplier)
        {
            newCameraPosY = cam.transform.position.y;

            if (mousePosition.y >= cam.transform.position.y + screenResolution.y - edgeRangeForMovement && cam.transform.position.y <= YBoundaryUp)
            {
                newCameraPosY = cam.transform.position.y + 1 * multiplier;
                upArrowAnim.SetBool("IsMovingUp", true);
            }
            else if (mousePosition.y <= cam.transform.position.y - screenResolution.y + edgeRangeForMovement && cam.transform.position.y >= YBoundaryDown)
            {
                newCameraPosY = cam.transform.position.y - 1 * multiplier;
                downArrowAnim.SetBool("IsMovingDown", true);
            }
            else
            {
                upArrowAnim.SetBool("IsMovingUp", false);
                downArrowAnim.SetBool("IsMovingDown", false);
            }

            cam.transform.position = new Vector3(cam.transform.position.x, newCameraPosY, cam.transform.position.z);
        }
    
}
