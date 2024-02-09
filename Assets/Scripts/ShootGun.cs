using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    public ParticleSystem _particleEmit;
    public int danConLai = 0;
    public int slBangDan = 10;
    int khoangCachBan = 100;
    float lucBan = 1f;
    private int danTrongBang = 5;
    private float shotTime = 0.1f;
    private float timeNextShort = 0f;
    bool particalPool = true;
    // Start is called before the first frame update
    void Start()
    {
        _particleEmit.Stop();
        //  Debug.Log("particle is stop");
        danConLai = 10;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            FireGun();
        }
    }
    void FireGun()
    {
       // Debug.Log("FireGun");
        if (danConLai == 0)
            return;
        if (Time.time - shotTime > timeNextShort)
        {
            timeNextShort = Time.time - Time.deltaTime; // tru di thoi gian vai giay de hoan thanh khung hinh
        }
        while (timeNextShort < Time.time && danConLai != 0)
        {
            FireOneShot();
            timeNextShort += shotTime; //delay time
            // do delay cua dan
        }
    }
    RaycastHit hit; // xac dinh deim tac dong
    void FireOneShot()
    {
        Vector3 direc = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, direc, out hit, khoangCachBan))
        {
            if (hit.rigidbody) // tac dong luc len vat the khi bi ban
            {
                hit.rigidbody.AddForceAtPosition(direc * lucBan, hit.point);
                if (hit.transform.tag == "Enemy" || hit.transform.tag == "Wall")
                    hit.collider.SendMessage("voidSatThuong", 5f, SendMessageOptions.DontRequireReceiver);// in loi khi cac thanh phan khong nhan duoc thong bao
                //hit.point sinh ra hieu ung va play
                //Debug.Log("121");
                if (particalPool)
                {
                    _particleEmit.transform.position = hit.point;
                    _particleEmit.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    _particleEmit.Play();
                  //  Debug.Log("phat ra");
                }
            }
        }
        //Debug.DrawRay(transform.position, direc, Color.red);
        danConLai--;
        if (danConLai == 0)
        {
            NapDan();
        }
    }
    void NapDan()
    {
        StartCoroutine("reloadDan", 2.1f);
    }
    IEnumerator reloadDan(float t)
    {
        yield return new WaitForSeconds(t);
        if (slBangDan > 0)
        {
          //  Debug.Log("nap dan");
            slBangDan--;
            danConLai = danTrongBang;
        }
    }
}
