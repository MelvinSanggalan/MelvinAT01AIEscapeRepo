using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* Script created by Melvin Sanggalan
 * Last updated 22/03/2023
 * Script attached to a custom UI object (button) and controls the player's movement
 */ 

public class NavButton : MonoBehaviour
{

    //variable for eventsystem
    [SerializeField] private EventSystem eventSystem;

    //variable for graphic raycaster
    [SerializeField] private GraphicRaycaster gRaycaster;

    //float to symbolise the direction the player is going to (test)
    //[SerializeField] private float nodeDirection;

    //variable for pointer data
    private PointerEventData pData;

    //event for nav button click
    public delegate void NavButtonClick(float nodeDir);
    public static NavButtonClick navButtonClickEvent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            //mouse interect method
            MouseInteract();
        }
    }



    private void MouseInteract()
    {
        //perform a graphic raycast from the pointer position the UI
        pData = new PointerEventData(eventSystem);

        //assign the current position of the mouse
        pData.position = Input.mousePosition;

        //initialize a list of raycast results
        List<RaycastResult> results = new List<RaycastResult>();

        //perform a graphic raycast from the pointer position to the UI
        gRaycaster.Raycast(pData, results);

        foreach(RaycastResult result in results)
        {
            //if button is detected, check if its the up button
            if (result.gameObject.tag == "UpButton")
            {
                //event call with player's node direction
                navButtonClickEvent(1);
            }
        }

    }

}
