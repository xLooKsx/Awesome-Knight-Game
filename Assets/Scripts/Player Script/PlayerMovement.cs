using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController characterController;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private float moveSpeed = 5f;
    private bool playerCanMove;
    private bool playerFinishedMovement = true;

    private Vector3 targetPosition = Vector3.zero;
    private Vector3 playerMovement = Vector3.zero;

    private float playerToPointDistance;

    private float gravity = 9.8f;
    private float height;
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateHeight();
        CheckIfFinishedMovement();
    }

    bool IsGrounded()
    {
        //Verifica se esta acontecendo uma colisao com o chao
        return collisionFlags == CollisionFlags.CollidedBelow;
    }

    void CalculateHeight()
    {
        /*
         * Controla a gravidade, caso ele esteja no chao a variavel é setada como 0,
         * se nao estiver a gravidade é multiplicada pela quantidade de frames na tela por secundo
         * e subtraida da altura que o personagem alcançou
         */
        height = IsGrounded() ? 0f : (height - gravity * Time.deltaTime);
    }

    void CheckIfFinishedMovement()
    {
        /*
         * Se o movimento terminou o poersonagem é liberado para se mover novamente, senao terminou
         * é verificado se ainda falta muito para o personagem terminar a animaçao
         */
        if (playerFinishedMovement)
        {
            MoveThePlayer();
            playerMovement.y = height * Time.deltaTime;
            collisionFlags = characterController.Move(playerMovement);
        }
        else
        {
            if (!playerAnimator.IsInTransition(0) && !playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Stand")
                && playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                //normalizeTime da animaçao é representado em um range de 0 até 1, sendo 0 o inicio e 1 o final
                playerFinishedMovement = true;
            }

        }
    }

    void MoveThePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Pega  o direcionamento da camera até o ponto que foi clicado
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Verifica se está acontecendo alguma colisao na tela, esse out serve para armazenar infomacao na variavel hit
            if (Physics.Raycast(ray, out hit))
            {
                //Verifica se a colisao esta acontecendo com um componente do tipo TerrainCollider
                if (hit.collider is TerrainCollider)
                {
                    //Transform é o objeto do jogador, atualmente, herdado pelo MonoBehavior
                    //retorna a diferenca entre os dois pontos
                    playerToPointDistance = Vector3.Distance(transform.position, hit.point);

                    if (playerToPointDistance >= 1.0f)
                    {
                        playerCanMove = true;
                        //Captura o posicionamento onde houve a colisao
                        targetPosition = hit.point;
                    }
                }
            }
        }
        //Verifica se o player pode se mexer
        if (playerCanMove)
        {
            //Invoka a animacao de andar, pelo parametro walk
            playerAnimator.SetFloat("Walk", 1.0f);

            /*
             * Cria um posicionamento temporario, pegando o eixo X e Z da colisao, 
             * o eixo y permanece o mesmo, por isso que usamos o dado y do transform
            */
            Vector3 targetTemp = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

            /*
             * Quaternion.LookRotation(targetTemp - transform.position) cria uma direcao de visao
             * Quaternion.Slerp faz a transiçao entre o posicionamento antigo(transfor) e o novo(targetTemp-transformPosition)
             * e setta em quanto tempo isso deve ocorrer(15f * deltTimer
             */
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetTemp - transform.position), 15.0f * Time.deltaTime);

            //Transaform.forwad é a representacao do eixo azul
            /*
             * Nesse caso o playerMovement esta como zero(declarado la encima) entao fica como (0, 0, 0)
             * e estou pegando o valor do eixo Z, entao o eixo de movimeto fica algo como (0,0,1.0)
             * 
             * It's easier to read Time.deltaTime as "per second".By doing that, the line can be read as "forward movespeed units 
             * per second". If you have transform.position += playerMovement, that would read as "move the player forward 
             * movespeed units per second"
             * https://discord.com/channels/493510779866316801/493511037421879316/893351078815498290
             * e ele se mexe somente no eixo z porque todo o resto esta como 0
             * 
             */
            playerMovement = transform.forward * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) <= 0.5f)
            {
                playerCanMove = false;
            }
        }
        else
        {
            playerMovement.Set(0f, 0f, 0f);
            playerAnimator.SetFloat("Walk", 0f);
        }
    }
}
