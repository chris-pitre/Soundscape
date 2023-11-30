using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private Grid grid;
    private List<Node> openList;
    private List<Node> closedList;
    public Pathfinding (int width, int height, List<Vector2> blocked){
        float cellSize = 0.5f;
        grid = new Grid(width, height, 0.5f, new Vector3(-(width/2f), - (height/2f), 0f), blocked);
        
    }
    public List<Node> FindPath(int startX, int startY, int endX, int endY){
        
        Vector2 startNodeVec = grid.GetXY(new Vector3 (startX, startY, 0f));
        Vector2 endNodeVec = grid.GetXY(new Vector3 (endX, endY, 0f));
       // Debug.Log("This is the start Vac " + startNodeVec);
        //Debug.Log("This is the end Vac " + endNodeVec);
        Node startNode = grid.GetGridObject((int)startNodeVec.x, (int)startNodeVec.y);
        Node endNode = grid.GetGridObject((int)endNodeVec.x, (int)endNodeVec.y);
        openList = new List<Node> { startNode };
        closedList = new List<Node>();
        for (int x = 0; x < grid.GetWidth(); x++){
            for (int y = 0; y < grid.GetHeight(); y++){
                Node node = grid.GetGridObject(x, y);
                node.gCost = int.MaxValue;
                node.CalculateFCost();
                node.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CaculateDistance(startNode, endNode);
        startNode.CalculateFCost();


        while(openList.Count > 0){
            
            Node currentNode = GetLowestFCost(openList);
            if (currentNode == endNode){
              
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Node neighbourNode in GetNextDoor(currentNode)){
                if (closedList.Contains(neighbourNode)) continue; 
                int tentativeGCost = currentNode.gCost + CaculateDistance(currentNode, neighbourNode);
                
                if(tentativeGCost < neighbourNode.gCost){
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CaculateDistance(neighbourNode,endNode);
                    neighbourNode.CalculateFCost();


                    if(!openList.Contains(neighbourNode)){
                        openList.Add(neighbourNode);
                    }
                }
            }
        }
        //no more nodes
        return null;
    }
    private int CaculateDistance(Node a, Node b){
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
    private Node GetLowestFCost(List<Node> NodeList){
        Node lowestFCostNode = NodeList[0];
        for (int i = 1; i< NodeList.Count; i++){
            if(NodeList[i].fCost < lowestFCostNode.fCost){
                lowestFCostNode = NodeList[i];
            }
        
        }
        return lowestFCostNode;
    }
    private List<Node> CalculatePath(Node endNode){
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node currentNode = endNode;
        while(currentNode.cameFromNode != null){
            
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }
    private List<Node> GetNextDoor(Node currentNode){
        List<Node> neighbourList = new List<Node>();
        if(currentNode.x - 1 >= 0){
            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
            if (currentNode.y -1 >= 0) neighbourList.Add(GetNode(currentNode.x -1, currentNode.y -1));
            if (currentNode.y +1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x -1, currentNode.y +1));
        }
         if(currentNode.x + 1 < grid.GetWidth()){
            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
            if (currentNode.y -1 >= 0) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y -1));
            if (currentNode.y +1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y +1));
        }
            if (currentNode.y -1 >= 0) neighbourList.Add(GetNode(currentNode.x, currentNode.y -1));
            if (currentNode.y +1 < grid.GetHeight()) neighbourList.Add(GetNode(currentNode.x, currentNode.y +1));
            
            
            return neighbourList;
    }
    
     public Node GetNode(int x, int y){
        return grid.GetGridObject(x,y);

    }
    public Vector3 GetWorldNode(int x, int y){
        return grid.GetWorldPosition(x,y);

    }
}
