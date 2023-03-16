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
    //private GameObject thePlayer;

    //my stuff

    // Start is called before the first frame update
    void Start()
    {
        InitializeAgent();

        //my stuff
        //thePlayer = GameObject.FindGameObjectWithTag("Player");
        //Debug.Log(thePlayer.name);

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
                DFSSearch();

                //my stuff
                //ChaseThePlayer(thePlayer.GetComponent<Player>().CurrentNode);
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
    //public void ChaseThePlayer(Node node)
    //{
    //    if(currentNode != null)
    //    {
    //        node = thePlayer.GetComponent<Player>().CurrentNode;
    //        currentDir = thePlayer.GetComponent<Player>().CurrentNode.transform.position - transform.position;
    //        currentDir = currentDir.normalized;
    //    }
    // }
    //my stuff









    //access the nodes on gamemanager
    //add GameManager.Instance.Nodes[0] to a list of unsearched nodes (root node)
    //check if root node is the same as GameManager.Instance.Player.CurrentNode
    //if it is the same:return that as the new destination for this enemy

    //add the children of the node being searched to the list of unsearched nodes
    //remove the node being searched from list of unsearched nodes
    //assign the node at the 'top' (last position of the unsearched list) as the node being searched
    //go back to line 120



    //VARIABLE FOR 'NODE CURRENTLY BEING SEARCHED'
    //BOOLEAN FOR 'TARGET FOUND'
    //LIST OF TYPE 'NODE' STORING 'UNSEARCHED NODES' (This is your stack)


    //SET 'TARGET FOUND' TO FALSE

    //ASSIGN GAMEMANAGER.INSTANCE.NODES[0] TO YOUR 'UNSEARCHED NODES' LIST

    //LOOP STARTS HERE
    //WHILE 'TARGET FOUND' IS FALSE, CONTINUE THE LOOP

    //1. TAKE LAST ITEM FROM 'UNSEARCHED NODES' LIST AND ASSIGN IT TO 'NODE CURRENTLY BEING SEARCHED' //note: if list isnt empty then

    //2. CHECK IF 'NODE CURRENTLY BEING SEARCHED' IS THE SAME AS *EITHER*
        //THE TARGET NODE OF THE PLAYER (NODE THEY ARE HEADING TOWARDS)
        //THE CURRENT NODE OF THE PLAYER (THE LAST NODE THEY VISITED)
    //IF THIS IS TRUE ('NODE CURRENTLY BEING SEARCHED' IS THE ONE WE WANT'):
        //ASSIGN 'NODE CURRENTLY BEING SEARCHED' AS 'CURRENTNODE'
        //BREAK THE LOOP AND FINISH THIS METHOD
    //IF IT ISN'T TRUE CONTINUE

    //3. USE A FOR LOOP TO ADD EACH CHILD OF 'NODE CURRENTLY BEING SEARCHED' TO UNSEARCHED NODES LIST

    //4. REMOVE 'NODE CURRENTLY BEING SEARCHED' FROM UNSEARCHED NODES LIST

    //5. RETURN TO START OF LOOP


    private Node nodebeingSearched;
    private bool targetFound;
    public List<Node> unsearchedNodes = new List<Node>();


    void DFSSearch()
    {
        targetFound = false;
        unsearchedNodes.Add(GameManager.Instance.Nodes[0]);

        while(targetFound == false)
        {
            if(unsearchedNodes.Count > 0)
            {
                nodebeingSearched = unsearchedNodes[unsearchedNodes.Count - 1];

                if(nodebeingSearched == GameManager.Instance.Player.CurrentNode)
                {
                    currentNode = nodebeingSearched;
                    Debug.Log(currentNode); //test
                    break;
                }
                //else
                //{
                //    continue;
                //}

            }

            foreach (Node nodeChildren in nodebeingSearched.Children)
            {
                unsearchedNodes.Add(nodeChildren);

            }

            unsearchedNodes.Remove(nodebeingSearched);
            return;



        }


    }







    /*
    void Search()
    {


        foreach(Node node in GameManager.Instance.Nodes)
        {
            unsearchedNodes.Add(GameManager.Instance.Nodes[0]);

            if(GameManager.Instance.Nodes[0] == GameManager.Instance.Player.CurrentNode)
            {

            }
        }


        
    }
    */






}
