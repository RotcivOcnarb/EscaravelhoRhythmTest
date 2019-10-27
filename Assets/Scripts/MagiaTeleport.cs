using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagiaTeleport : MonoBehaviour
{

    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMe(){
        Destroy(gameObject);
    }

    public void Shoot(){

        for(int i = 0; i < 16; i ++){
            float step = 360 / 16f * i;

            GameObject bo = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            bo.GetComponent<Bullet>().velocity = new Vector3(Mathf.Cos(step * Mathf.Deg2Rad), Mathf.Sin(step * Mathf.Deg2Rad), 0) * 3;
        }
        
    }
}
