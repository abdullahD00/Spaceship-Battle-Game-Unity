using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemiKontrol : MonoBehaviour
{
      float Speed=10f;
     private  float Xmin = -5.8f;
     private  float Xmax = 5.8f;
     private  float Xyeni;
     float İnceAyar = 0.7f;
     float uzaklık;
    [SerializeField] GameObject MermiGemi;
    private float MermiPower = 8f;
    private float GemininCanı = 400f;

    private SkorKontrolü SkorKontrolü;

    public AudioClip GemimizAtesEtmeSesi;
    public AudioClip GemimizÖlmeSesi;

    void Start()
    {

        SkorKontrolü = GameObject.Find("SkorRakam").GetComponent<SkorKontrolü>();
        
       
        uzaklık = transform.position.z - Camera.main.transform.position.z;
        Vector3 SolUc = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, uzaklık));
        Vector3 SagUc = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, uzaklık));
        Xmin = SolUc.x + İnceAyar;
        Xmax = SagUc.x - İnceAyar;
       
    }

   
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {

            InvokeRepeating("AtesEtme", 0.0000000001f, 0.5f); // (fonksiyon,ne kadar sonra başlayacağı,ateş etme aralığı);
            
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            CancelInvoke("AtesEtme");
        }

        Xyeni = Mathf.Clamp(transform.position.x, Xmin, Xmax);
        transform.position = new Vector3(Xyeni, transform.position.y, transform.position.z);
        

        if (Input.GetKey(KeyCode.D))
        {
            transform.position =transform.position + new Vector3(Speed * Time.deltaTime, 0, 0);
           // transform.position += Vector3.right*Time.deltaTime * Speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position =transform.position - new Vector3(Speed * Time.deltaTime, 0, 0);
            // transform.position += Vector3.left * Speed * Time.deltaTime;
        }
       
    }
    void AtesEtme()
    {
            GameObject GemimizinMermisi = Instantiate(MermiGemi, transform.position + new Vector3(0,1f), Quaternion.identity) as GameObject;
            GemimizinMermisi.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, MermiPower, 0f);
        AudioSource.PlayClipAtPoint(GemimizAtesEtmeSesi, transform.position);
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MermiKontrolu Mermi = collision.gameObject.GetComponent<MermiKontrolu>();

        if (Mermi)
        {
            Mermi.CarptigindaYokOl();
            GemininCanı -= Mermi.ZararVerme();
            if (GemininCanı <= 0)
            {
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(GemimizÖlmeSesi, transform.position);
                SkorKontrolü.SkoruSıfırla();
                
            }
        }

    }
}
