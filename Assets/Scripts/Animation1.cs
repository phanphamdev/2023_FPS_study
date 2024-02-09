using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1 : MonoBehaviour
{
    // Start is called before the first frame update
   
    Animation anim;

    bool _reload = false;


    void Start()
    {
        anim = GetComponent<Animation>();
       _reload = false;
        anim.CrossFade("Take_In",1f);
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            anim.CrossFade("Fire");
        }
        else if (_reload || transform.GetComponent<ShootGun>().danConLai == 0) 
        {
            StartCoroutine("funReload", 2.1f);
        }
        else
        {
            anim.CrossFade("Idle");
        }
        if(Input.GetKey(KeyCode.R))
        {
            _reload = true;
        } 
       
    }
    IEnumerator funReload(int t)
    {
        anim.CrossFade("Reload");
        yield return new WaitForSeconds(t);
        _reload = false;
    }
}
