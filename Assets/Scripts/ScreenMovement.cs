using UnityEngine;
using System;
using System.Collections;

public class ScreenMovement : MonoBehaviour
{
    [Header("References")]
    public GameObject camera;

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
            newCameraPosX = camera.transform.position.x;

            if (mousePosition.x >= camera.transform.position.x + screenResolution.x - edgeRangeForMovement && camera.transform.position.x <= XBoundaryRight)
            {
                newCameraPosX = camera.transform.position.x + 1 * multiplier;
            }
            else if (mousePosition.x <= camera.transform.position.x - screenResolution.x + edgeRangeForMovement && camera.transform.position.x >= XBoundaryLeft)
            {
                newCameraPosX = camera.transform.position.x - 1 * multiplier;
            }

            camera.transform.position = new Vector3(newCameraPosX, camera.transform.position.y, camera.transform.position.z);
        }

        void CalculateCameraPosY(Vector3 mousePosition, float multiplier)
        {
            newCameraPosY = camera.transform.position.y;

            if (mousePosition.y >= camera.transform.position.y + screenResolution.y - edgeRangeForMovement && camera.transform.position.y <= YBoundaryUp)
            {
                newCameraPosY = camera.transform.position.y + 1 * multiplier;
            }
            else if (mousePosition.y <= camera.transform.position.y - screenResolution.y + edgeRangeForMovement && camera.transform.position.y >= YBoundaryDown)
            {
                newCameraPosY = camera.transform.position.y - 1 * multiplier;
            }

            camera.transform.position = new Vector3(camera.transform.position.x, newCameraPosY, camera.transform.position.z);
        }
    
}
