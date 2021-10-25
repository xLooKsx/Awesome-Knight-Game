using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEffects : MonoBehaviour
{
    public GameObject groundImpactSpawn;
    public GameObject kickFxSpawn;
    public GameObject fireTornadoSpawn;
    public GameObject fireShieldSpwan;
    public GameObject groundImpactPrefab;
    public GameObject kickFxPrefab;
    public GameObject fireTornadoPrefab;
    public GameObject fireShieldPrefab;
    public GameObject healFxPrefab;
    public GameObject thunderFxPrefab;

    void GroundImpact()
    {
        Instantiate(groundImpactPrefab, groundImpactSpawn.transform.position, Quaternion.identity);
    }

    void Kick()
    {
        Instantiate(kickFxPrefab, kickFxSpawn.transform.position, Quaternion.identity);
    }

    void FireTornado()
    {
        Instantiate(fireTornadoPrefab, fireTornadoSpawn.transform.position, Quaternion.identity);
    }

    void FireShield()
    {
        GameObject fireObject = Instantiate(fireShieldPrefab, fireShieldSpwan.transform.position, Quaternion.identity)
            as GameObject;
        fireObject.transform.SetParent(transform);
    }

    void Heal()
    {
        Vector3 temp = transform.position;
        temp.y += 2f;
        GameObject healObject = Instantiate(healFxPrefab, temp, Quaternion.identity)
            as GameObject;
        healObject.transform.SetParent(transform);
    }

    void ThunderAttack()
    {
        for(int i = 0; i<8; i++)
        {
            Vector3 position = Vector3.zero;

            if(i == 0)
            {
                position = new Vector3(transform.position.x - 4f, transform.position.y + 2f, transform.position.z);
            }else if(i == 1)
            {
                position = new Vector3(transform.position.x + 4f, transform.position.y + 2f, transform.position.z);
            }
            else if (i == 2)
            {
                position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z - 4f);
            }
            else if (i == 3)
            {
                position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z + 4f);
            }
            else if (i == 4)
            {
                position = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z - 2.5f);
            }
            else if (i == 5)
            {
                position = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }
            else if (i == 6)
            {
                position = new Vector3(transform.position.x - 2.5f, transform.position.y + 2f, transform.position.z - 2.5f);
            }
            else if (i == 7)
            {
                position = new Vector3(transform.position.x + 2.5f, transform.position.y + 2f, transform.position.z + 2.5f);
            }

            Instantiate(thunderFxPrefab, position, Quaternion.identity);
        }
    }
}
