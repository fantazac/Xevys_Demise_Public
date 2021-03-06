﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Source: https://www.assetstore.unity3d.com/en/#!/content/55578
public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private float _smoothTime = 7f;

    [Header("Target Elements")]
    private Vector3 followtarget;

    [SerializeField]
    private Vector3 focusPosition;

    private GameObject focusObject;

    // Area boundary elements
    [Header("Area Elements")]
    [SerializeField]
    private int _currentArea = 0;
    [SerializeField]
    private List<GameObject> listAreaNodes = new List<GameObject>();

    private CinematicManager _cinematicManager;
    private bool _isInCinematic = false;
    private Vector3 _positionBeforeCinematic;

    public delegate void OnAreaChangedHandler(int roomId);
    public event OnAreaChangedHandler OnAreaChanged;

    public int CurrentArea { get { return _currentArea; } }

    private void Start()
    {
        _cinematicManager = StaticObjects.GetCinematic().GetComponent<CinematicManager>();
        focusObject = StaticObjects.GetPlayer();

        if (focusObject != null)
        {
            FocusObject = focusObject;
        }

        if (listAreaNodes.Count == 0)
        {
            Debug.LogWarning(gameObject.name.ToString() + " (CameraManager): No Area boundaries are assigned. The camera will move freely to the set targets");
        }
    }

    public void SetCameraOnPlayer(Vector3 position, int cameraDimensionId)
    {
        _currentArea = cameraDimensionId;
        transform.position = position;
    }

    private void Update()
    {
        if (_cinematicManager.MoveCameraToCinematic)
        {
            if (!_isInCinematic)
            {
                _positionBeforeCinematic = transform.position;
                _isInCinematic = true;
            }
            
            transform.position = _cinematicManager.CinematicPosition;
        }
        else if (_isInCinematic)
        {
            transform.position = _positionBeforeCinematic;
            _isInCinematic = false;
        }
        else
        {
            // Declare Vector3 for the new position
            Vector3 newPosition;

            if (focusObject != null)
            {
                followtarget = focusObject.transform.position;
            }
            else
            {
                followtarget = focusPosition;
            }

            newPosition = followtarget;
            newPosition.z = transform.position.z;

            if (listAreaNodes.Count > 0)
            {
                // If the current room size is smaller than the camera, fix the camera in the center of the room only following the focusobject over the y-axis
                if (GetAreaRect(_currentArea).width < (Camera.main.orthographicSize * Camera.main.aspect) * 2)
                {
                    newPosition.x = listAreaNodes[_currentArea].transform.GetChild(0).position.x + GetAreaRect(_currentArea).width / 2;
                }
                else
                {
                    newPosition.x = Mathf.Clamp(followtarget.x,
                        listAreaNodes[_currentArea].transform.GetChild(0).position.x + (Camera.main.orthographicSize * Camera.main.aspect),
                        listAreaNodes[_currentArea].transform.GetChild(1).position.x - (Camera.main.orthographicSize * Camera.main.aspect));
                }

                // Same for rooms with a smaller height than the camera. Fix the camera in the center of the roomheight and follow focusobject over x-axis
                if (GetAreaRect(_currentArea).height < Camera.main.orthographicSize * 2)
                {
                    newPosition.y = listAreaNodes[_currentArea].transform.GetChild(0).position.y - GetAreaRect(_currentArea).height / 2;
                }
                else
                {
                    newPosition.y = Mathf.Clamp(followtarget.y,
                    listAreaNodes[_currentArea].transform.GetChild(1).position.y + Camera.main.orthographicSize,
                    listAreaNodes[_currentArea].transform.GetChild(0).position.y - Camera.main.orthographicSize);
                }

                // Check wether the player is outside the boundaries of the camera. If so trigger a transition, else move towards the current set target position
                if (!GetAreaRect(_currentArea).Contains(followtarget))
                {
                    SetNewArea();
                }

                // Adjust the camera's position to that of the newly determined position
                transform.position = Vector3.Lerp(transform.position, newPosition, _smoothTime * Time.deltaTime);
            }
        } 
    }

    // Method to check what area the player has entered and sets the CurrentArea to this new area
    private void SetNewArea()
    {
        int previousArea = _currentArea;

        foreach (GameObject n in listAreaNodes)
        {
            if (GetAreaRect(listAreaNodes.IndexOf(n)).Contains(followtarget))
            {
                previousArea = listAreaNodes.IndexOf(n);

                if (previousArea == _currentArea)
                {
                    return;
                }
                _currentArea = previousArea;
                if (gameObject.name == StaticObjects.GetMainObjects().MainCamera && OnAreaChanged != null)
                {
                    OnAreaChanged(listAreaNodes.IndexOf(n));
                }
            }
        }
    }

    // Changing the focusObject sets the focusPosition to zero
    public GameObject FocusObject
    {
        get { return focusObject; }
        set
        {
            focusObject = value;
            focusPosition = Vector3.zero;
        }
    }

    // Changing the focusPosition sets the focusObject to null
    public Vector3 FocusPosition
    {
        get { return focusPosition; }
        set
        {
            focusPosition = value;
            focusObject = null;
        }
    }

    // Returns a Rect form of the area given in the parameter _area
    private Rect GetAreaRect(int _area)
    {
        GameObject camDimensions = listAreaNodes[_area];
        Rect rect = new Rect(camDimensions.transform.GetChild(0).position.x, camDimensions.transform.GetChild(1).position.y,
                camDimensions.transform.GetChild(1).position.x - camDimensions.transform.GetChild(0).position.x, Mathf.Abs(camDimensions.transform.GetChild(1).position.y - camDimensions.transform.position.y));
        return rect;
    }

    // Returns a Rect form of the camera
    public Rect GetCameraRect()
    {
        Rect rect = new Rect(transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect), transform.position.y - Camera.main.orthographicSize,
            (Camera.main.orthographicSize * Camera.main.aspect) * 2, -Camera.main.orthographicSize * 2);
        return rect;
    }

    private void OnDrawGizmos()
    {
        if (listAreaNodes.Count == 0)
        {
            return;
        }

        // Draw the current selected area's bounding box
        foreach (GameObject camDimensions in listAreaNodes)
        {
            Transform cameraDimensionStart = camDimensions.transform.GetChild(0);
            Transform cameraDimensionEnd = camDimensions.transform.GetChild(1);

            Gizmos.color = Color.red;
            if (camDimensions == listAreaNodes[_currentArea])
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawLine(new Vector2(cameraDimensionStart.position.x, cameraDimensionStart.position.y), new Vector2(cameraDimensionEnd.position.x, cameraDimensionStart.position.y));
            Gizmos.DrawLine(new Vector2(cameraDimensionStart.position.x, cameraDimensionEnd.position.y), new Vector2(cameraDimensionEnd.position.x, cameraDimensionEnd.position.y));
            Gizmos.DrawLine(new Vector2(cameraDimensionStart.position.x, cameraDimensionStart.position.y), new Vector2(cameraDimensionStart.position.x, cameraDimensionEnd.position.y));
            Gizmos.DrawLine(new Vector2(cameraDimensionEnd.position.x, cameraDimensionStart.position.y), new Vector2(cameraDimensionEnd.position.x, cameraDimensionEnd.position.y));
        }
    }

    public void setCurrentArea(int currentArea)
    {
        _currentArea = currentArea;
    }

    public void setListNodes(List<GameObject> nodes)
    {
        listAreaNodes = nodes;
    }

    public List<GameObject> getListNodes()
    {
        return listAreaNodes;
    }
}
