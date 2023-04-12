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
    public delegate void NavButtonClick(GameObject newMouseNode);
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

        //raycast to check if a node is there and to change the UI color
        RaycastHit raycastColorHitResults;


        foreach (RaycastResult result in results)
        {
            //if button is detected, check if its the up button
            if(result.gameObject.tag == "UpButton")
            {
                if (Physics.Raycast(GameManager.Instance.Player.transform.position, GameManager.Instance.Player.transform.forward, out raycastColorHitResults, 10))
                {
                    //flash ui color to green
                    StartCoroutine(uiColorFlashGreen(result.gameObject));

                    //event call with player's node direction
                    navButtonClickEvent(raycastColorHitResults.collider.gameObject);
                }
                else
                {
                    //if no node found then flash ui color to red
                    StartCoroutine(uiColorFlashRed(result.gameObject));
                }

            }

            //check if its the down button
            if (result.gameObject.tag == "DownButton")
            {
                if (Physics.Raycast(GameManager.Instance.Player.transform.position, -GameManager.Instance.Player.transform.forward, out raycastColorHitResults, 10))
                {
                    //flash ui color to green
                    StartCoroutine(uiColorFlashGreen(result.gameObject));

                    //event call with player's node direction
                    navButtonClickEvent(raycastColorHitResults.collider.gameObject);
                }
                else
                {
                    //if no node found then flash ui color to red
                    StartCoroutine(uiColorFlashRed(result.gameObject));
                }

            }

            //check if its the left button
            if (result.gameObject.tag == "LeftButton")
            {
                if (Physics.Raycast(GameManager.Instance.Player.transform.position, -GameManager.Instance.Player.transform.right, out raycastColorHitResults, 10))
                {
                    //flash ui color to green
                    StartCoroutine(uiColorFlashGreen(result.gameObject));

                    //event call with player's node direction
                    navButtonClickEvent(raycastColorHitResults.collider.gameObject);
                }
                else
                {
                    //if no node found then flash ui color to red
                    StartCoroutine(uiColorFlashRed(result.gameObject));
                }

            }

            //check if its the right button
            if (result.gameObject.tag == "RightButton")
            {
                if (Physics.Raycast(GameManager.Instance.Player.transform.position, GameManager.Instance.Player.transform.right, out raycastColorHitResults, 10))
                {
                    //flash ui color to green
                    StartCoroutine(uiColorFlashGreen(result.gameObject));

                    //event call with player's node direction
                    navButtonClickEvent(raycastColorHitResults.collider.gameObject);
                }
                else
                {
                    //if no node found then flash ui color to red
                    StartCoroutine(uiColorFlashRed(result.gameObject));
                }

            }
        }

    }

    //coroutines for UI color flash
    IEnumerator uiColorFlashGreen(GameObject uiGameObject)
    {
        uiGameObject.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(0.5f);
        uiGameObject.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator uiColorFlashRed(GameObject uiGameObject)
    {
        uiGameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        uiGameObject.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.5f);
    }

}
