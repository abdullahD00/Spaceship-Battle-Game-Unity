using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MermiKontrolu : MonoBehaviour
{
    private float VerdigiZarar = 10f;

    public void CarptigindaYokOl()
    {
        Destroy(gameObject);
    }
    public float ZararVerme()
    {
        return VerdigiZarar;    
    }
}
