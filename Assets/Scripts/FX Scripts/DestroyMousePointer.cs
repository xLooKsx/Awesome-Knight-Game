using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMousePointer : MonoBehaviour
{
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3.Distance calcula a distancia entre dois vetores
        if(Vector3.Distance(transform.position, player.position) <= 1.5f)
        {
            //Destroi o gameObject atual, nesse caso o mousePointer
            Destroy(gameObject);
        }
    }
}
