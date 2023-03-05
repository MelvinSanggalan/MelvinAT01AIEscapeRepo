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


    //my stuff
    private GameObject thePlayer;
    //my stuff

    // Start is called before the first frame update
    void Start()
    {
        InitializeAgent();

        //my stuff

        thePlayer = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(thePlayer.name);
        //my stuff

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

                //my stuff
                ChaseThePlayer(thePlayer.GetComponent<Player>().CurrentNode);
                //my stuff
            }
            else
            {
                Debug.LogWarning($"{name} - No current node");
            }

            Debug.DrawRay(transform.position, currentDir, Color.cyan);
        }


        //my stuff
        //Debug.Log(thePlayer.GetComponent<Player>().CurrentNode);
        //my stuff

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
    //my stuff
    public void ChaseThePlayer(Node node)
    {
        if(currentNode != null)
        {
            node = thePlayer.GetComponent<Player>().CurrentNode;
            currentDir = thePlayer.GetComponent<Player>().CurrentNode.transform.position - transform.position;
            currentDir = currentDir.normalized;
        }
    }
    //my stuff

}
