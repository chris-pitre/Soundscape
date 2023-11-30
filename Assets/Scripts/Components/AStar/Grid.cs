using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid {

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private Node[,] gridArray;
    

    public Grid(int width, int height, float cellSize, Vector3 originPosition, List<Vector2> blocked) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        
        gridArray = new Node[width, height];
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                gridArray[x, y] = new Node(this, x, y);
                

        }}
        if(blocked != null){
            
        foreach(Vector2 blockedNode in blocked){
            Debug.Log(blockedNode+ " " + blockedNode.x +"  " + blockedNode.y);
            Vector2 temp = GetXY(new Vector3(blockedNode.x, blockedNode.y, 0f)); 
            
            GetGridObject((int)temp.x, (int)temp.y).blocked = true;
        }
        }else{
         
        }
        bool showDebug = true;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    if(!GetGridObject(x,y).blocked){
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.yellow, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.yellow, 100f);
                    }else{
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.red, 100f);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, 100f);
                    }
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.yellow, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.yellow, 100f);

            
        }
        
    }

    public int GetWidth() {
        return width;
    }

    public int GetHeight() {
        return height;
    }

    public float GetCellSize() {
        return cellSize;
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y, 0f)  + originPosition;
    }

    public Vector2 GetXY(Vector3 worldPosition) {
        Vector2 returnAble = new Vector2();
        returnAble.x = Mathf.FloorToInt((worldPosition - originPosition).x );
        returnAble.y = Mathf.FloorToInt((worldPosition - originPosition).y );
        return returnAble;
    }


   
    
    public Node GetGridObject(int x, int y){
        return gridArray[x,y];
    }
   
    
    
}


