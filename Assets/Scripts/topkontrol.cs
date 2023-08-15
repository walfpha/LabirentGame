using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hareket : MonoBehaviour
{
    private Rigidbody rb;
    public float Hiz = 1.8f;
    public Text zaman, can, durum;
    float zamanSayaci = 120;
    float canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayaci -= Time.deltaTime;
            zaman.text = (int)zamanSayaci + "";
        }
        else if(!oyunTamam)
        {
            durum.text = "Kaybettiniz";
        }
        if (zamanSayaci < 0)
        {
            oyunDevam = false;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-yatay, 0, -dikey);
            rb.AddForce(kuvvet * Hiz);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        string objIsmi = other.gameObject.name;
        if (objIsmi.Equals("bitis"))
        {
            //print("Oyunu Kazandiniz");
            oyunTamam = true;
            durum.text = "Kazandýnýz";
        }
        else if (!objIsmi.Equals("zemin") && !objIsmi.Equals("labzemin") && !objIsmi.Equals("baslangic"))
            {
            canSayaci -= 1;
            can.text = canSayaci + "";
            if (canSayaci == 0)
            {
                oyunDevam = false;
            }
        }
    }
}
