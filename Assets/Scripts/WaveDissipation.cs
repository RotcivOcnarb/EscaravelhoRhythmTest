using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDissipation : MonoBehaviour
{
    public Vector3 velocity;

    public float yMax;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + velocity * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y += (yMax - pos.y) / 10f;
        transform.position = pos;
    }

    public void DestroyMe(){
        Destroy(gameObject);
    }
}
