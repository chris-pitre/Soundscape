using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public int x;
    public int y;
    public bool blocked;
    public int gCost;
    public int hCost;
    public int fCost;
    public Node cameFromNode;
    private Grid grid;
    public  Node(Grid grid, int x, int y, bool blocked = false){
        this.x = x;
        this.y = y;
        this.grid = grid;
        this.blocked = blocked;
    }

  
    public void CalculateFCost(){
        if (blocked){
            fCost = int.MaxValue;
        }else{
            fCost = gCost + hCost;
        }
        
    }
    public void setXY(int x, int y){
        this.x = x;
        this.y = y;
    }

   
    public override string ToString(){
        return x + "," + y;
    }

}

