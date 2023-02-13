using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstSearch : MonoBehaviour
{
    //list variable for stack

    //listvariable for path

    //list of all nodes in the graph

    //variable for the current node

    //varaible for starting node

    //variable for destination node



    // Start is called before the first frame update
    void Start()
    {
        //find all available nodes and add them to the node list
    }

    // Update is called once per frame
    void Update()
    {
        //detect user input for triggering the pathfinding
    }

    public List<Node> FindPath(Node startNode, Node targtNode)
    {
        //set currentnode to startNode

        //add currentnode to stack

        //set local variable 'found' to to false

        //initiate while loop to continue so long as 'found' is false
        //check if currentnode is target node
        //if isn't continue the loop
        //otherwise set 'found' to true and break the loop

        //for each neighbour of current node
        //check if it's on the stack
        //check if it's already searched
        //if neither is true,add neighbour to stack and set currentnode as parent

        //set currentnode to 'searched'
        //remove currentnode from stack

        //check if stack is empty
            //if yes: break loop and return null with error message as path doesn't exist
            //if no: set last node in stack as current node
            //return to start of loop

        //if 'found' is true (while loop)
        //add currentnode to path
            //check if currentnode has parent
            //if yes: set parent as currentnode and continue loop
            //otherwise return path value

        return null;


    }

}
