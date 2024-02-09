using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 100f;
    private float jumpSpeed = 8f;
    private float lucHapDan = 20f;
    private Vector3 moveDirection = Vector3.zero;
    private bool grounded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        CharacterController _controller = GetComponent<CharacterController>();
        if (grounded)
        {
            Debug.Log("chay");
            moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed);
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
       // else
        {
            moveDirection.y -= lucHapDan * Time.deltaTime;
            grounded = (_controller.Move(moveDirection * Time.deltaTime) & CollisionFlags.CollidedBelow) != 0;  
        }
    }
    void Update()
    {

    }
}
