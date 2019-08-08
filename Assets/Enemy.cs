using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject deathFX;
    void Start()
    {
        deathFX.SetActive(false);
     //   Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnParticleCollision(GameObject obj)
    {
        Destroy(gameObject);
        print("hitttt");
        Instantiate(deathFX, transform.position, Quaternion.identity);
    }
}
