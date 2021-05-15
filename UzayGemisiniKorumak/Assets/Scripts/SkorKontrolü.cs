using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkorKontrolü : MonoBehaviour
{
    private Text SkorMetni;
    private int Skor;

    void Start()
    {
        SkorMetni = GetComponent<Text>();
    }

    
    void Update()
    {
        
    }

    public void SkoruArttir(int Puan)
    {
        Skor += Puan;
        SkorMetni.text = Skor.ToString();
    }
    public void SkoruSıfırla()
    {
        Skor = 0;
        SkorMetni.text = Skor.ToString();
    }
}
