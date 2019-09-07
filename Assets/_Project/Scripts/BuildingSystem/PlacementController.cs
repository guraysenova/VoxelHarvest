using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementController : MonoBehaviour {

    [SerializeField]
    GameObject placeableObjectPrefab = null; // TO DO : make it so it will pick the object the player is holding.

    [SerializeField]
    GameObject currentPlaceableObject = null;

    [SerializeField]
    GameObject objectPlacerGO = null;

    [SerializeField]
    KeyCode newObjectHotkey = KeyCode.A;

    private float mouseWheelRotation;

    [SerializeField]
    private float rotationSpeed = 10f;

    private bool followCursor = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleNewObjectHotkey();

        if (Input.GetKeyUp(KeyCode.F4) && followCursor == false)
        {
            Cursor.lockState = CursorLockMode.None;
            followCursor = true;
        }
        else if (Input.GetKeyUp(KeyCode.F4) && followCursor == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            followCursor = false;
        }

        if (currentPlaceableObject != null)
        {
            MoveObjectToMousePos();
            RotateWithMouseWheel();
            ReleasePlacementIfClicked();
        }

    }

    private void ReleasePlacementIfClicked()
    {
        if (Input.GetMouseButtonUp(0))
        {
            currentPlaceableObject.layer = 0;
            currentPlaceableObject = null;
        }
    }

    private void RotateWithMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * rotationSpeed);
    }

    private void MoveObjectToMousePos()
    {
        if(followCursor)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                currentPlaceableObject.transform.position = hitInfo.point;
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }
        else
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(objectPlacerGO.transform.position, objectPlacerGO.transform.forward, out hitInfo))
            {
                currentPlaceableObject.transform.position = hitInfo.point;
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
        }
    }

    void HandleNewObjectHotkey()
    {
        if (Input.GetKeyDown(newObjectHotkey))
        {
            if(currentPlaceableObject == null)
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
            }
            else
            {
                Destroy(currentPlaceableObject);
            }
        }
    }
}
