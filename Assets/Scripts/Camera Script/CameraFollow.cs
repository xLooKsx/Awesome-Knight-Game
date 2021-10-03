using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followHeight = 8f;
    public float followDistance = 6f;

    private Transform player;

    private float targetHeight;
    private float currentHeight;
    private float currentRotation;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //A altura da camera que queremos vai ser a altura do personagem + followHeight
        targetHeight = player.position.y + followHeight;

        //Faz a captura da rotacao atual da camera no eixo Y
        currentRotation = transform.eulerAngles.y;

        /*
         * Setta a altura da camera
         * Mathf.lerp faz a transiçao de um valor x para um valor y e usa o 0.9*time.deltaTime como um guia de tempo,EX:
         * x = 10
         * y = 50
         * tempo = 10s
         * 
         * Faz a transiçao de 10 até 50 em 10 segundos
         */
        currentHeight = Mathf.Lerp(transform.position.y, targetHeight, 0.9f * Time.deltaTime);

        //Faz a rotaçao no eixo Y
        Quaternion euler = Quaternion.Euler(0f, currentRotation, 0f);

        Vector3 targetPosition = player.position - (euler * Vector3.forward) * followDistance;

        targetPosition.y = currentHeight;

        transform.position = targetPosition;
        transform.LookAt(player);
    }
}
