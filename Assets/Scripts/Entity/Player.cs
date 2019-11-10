using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    private GameObject _text;

    private void Start()
    {
        _text = GameObject.Find("Description");
    }

    protected override Vector3 GetNextLocation()
    {
        Vector3 currentTransform = GetComponent<Transform>().localPosition;
        if (direction == Direction.west)
        {
            if (currentTransform.x == 4.05f)
            {
                return new Vector3(1.7f, currentTransform.y, currentTransform.z);
            }
            else if (currentTransform.x == 1.7f)
            {
                return new Vector3(0, currentTransform.y, currentTransform.z);
            }
            else if (currentTransform.x == 0f)
            {
                return new Vector3(-1.7f, currentTransform.y, currentTransform.z);
            }
            else if (currentTransform.x == -1.7f)
            {
                return new Vector3(-4.05f, currentTransform.y, currentTransform.z);
            }
        }
        else if (direction == Direction.north)
        {
            if (currentTransform.z == -4f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, -1.46f);
            }
            else if (currentTransform.z == -1.46f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, 0f);
            }
            else if (currentTransform.z == 0f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, 1.46f);
            }
            else if (currentTransform.z == 1.46f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, 4f);
            }
        }
        else if (direction == Direction.east)
        {
            if (currentTransform.x == -4.05f)
            {
                return new Vector3(-1.7f, currentTransform.y, currentTransform.z);
            }
            else if (currentTransform.x == -1.7f)
            {
                return new Vector3(0, currentTransform.y, currentTransform.z);
            }
            else if (currentTransform.x == 0f)
            {
                return new Vector3(1.7f, currentTransform.y, currentTransform.z);
            }
            else if (currentTransform.x == 1.7f)
            {
                return new Vector3(4.05f, currentTransform.y, currentTransform.z);
            }
        }
        else if (direction == Direction.south)
        {
            if (currentTransform.z == 4f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, 1.46f);
            }
            else if (currentTransform.z == 1.46f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, 0f);
            }
            else if (currentTransform.z == 0f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, -1.46f);
            }
            else if (currentTransform.z == -1.46f)
            {
                return new Vector3(currentTransform.x, currentTransform.y, -4f);
            }
        }
        return new Vector3();
    }
}
