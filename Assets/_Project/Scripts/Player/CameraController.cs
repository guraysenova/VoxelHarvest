using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform pivot;
    public Transform pivotParent;
    public Transform head;

    public Vector3 offSet;

    public bool useOffSetValues;
    public bool shouldRotatePlayerHead = false;
    public bool isInMenu = false;


    public float rotateSpeed;

    public Transform lookAtObject;
    public Transform lookAtPivot;

    private void Start()
    {
        if (!useOffSetValues)
        {
            offSet = target.position - transform.position;
        }

        pivot.transform.position = pivotParent.transform.position;
        pivot.transform.parent = pivotParent.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        ZoomInOutWithMouseWheel();
    }

    private void ZoomInOutWithMouseWheel()
    {
        if (pivotParent.transform.localPosition.y >= 20f && pivotParent.transform.localPosition.y <= 100f)
        {
            pivotParent.transform.Translate(pivotParent.transform.up/3f * -Input.mouseScrollDelta.y);
        }
        pivotParent.transform.localPosition = new Vector3(0f,Mathf.Clamp(pivotParent.transform.localPosition.y, 20f, 100f) , 0f);
    }

    private void LateUpdate()
    {
        if (isInMenu)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            shouldRotatePlayerHead = !shouldRotatePlayerHead;
        }


        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivotParent.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0);

        if (shouldRotatePlayerHead)
        {
            lookAtPivot.Rotate(0, horizontal, 0);
        }

        if (pivot.rotation.eulerAngles.x > 30f && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(30f, 0f, 0f);
        }

        if(pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 300f)
        {
            pivot.rotation = Quaternion.Euler(300f, 0f, 0f);
        }


        if (shouldRotatePlayerHead)
        {
            float yAngle = pivotParent.localRotation.eulerAngles.y;

            if (Mathf.Abs(yAngle) < 0.01f)
            {
                yAngle = 0f;
            }
            pivotParent.localRotation = Quaternion.Euler(pivotParent.localRotation.eulerAngles.x, MyMathf.ClampAngle(yAngle, -60f , 60f), pivotParent.localRotation.eulerAngles.x);
            lookAtPivot.localRotation = Quaternion.Euler(pivotParent.localRotation.eulerAngles.x, MyMathf.ClampAngle(yAngle, -60f, 60f), pivotParent.localRotation.eulerAngles.x);
        }

        if (shouldRotatePlayerHead)
        {
            head.LookAt(lookAtObject);
        }

        float desiredYAngle = pivotParent.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = pivotParent.position - (rotation * offSet);

        if(transform.position.y < pivotParent.position.y)
        {
            transform.position = new Vector3(transform.position.x, pivotParent.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(pivotParent);
    }
}
