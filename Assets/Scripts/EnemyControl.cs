using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public float speed;
    public float DistanceAttack = 5f;
    public float DistanceRada = 20f;
    public Transform PlayerTargets;
    public Transform myEnemy;
    private float shotTime = 0.1f;
    private float timeNextShort = 0f;
    Rigidbody myEnemyRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myEnemyRigidbody = GetComponent<Rigidbody>();
        var _Player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_Player)
        {
            PlayerTargets = _Player;
            //  Debug.Log("sadasd");
        }
        else
        {
            // ngua nhien move
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(myEnemy.position, PlayerTargets.position) <= DistanceRada && Vector3.Distance(myEnemy.position, PlayerTargets.position) >= DistanceAttack)
        {
            speed = 5f;
            myEnemy.position = Vector3.MoveTowards(myEnemy.position, PlayerTargets.position, speed * Time.deltaTime);
            myEnemy.LookAt(PlayerTargets);
            //  Debug.Log("phat hien player");
        }
        else if (Vector3.Distance(myEnemy.position, PlayerTargets.position) < 5 && Vector3.Distance(myEnemy.position, PlayerTargets.position) > 0)
        {
            // Enemy tan cong Player
            speed = 0f;
            // Debug.Log("tan cong player");
            FireGunAttack();
            
        }
        else
        {
            // Enemy move ngau nhien tren dia hinh
            if (boolMoveRandom)
            {
                MoveRandom();
                //  Debug.Log("di chuyen ngau nhien");
            }
            else
            {
                StartCoroutine(setPosRandom(2f));
                // Debug.Log("di chuyen ngau nhien");
            }

        }
    }
    void FireGunAttack()
    {
        // Debug.Log("FireGun");

        if (Time.time - shotTime > timeNextShort)
        {
            timeNextShort = Time.time - Time.deltaTime; // tru di thoi gian vai giay de hoan thanh khung hinh
        }
        while (timeNextShort < Time.time)
        {
            CreatDan();
            timeNextShort += shotTime; //delay time
            // do delay cua dan
        }
    }
    RaycastHit hit; // xac dinh deim tac dong

    float khoangCachBan = 20;
    float lucBan;
    void FireOneShot()
    {
        Vector3 direc = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, direc, out hit, khoangCachBan))
        {
            CreatDan();
        }
        
    }

    public bool boolMoveRandom = false;
    Vector3 positRandom = Vector3.zero;
    float velocity_ = 0f;
    void MoveRandom()
    {

        myEnemy.position = Vector3.MoveTowards(myEnemy.position, positRandom, speed * Time.deltaTime);
        myEnemy.LookAt(positRandom);
        velocity_ = myEnemyRigidbody.velocity.magnitude;
        float tamX, tamZ;
        if (velocity_ > 0.7f)
        {
            if (myEnemy.position.x > 0)
            {
                tamX = 1f;
                // Debug.Log("1")
            }
            else
            {
                tamX = -1f;
            }
            if (myEnemy.position.z > 0)
            {
                tamZ = 1f;
            }
            else
            {
                tamZ = -1f;
                positRandom = new Vector3(myEnemy.position.x - tamX, myEnemy.position.y, myEnemy.position.z - tamZ);
                // Debug.Log("4")
            }
        }
    }
    IEnumerator setPosRandom(float t)
    {
        boolMoveRandom = true;
        yield return new WaitForSeconds(t);
        Debug.Log("setposRandom bool 1");
        positRandom = new Vector3(myEnemy.position.x + Random.Range(-10, 10), myEnemy.position.y, myEnemy.position.z + Random.Range(-10, 10));
        boolMoveRandom = false;
        yield return new WaitForSeconds(1f);
        Debug.Log("setposRandom bool 2" + " " + t);
        boolMoveRandom = true;
        Debug.Log("setposRandom bool 3");
    }
    public GameObject prefabsDan;
    public Transform positionInis;

    void CreatDan()
    {
        if (prefabsDan == null || positionInis == null)
            return;
        GameObject ob = Instantiate(prefabsDan, positionInis.position, positionInis.rotation) as GameObject;
        ob.transform.position = positionInis.position + myEnemy.transform.forward;
        Rigidbody rb = ob.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.velocity = myEnemy.transform.forward * 50f;
            // Debug.Log("creat dan");
        }

    }
}
