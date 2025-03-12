using System;
using UnityEditor.UIElements;
using UnityEngine;

/*
* The GameObject also needs a collider otherwise OnMouseUpAsButton() can not be detected.
*/
public class Draggable: MonoBehaviour
{
    //public Application application;
    public Transform trans;
    public GameObject canvas;
 
    private bool isDragging = true;

    public void StartDragging()
    {
        isDragging = true;
    }

    public void Update()
    {
        if (isDragging)
        {
            trans.position = GetMousePosition();
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
        }
    }

    private void OnMouseUpAsButton()
    {
        isDragging = !isDragging;

        if (!isDragging)
        {
            // Stopped dragging. Add any logic here that you need for this scenario.
            //application.UpdateObject2D();
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionInWorld.z = 0;
        return positionInWorld;
    }

}
