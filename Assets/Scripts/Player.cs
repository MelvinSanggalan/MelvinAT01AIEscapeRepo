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

        //mine
        NavButton.navButtonClickEvent += MouseMove;
        //mine
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

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //use raycast to see if there is a node in front of the player
            if (Physics.Raycast(transform.position, transform.forward, out raycastHitResults, 10))
            {
                //give raycastmove the node that was found
                RaycastMove(raycastHitResults.collider.gameObject);
            }
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //use raycast to see if there is a node behind the player
            if (Physics.Raycast(transform.position, -transform.forward, out raycastHitResults, 10))
            {
                RaycastMove(raycastHitResults.collider.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //use raycast to see if there is a node to the left of the player
            if (Physics.Raycast(transform.position, -transform.right, out raycastHitResults, 10))
            {
                RaycastMove(raycastHitResults.collider.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //use raycast to see if there is a node to the right of the player
            if (Physics.Raycast(transform.position, transform.right, out raycastHitResults, 10))
            {
                RaycastMove(raycastHitResults.collider.gameObject);
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
}
