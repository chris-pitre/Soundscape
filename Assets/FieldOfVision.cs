using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FieldOfVision : MonoBehaviour
{
    public float viewSize; //The radius of view circle
    [Range(0,360)]
    public float FOV; // The actual FOV 
    public LayerMask viewMask;  // Objects on layers in this will have shadows cast on them
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    public float rayPerDeg; //This is how many rays will be cast per degree try to leave it around 2-3 higher values lag alot
    public int edgeResolveIterations; //Higher values of this improve how well shadows are around corners 
    
    void Start(){
        viewMesh = new Mesh();
        viewMeshFilter.mesh = viewMesh;
    }
    
    void LateUpdate(){ // We call it as LateUpdate to let everything fully render before we draw the mesh
        DrawFieldOfView();
    }
    void DrawFieldOfView(){ //We use this method to create a mesh 
        int rayCount = Mathf.RoundToInt(FOV * rayPerDeg);
        float raySize = FOV / rayCount;
        List<Vector3> points = new List<Vector3>();
        rayCastInfo oldRayCast = new rayCastInfo();
        
        for(int x = 0; x <= rayCount; x++){
            float curAngle = transform.eulerAngles.z - FOV/2 + raySize * x;
            rayCastInfo theRayCast = castRays(curAngle);
           if(x >0){ 
                if (oldRayCast.hit != theRayCast.hit){// If the status of the ray is diffrent (one hit an object the other did not) we loop between that edge to find the actual edge to smooth the shadow while not casting like 10000 rays
                    EdgeInfo edge = FindEdge(oldRayCast, theRayCast);
                        if(edge.pointA != Vector3.zero){
                            points.Add (edge.pointA);
                            }
                        if(edge.pointB != Vector3.zero){
                            points.Add (edge.pointB);
                            }
                            
                    }
                }
           

           points.Add(theRayCast.point);
           oldRayCast = theRayCast;
        }

        int verCount = points.Count + 1;
        Vector3[] verts = new Vector3[verCount];
        int[] triangles = new int[(verCount-2) * 3];

        verts[0] = new Vector3(0,0,0);
        for (int x = 0; x < verCount -1; x++){
            verts[x +1] = transform.InverseTransformPoint(points[x]);
            if(x < verCount-2){
            triangles[x*3] = 0;
            triangles[x*3 +1] = x+1;
            triangles[x*3 +2] = x+2;
        }
        }
        viewMesh.Clear();
        viewMesh.vertices = verts;
        int[] tempTri = new int[(verCount-2) * 3];
        for(int x = 0; x < triangles.Length; x++){
            tempTri[triangles.Length-1-x] = triangles[x];
        }
        
        viewMesh.triangles = tempTri;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(rayCastInfo min, rayCastInfo max){ // We use this to smooth the edges around objects
        float minAng = min.angle;
        float maxAng = max.angle;
        Vector3 minPoint = new Vector3(0, 0 ,0);
        Vector3 maxPoint = new Vector3(0, 0 ,0);

        for(int x = 0; x < edgeResolveIterations; x++){
            float angle = (minAng + maxAng)/2;
            rayCastInfo newRayCast = castRays(angle);

            
            if(newRayCast.hit == min.hit){
                minAng = angle;
                minPoint = newRayCast.point;   
            }else{
                maxAng = angle;
                maxPoint = newRayCast.point;  
            }
        }
        return new EdgeInfo(minPoint, maxPoint);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal){ // We use this method to find the Vector3 angle when given an angle in degrees. The bool should be false if the angle is local rotation
        if(!angleIsGlobal){
            angleInDegrees += transform.eulerAngles.z;
        }
     
        return  new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad) , 0);
    }
    rayCastInfo castRays(float theAngle){ // We use this so we dont have to type out the raycast each time
        Vector3 direction = DirFromAngle (theAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, viewSize, viewMask)){
            return new rayCastInfo(true, hit.point, hit.distance, theAngle);
        }else{
            return new rayCastInfo(false, transform.position + direction * viewSize, viewSize, theAngle);
        }
    }

    public struct rayCastInfo{ //This is a data structure to hold the information our castRays function produces
            public bool hit;
            public Vector3 point;
            public float dst;
            public float angle;

            public rayCastInfo(bool _hit, Vector3 _point, float _dst, float _angle){
                hit = _hit;
                point = _point;
                dst = _dst;
                angle = _angle;
            }
    }

    public struct EdgeInfo { //This is a data structure to hold the information our FindEdge function produces
        public Vector3 pointA;
        public Vector3 pointB;
    
        public EdgeInfo(Vector3 _pointA, Vector3 _pointB){
            pointA = _pointA;
            pointB = _pointB;
        }
    }

}
