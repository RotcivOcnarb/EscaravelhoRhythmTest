using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

public class LurkerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject magiaPrefab;
    public GameObject bulletPrefab;
    public GameObject wavePrefab;
    float alpha = 0;

    public Sprite bulletSprite;

    int wave_f = 1;

    Vector3 startPosition;

    void Start()
    {
        Koreographer.Instance.RegisterForEvents("teleport", TeleportRandom);
        Koreographer.Instance.RegisterForEvents("waves", SpawnWaves);
        Koreographer.Instance.RegisterForEvents("notas", SpawnNote);

        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
        alpha += (1 - alpha) / 10f;
    }
    public void TeleportRandom(KoreographyEvent evt){
        alpha = 0;
        GameObject go = Instantiate(magiaPrefab, transform.Find("Lurker").gameObject.transform.position, magiaPrefab.transform.rotation);
        go.GetComponent<MagiaTeleport>().bulletPrefab = bulletPrefab;

        Vector2 desloc = Random.insideUnitCircle.normalized;
        transform.Find("Lurker").gameObject.transform.localPosition = new Vector3(desloc.x, desloc.y)*2f;

        if(evt.GetIntValue() == 1){
            transform.Find("Lurker").gameObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void SpawnWaves(KoreographyEvent evt){
        Debug.Log("Wave");

        GameObject go = Instantiate(wavePrefab, transform.position, wavePrefab.transform.rotation);
        WaveDissipation wd = go.GetComponent<WaveDissipation>();
        go.transform.position = new Vector3(go.transform.position.x, -5, go.transform.position.z);
        wd.velocity = new Vector3(5 * wave_f, 0, 0);
        wd.yMax = -3.5f;

        wave_f *= -1;

    }

    public void SpawnNote(KoreographyEvent evt){
        GameObject bl = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
        Destroy(bl.GetComponent<Animator>());
        bl.GetComponent<SpriteRenderer>().sprite = bulletSprite;
        bl.GetComponent<Bullet>().velocity = Random.insideUnitCircle.normalized * 5f;
        bl.transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }
}
