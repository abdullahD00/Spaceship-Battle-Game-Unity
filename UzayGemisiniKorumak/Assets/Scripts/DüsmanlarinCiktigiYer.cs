using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DüsmanlarinCiktigiYer : MonoBehaviour
{
    [SerializeField] GameObject DusmanPrefab;
    [SerializeField] float Genislik;
    [SerializeField] float Yukseklik;
    private bool SagaHareket = true;
    private float HızGemiler = 5f;
    private float XgemiMin = -1.55f;
    private float XgemiMax = 1.55f;     
    private float Uzaklik;
    private float İnceAyar = 0.7f;

    

    


    void Start()
    {
        
        Uzaklik = transform.position.z - Camera.main.transform.position.z;
        Vector3 SagUc = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, Uzaklik));
        Vector3 SolUc = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Uzaklik));
        XgemiMin = SolUc.x + İnceAyar;
        XgemiMax = SagUc.x - İnceAyar;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Genislik, Yukseklik));
    }

    void Update()
    {
        if (SagaHareket)
        {
            transform.position += Vector3.right * HızGemiler * Time.deltaTime;
        }
        else if (!SagaHareket)
        {
            transform.position += Vector3.left * HızGemiler * Time.deltaTime;
        }

        float SagSinir = transform.position.x + Genislik / 2.70f;
        float SolaSinir = transform.position.x - Genislik / 2.70f;

        if (SagSinir > XgemiMax)
        {
            SagaHareket = false;
        }
        else if (SolaSinir < XgemiMin)
        {
            SagaHareket = true;
        }
        if (DusmanlarinHepsiOlduMu())
        {
            DusmanlarinTekTekYaratilmasi();
        }
    }


    Transform SonrakiUygunPozisyon()
    {
        foreach (Transform cocuklarinpozisyonlari in transform)
        {
            if (cocuklarinpozisyonlari.childCount==0)
            {
                return cocuklarinpozisyonlari;
            }

        }

        return null;
    }


      void DusmanlarinTekTekYaratilmasi()
    {
        Transform UygunPozisyon = SonrakiUygunPozisyon();

        if (UygunPozisyon)
        {
            GameObject dusman = Instantiate(DusmanPrefab, UygunPozisyon.transform.position, Quaternion.identity);
            dusman.transform.parent = UygunPozisyon;
        }
        if (SonrakiUygunPozisyon())
        {
            Invoke("DusmanlarinTekTekYaratilmasi", 1f);
        }
    }


    
    bool DusmanlarinHepsiOlduMu()
    {
        foreach (Transform CocuklarinPozisyonlari in transform)
        {
            if (CocuklarinPozisyonlari.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
    private void DusmanlarinYenidenYaratilmasi()
    {
        foreach (Transform cocuk in transform)
        {
            GameObject dusman = Instantiate(DusmanPrefab, cocuk.transform.position, Quaternion.identity);
            dusman.transform.parent = cocuk;
        }
    }
    
}
