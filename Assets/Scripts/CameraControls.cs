using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public float cameraMoveSpeed;

    [Header("Coordinates man/max value")]
    [SerializeField]
    private float maxXCoord;
    [SerializeField]
    private float minXCoord;
    [SerializeField]
    private float maxYCoord;
    [SerializeField]
    private float minYCoord;

    private float cameraXSize;
    private float cameraYSize;

    private Vector3 newPos;
    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    public void SetSize(SO_GameSettings gameSettings) 
    {
        cameraXSize = gameObject.GetComponent<Camera>().orthographicSize;
        maxXCoord = gameSettings.xSize * gameSettings.tileWidth - cameraXSize + transform.position.x;
        minXCoord = -gameSettings.xSize * gameSettings.tileWidth + cameraXSize + transform.position.x;
        maxYCoord = gameSettings.ySize * gameSettings.tileHeight - cameraXSize + transform.position.y;
        minYCoord = -gameSettings.ySize * gameSettings.tileHeight + cameraXSize + transform.position.y;
    }

    private void Move()
    {
        float xInput = 0;
        float yInput = 0;

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            yInput = -1;
            Debug.Log("Down");
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            yInput = 1;
            Debug.Log("Up");
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            xInput = -1;
            Debug.Log("Left");
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            xInput = 1;
            Debug.Log("Right");
        }

        Vector3 oldPos = gameObject.transform.position;

        newPos = new Vector3(Mathf.Clamp(oldPos.x + xInput * cameraMoveSpeed * Time.deltaTime, minXCoord, maxXCoord), Mathf.Clamp(oldPos.y + yInput * cameraMoveSpeed * Time.deltaTime, minYCoord, maxYCoord), -10);
        gameObject.transform.position = newPos;
    }
}
