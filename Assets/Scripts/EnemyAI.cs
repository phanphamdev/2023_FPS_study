using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public Transform otherTargetPoint;
    public Transform myTargetPoint;
    public List<Transform> waypoints;
    // Start is called before the first frame update
     void Awake()
    {
        myTargetPoint = gameObject.transform.Find("TargetPoint").transform;
        otherTargetPoint = target.transform.Find("TargetPoint").transform;
    }
    void Start()
    {
        ClearWayPoint();
        CreateWaypoint(myTargetPoint.position,"wpStart");
        //DeBug.Log("bat dau chay");
    }
    void FindTarget()
    {
        if(otherTargetPoint = null)
        {
            return;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateWaypoint(Vector3 pos,string name_)
    {
        GameObject newWP = new GameObject();
        newWP.name = name_;
        newWP.transform.position = pos;
        waypoints.Add(newWP.transform);
    }
    void ClearWayPoint()
    {
        foreach(Transform trs in waypoints)
        {
            if(transform != null)
            {
                Destroy(transform.gameObject);
                Debug.Log("clear in loop");
            }
        }
        waypoints.Clear();
        Debug.Log("clear in wayclear");
    }
}
