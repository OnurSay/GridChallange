using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScaleScript : MonoBehaviour
{

    // I tried to make the grid fit the screen perfectly but cannot find it for 3D objects. If i use canvas, will be easy one.
    Vector2 screenSizes;
    float depth;
    float width;
    float height;
    Vector3 lowerLeftPoint, upperRightPoint, upperLeftPoint, lowerRightPoint;
    float xPixelDistanceFirst, yPixelDistanceFirst;
    float xPixelDistance, yPixelDistance;
    void Start()
    {

        screenSizes.x = Screen.width;
        screenSizes.y = Screen.height;
        depth = gameObject.transform.lossyScale.z;
        width = gameObject.transform.lossyScale.x;
        height = gameObject.transform.lossyScale.y;

        lowerLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - width /2, gameObject.transform.position.y - height / 2, gameObject.transform.position.z - depth / 2));
        upperRightPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + width / 2, gameObject.transform.position.y + height / 2, gameObject.transform.position.z - depth / 2));
        upperLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - width / 2, gameObject.transform.position.y + height / 2, gameObject.transform.position.z - depth / 2));
        lowerRightPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + width / 2, gameObject.transform.position.y - height / 2, gameObject.transform.position.z - depth / 2));

        xPixelDistanceFirst = Mathf.Abs(lowerLeftPoint.x - upperRightPoint.x);
        yPixelDistanceFirst = Mathf.Abs(lowerLeftPoint.y - upperRightPoint.y);



    }

    // Update is called once per frame
    void Update()
    {
        if (screenSizes.x != Screen.width && screenSizes.y != Screen.height)
        {
            depth = gameObject.transform.lossyScale.z;
            width = gameObject.transform.lossyScale.x;
            height = gameObject.transform.lossyScale.y;

            lowerLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - width / 2, gameObject.transform.position.y - height / 2, gameObject.transform.position.z - depth / 2));
            upperRightPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + width / 2, gameObject.transform.position.y + height / 2, gameObject.transform.position.z - depth / 2));
            upperLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x - width / 2, gameObject.transform.position.y + height / 2, gameObject.transform.position.z - depth / 2));
            lowerRightPoint = Camera.main.WorldToScreenPoint(new Vector3(gameObject.transform.position.x + width / 2, gameObject.transform.position.y - height / 2, gameObject.transform.position.z - depth / 2));

            xPixelDistance = Mathf.Abs(lowerLeftPoint.x - upperRightPoint.x);
            yPixelDistance = Mathf.Abs(lowerLeftPoint.y - upperRightPoint.y);

            Debug.Log("xpix first =  " + xPixelDistanceFirst + " ypix First = " + yPixelDistanceFirst);
            Debug.Log("xpix =  " + xPixelDistance + " ypix = " + yPixelDistance);

            this.gameObject.transform.localScale = new Vector3(transform.localScale.x * xPixelDistance / xPixelDistanceFirst , this.gameObject.transform.localScale.y, transform.localScale.z * yPixelDistance/yPixelDistanceFirst );

            screenSizes.x = Screen.width;
            screenSizes.y = Screen.height;
            xPixelDistanceFirst = xPixelDistance;
            yPixelDistanceFirst = yPixelDistance;
        }
    }
}
