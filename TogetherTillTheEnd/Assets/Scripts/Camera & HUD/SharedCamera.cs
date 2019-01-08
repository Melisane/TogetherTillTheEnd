using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedCamera : MonoBehaviour
{

    private GameObject warrior;
    private GameObject mage;

    //private Vector2 minimumSize;
    private Camera cam;

    [SerializeField]
    private float zoomSpeed = 1.5f;

    [SerializeField]
    public float boundariesOffset = 2.0f;

    private float operativeOffset;

    public bool dezoomed = false;

    // The small orthographic size is the one set directly before starting the game.
    private float smallOrthographicSize;

    public float Yoffset;

    //To make smooth changes on Y
    public float changeY = 0.0f;
    public bool returnHome = false;
    float panTime = 0.0f;
    Vector3 changeYVector;
    float camStartY;

    [SerializeField]
    private float bigOrthographicSize = 17f;

    [SerializeField]
    private float triggerDistancePlayers = 18f;

    [SerializeField]
    public bool followInY;

    public bool inSpecialState = false;

    // Use this for initialization
    void Start()
    {
        warrior = FindObjectOfType<Warrior>().gameObject;
        mage = FindObjectOfType<Mage>().gameObject;
        cam = GetComponent<Camera>();

        smallOrthographicSize = cam.orthographicSize;

        operativeOffset = boundariesOffset - 0.5f;
        if (operativeOffset < 0)
            operativeOffset = 0f;

        // Get position of players
        Vector3 warriorPos = warrior.transform.position;
        Vector3 magePos = mage.transform.position;
        transform.position = new Vector3((warriorPos.x + magePos.x) * 0.5f, (warriorPos.y + magePos.y) * 0.5f + Yoffset, transform.position.z);

        camStartY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        SetCameraPositionAndSize();
    }

    private void SetCameraPositionAndSize()
    {
        // Get position of players
        Vector3 warriorPos = warrior.transform.position;
        Vector3 magePos = mage.transform.position;

        SmoothPan();

        float y;
        if (followInY)
            y = (warriorPos.y + magePos.y) * 0.5f + Yoffset;
        else
            y = transform.position.y;

        // Set the camera position as the average between both of those positions
        transform.position = new Vector3((warriorPos.x + magePos.x) * 0.5f, y, transform.position.z);

        if (!inSpecialState)
        {
            if (!dezoomed && (PlayerReachedRightBoundary(warriorPos.x) || PlayerReachedRightBoundary(magePos.x)))
            {
                dezoomed = true;
            }
            else if (AtTriggerDistancePlayers(warriorPos.x, magePos.x))
            {
                dezoomed = false;
            }
        }

        if (dezoomed)
            DezoomCamera();
        else
            ZoomCamera();
    }

    public bool PlayerReachedRightBoundary(float playerXPos)
    {
        return GetCameraRightBoundary() - 0.01f <= playerXPos;
    }

    public float GetCameraRightBoundary()
    {
        return transform.position.x + (cam.orthographicSize * cam.aspect) - operativeOffset;
    }

    public bool PlayerReachedLeftBoundary(float playerXPos)
    {
        return GetCameraLeftBoundary() + 0.01f >= playerXPos;
    }

    public float GetCameraLeftBoundary()
    {
        return transform.position.x - (cam.orthographicSize * cam.aspect) + operativeOffset;
    }

    public bool AtTriggerDistancePlayers(float warriorXPos, float mageXPos)
    {
        return Mathf.Abs(warriorXPos - mageXPos) <= triggerDistancePlayers;

    }

    public void DezoomCamera()
    {
        if (cam.orthographicSize >= bigOrthographicSize)
            return;
        else
            cam.orthographicSize += Time.deltaTime * zoomSpeed;
    }

    public void ZoomCamera()
    {
        if (cam.orthographicSize <= smallOrthographicSize)
            return;
        else
            cam.orthographicSize -= Time.deltaTime * zoomSpeed;
    }

    private void SmoothPan()
    {
        panTime -= Time.deltaTime;
        if (changeY != 0)
        {
            panTime = Mathf.Abs(changeY);
            changeYVector = new Vector3(transform.position.x, camStartY + changeY, transform.position.z);
            changeY = 0;
        }

        if(returnHome)
        {
            panTime = 2.0f;
            changeYVector = new Vector3(transform.position.x, camStartY, transform.position.z);
        }

        if (panTime > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, changeYVector, Time.deltaTime * 2);
            returnHome = false;
        }
            
    }

    
}
