using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Movement speed modifier.")]
    [SerializeField] private float speed = 3;
    private Node currentNode;
    private Vector3 currentDir;
    private bool playerCaught = false;

    public delegate void GameEndDelegate();
    public event GameEndDelegate GameOverEvent = delegate { };

    //my variables
    //stack
    //public static List<Node> stack = new List<Node>();

    //private GameObject thePlayer;
    //my variables


    // Start is called before the first frame update
    void Start()
    {
        InitializeAgent();

        //my dfs stuff (test)
        /*
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        foreach (Node node in GameManager.Instance.Nodes)
        {
            if (node.Parents.Length > 2 && node.Children.Length == 0)
            {
                currentNode = node;
                break;
            }
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCaught == false)
        {
            if (currentNode != null)
            {
                //If within 0.25 units of the current node.
                if (Vector3.Distance(transform.position, currentNode.transform.position) > 0.25f)
                {
                    transform.Translate(currentDir * speed * Time.deltaTime);
                }
                //Implement path finding here
                //call dfs algorithm method here
                //DFSSearch(thePlayer.GetComponent<Player>().CurrentNode);
                
            }
            else
            {
                Debug.LogWarning($"{name} - No current node");
            }

            Debug.DrawRay(transform.position, currentDir, Color.cyan);
        }
    }

    //Called when a collider enters this object's trigger collider.
    //Player or enemy must have rigidbody for this to function correctly.
    private void OnTriggerEnter(Collider other)
    {
        if (playerCaught == false)
        {
            if (other.tag == "Player")
            {
                playerCaught = true;
                GameOverEvent.Invoke(); //invoke the game over event
            }
        }
    }

    /// <summary>
    /// Sets the current node to the first in the Game Managers node list.
    /// Sets the current movement direction to the direction of the current node.
    /// </summary>
    void InitializeAgent()
    {
        currentNode = GameManager.Instance.Nodes[0];
        currentDir = currentNode.transform.position - transform.position;
        currentDir = currentDir.normalized;
    }


    //Implement DFS algorithm method here (mine)
    /*
    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        startNode = currentNode;
        stack.Add(currentNode);
    
        playerCaught = false;

        while (playerCaught == false)
        {
            if(currentNode == targetNode)
            {
                playerCaught = true;
                break;
            }
        }

     

        return null;
    }
    */

    //my DFS algorithm method 2
    /*
    public void DFSSearch(Node node)
    {
        if(currentNode != null)
        {
            currentNode = thePlayer.GetComponent<Player>().CurrentNode;
            currentDir = node.transform.position = transform.position;
            currentDir = currentDir.normalized;
            //currentNode = node;
        }
    }
    */

}
