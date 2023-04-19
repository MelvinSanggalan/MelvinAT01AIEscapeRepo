using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    //Define delegate types and events here

    public Node CurrentNode { get; private set; }
    public Node TargetNode { get; private set; }

    [SerializeField] private float speed = 4;
    private bool moving = false;
    private Vector3 currentDir;

    //ui nav buttons
    private GameObject northButton;
    private GameObject southButton;
    private GameObject leftButton;
    private GameObject rightButton;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Node node in GameManager.Instance.Nodes)
        {
            if(node.Parents.Length > 2 && node.Children.Length == 0)
            {
                CurrentNode = node;
                break;
            }
        }

        //subscribe to navButtonClickEvent
        NavButton.navButtonClickEvent += MouseMove;

        northButton = GameObject.FindGameObjectWithTag("UpButton");
        southButton = GameObject.FindGameObjectWithTag("DownButton");
        leftButton = GameObject.FindGameObjectWithTag("LeftButton");
        rightButton = GameObject.FindGameObjectWithTag("RightButton");

    }

    // Update is called once per frame
    void Update()
    {
        if (moving == false)
        {
            //Implement inputs and event-callbacks here

            InputRaycast();


        }
        else
        {
            if (Vector3.Distance(transform.position, TargetNode.transform.position) > 0.25f)
            {
                transform.Translate(currentDir * speed * Time.deltaTime);
            }
            else
            {
                moving = false;
                CurrentNode = TargetNode;
            }
        }
    }

    //Implement mouse interaction method here

    //mine
    private Node playerMouseNextNode;

    public void MouseMove(GameObject newMouseNode)
    {
        //Debug.Log(newMouseNode.name + "was hit.");

        //check if newmousenode has node component
        if (newMouseNode.GetComponent<Node>())
        {
            //set newmousenode's node as playermousenextnode and move to it
            playerMouseNextNode = newMouseNode.GetComponent<Node>();
            MoveToNode(playerMouseNextNode);
        }


    }
    //mine


    //mine

    //implement raycast method here

    private Node playerNextNode;

    public void InputRaycast()
    {
        RaycastHit raycastHitResults;

        //forwards
        if(Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") > 0)
        {
            //use raycast to see if there is a node in front of the player
            if (Physics.Raycast(transform.position, transform.forward, out raycastHitResults, 10))
            {
                //give raycastmove the node that was found
                RaycastMove(raycastHitResults.collider.gameObject);
                //flash green
                StartCoroutine(uiColorFlashGreen(northButton.gameObject));
            }
            else
            {
                //flash red
                StartCoroutine(uiColorFlashRed(northButton.gameObject));
            }
        }

        //backwards
        if(Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0)
        {
            //use raycast to see if there is a node behind the player
            if (Physics.Raycast(transform.position, -transform.forward, out raycastHitResults, 10))
            {
                RaycastMove(raycastHitResults.collider.gameObject);
                StartCoroutine(uiColorFlashGreen(southButton.gameObject));
            }
            else
            {
                StartCoroutine(uiColorFlashRed(southButton.gameObject));
            }
        }

        //left
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            //use raycast to see if there is a node to the left of the player
            if (Physics.Raycast(transform.position, -transform.right, out raycastHitResults, 10))
            {
                RaycastMove(raycastHitResults.collider.gameObject);
                StartCoroutine(uiColorFlashGreen(leftButton.gameObject));
            }
            else
            {
                StartCoroutine(uiColorFlashRed(leftButton.gameObject));
            }
        }

        //right
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            //use raycast to see if there is a node to the right of the player
            if (Physics.Raycast(transform.position, transform.right, out raycastHitResults, 10))
            {
                RaycastMove(raycastHitResults.collider.gameObject);
                StartCoroutine(uiColorFlashGreen(rightButton.gameObject));
            }
            else
            {
                StartCoroutine(uiColorFlashRed(rightButton.gameObject));
            }
        }

    }


    //method to move the player to the node
    public void RaycastMove(GameObject raycastHitResults)
    {
        Debug.Log(raycastHitResults.name + " was hit.");

        if (raycastHitResults.GetComponent<Node>())
        {
            playerNextNode = raycastHitResults.GetComponent<Node>();
            MoveToNode(playerNextNode);
        }

    }

    //mine


    /// <summary>
    /// Sets the players target node and current directon to the specified node.
    /// </summary>
    /// <param name="node"></param>
    public void MoveToNode(Node node)
    {
        if (moving == false && node.gameObject != null)
        {
            TargetNode = node;
            currentDir = TargetNode.transform.position - GameManager.Instance.Player.transform.position;
            currentDir = currentDir.normalized;
            moving = true;
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
