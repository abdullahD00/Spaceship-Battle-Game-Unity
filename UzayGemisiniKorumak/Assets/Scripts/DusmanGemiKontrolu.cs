using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusmanGemiKontrolu : MonoBehaviour
{
    private float GemininCanı = 100f;
    [SerializeField] GameObject DusmanMermisi;
    private float DusmanMermiHizi = 6f;
    private float MermiAtmaOlasiligi = 0.6f;
    private SkorKontrolü SkorKontrolü;
    private int ÖldürülenDüşmanBaşınaEldeEdilenPuan = 200;

    public AudioClip AtesEtmeSesi;
    public AudioClip ÖlmeSesi;


    private void Start()
    {
        SkorKontrolü = GameObject.Find("SkorRakam").GetComponent<SkorKontrolü>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MermiKontrolu Mermi = collision.gameObject.GetComponent<MermiKontrolu>();

        if (Mermi.tag=="Mermi")
        {   
            Mermi.CarptigindaYokOl();
            GemininCanı -= Mermi.ZararVerme();
            if (GemininCanı<=0)
            {
                Destroy(gameObject);
                SkorKontrolü.SkoruArttir(ÖldürülenDüşmanBaşınaEldeEdilenPuan);
                AudioSource.PlayClipAtPoint(ÖlmeSesi, transform.position);
                
                
            }
        }
        

    }

    private void Update()
    {

        if (Random.value<(MermiAtmaOlasiligi*Time.deltaTime))
        {
            AtesEt();
        }
        
    }

    void AtesEt()
    {
        Vector3 MermiBaslangicKonumu = transform.position + new Vector3(0, -0.8f, 0);
        GameObject Mermi = Instantiate(DusmanMermisi, MermiBaslangicKonumu, Quaternion.identity) as GameObject;
        Mermi.GetComponent<Transform>().localScale = new Vector3(1f, -1f, 1f);
        Mermi.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -DusmanMermiHizi);
        AudioSource.PlayClipAtPoint(AtesEtmeSesi, transform.position);
    } 
    
}
