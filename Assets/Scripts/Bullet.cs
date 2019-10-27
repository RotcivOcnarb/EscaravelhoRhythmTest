using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float timer = 0;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + velocity * Time.deltaTime;

        timer += Time.deltaTime;
        
        if(timer > 5){
            Destroy(gameObject);
        }
    }
}
