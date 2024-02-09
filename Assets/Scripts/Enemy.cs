using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Image _healthbarsprite;
    public Transform mainCam;
    public float maxhealth;
    float healthCurrent;
    void Start()
    {
        healthCurrent = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        _healthbarsprite.transform.parent.LookAt(mainCam);
    }
    public void voidSatThuong(float num)
    {
        healthCurrent -= num;
        _healthbarsprite.fillAmount = healthCurrent/maxhealth;  
        Debug.Log("Tr? sat th??ng:" + num);
        Debug.Log("mau hien tai" + healthCurrent);
    }    
}
