using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    private PlayerController target;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;
    private float aspect;

    public Tilemap theMap;
    public float smoothSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance;

        SetCameraLimits();

        target.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }

    private void Update()
    {
        if (aspect != Camera.main.aspect)
        {
            //Debug.Log("transform.position.x :" + transform.position.x + "topRightLimit.x :" + topRightLimit.x + "bottomLeftLimit.x:" + bottomLeftLimit.x + "transform.position.y :" + transform.position.y + "topRightLimit.y :" + topRightLimit.y + "bottomLeftLimit.y :" + bottomLeftLimit.y);
            //Debug.Log("Camera.main.orthographicSize: " + Camera.main.orthographicSize + " Camera.main.aspect: " + Camera.main.aspect + " halfWidth : " + halfWidth + " Mathf.Abs(bottomLeftLimit.x) + topRightLimit.x : " + Mathf.Abs(bottomLeftLimit.x) + " " + topRightLimit.x);
            SetCameraLimits();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 smoothedPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            if(!target.isFreezed)
            {
                smoothedPosition = Vector3.Lerp(transform.position, smoothedPosition, smoothSpeed * Time.deltaTime);
            }
            
            transform.position = smoothedPosition;
            if (halfWidth * 2 < (Mathf.Abs(theMap.localBounds.min.x) + theMap.localBounds.max.x))
            {
                //keep the camera inside the bounds
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
            }
            else
            {
                //keep the camera inside the bounds
                transform.position = new Vector3(0, Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
            }
        } 
    }

    public void SetCameraLimits()
    {
        aspect = Camera.main.aspect;
        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * aspect;

        GameManager.instance.isMobile = aspect < 1;

        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);
        //Debug.Log("Aspect : " + aspect + " halfHeight : " + halfHeight + " halfWidth : " + halfWidth );
        //Debug.Log("transform.position.x :" + transform.position.x + "topRightLimit.x :" + topRightLimit.x + "bottomLeftLimit.x:" + bottomLeftLimit.x + "transform.position.y :" + transform.position.y + "topRightLimit.y :" + topRightLimit.y + "bottomLeftLimit.y :" + bottomLeftLimit.y);
        //Debug.Log("Camera.main.orthographicSize: " + Camera.main.orthographicSize + " Camera.main.aspect: " + Camera.main.aspect + " halfWidth : " + halfWidth + " Mathf.Abs(bottomLeftLimit.x) + topRightLimit.x : " + Mathf.Abs(bottomLeftLimit.x) + " " + topRightLimit.x);
    }
}