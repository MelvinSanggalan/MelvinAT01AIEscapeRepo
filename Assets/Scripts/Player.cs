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

    //[SerializeField]EventSystem

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
    }

    // Update is called once per frame
    void Update()
    {
        if (moving == false)
        {
            //Implement inputs and event-callbacks here

            //test (20/03/2023, use input manager)
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                MoveToNode(CurrentNode.Parents[0]);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveToNode(CurrentNode.Children[0]);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveToNode(CurrentNode.Parents[1]);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveToNode(CurrentNode.Parents[2]);
            }


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



    /// <summary>
    /// Sets the players target node and current directon to the specified node.
    /// </summary>
    /// <param name="node"></param>
    public void MoveToNode(Node node)
    {
        if (moving == false)
        {
            TargetNode = node;
            currentDir = TargetNode.transform.position - transform.position;
            currentDir = currentDir.normalized;
            moving = true;
        }
    }
}
