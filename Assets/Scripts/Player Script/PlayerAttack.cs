using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public Image fillWaitImage1;
    public Image fillWaitImage2;
    public Image fillWaitImage3;
    public Image fillWaitImage4;
    public Image fillWaitImage5;
    public Image fillWaitImage6;

    private int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };

    private Animator animamator;
    private bool canAttack = true;

    private PlayerMovement playerMovement;

    void Start()
    {
        animamator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!animamator.IsInTransition(0) && animamator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        CheckToFade();
        CheckInput();
    }

    void CheckInput() { 
        
        /*
         * Verifica se o parametro 'Atk' na aba Animator esta igual a 0
         * se estiver igual a 0 significa que nao tem nenhuma animacao
         * de atack rolando, mas sim uma animaçao de movimento
         */
        if(animamator.GetInteger("Atk") == 0)
        {
            playerMovement.PlayerFinishedMovement = false;

            if (!animamator.IsInTransition(0) && animamator.GetCurrentAnimatorStateInfo(0).IsName("Stand"))
            {
                playerMovement.PlayerFinishedMovement = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            /*
             * Faz com que a posicao que o player se mova
             * seja a posicao atual dele, ou seja o player
             * nao ira se mover durante o ataque
             */
            playerMovement.TargetPosition = transform.position;

            /*
             * Para atacar o player nao pode estar se movendo(playerMovement.PlayerFinishedMovement)
             * nao pode ter o respectivo ataque em cooldown(fadeImages[0] != 1)
             * e precisa estar livre para atacar (canAttack)
             */
            if (playerMovement.PlayerFinishedMovement && fadeImages[0] != 1 && canAttack)
            {
                //Seta o cooldown
                fadeImages[0] = 1;

                //Setta a variavel de ataque do animator como 1, para rodar a primeira imagem de ataque
                animamator.SetInteger("Atk", 1);
            }
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerMovement.TargetPosition = transform.position;
            if (playerMovement.PlayerFinishedMovement && fadeImages[1] != 1 && canAttack)
            {
                fadeImages[1] = 1;
                animamator.SetInteger("Atk", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerMovement.TargetPosition = transform.position;
            if (playerMovement.PlayerFinishedMovement && fadeImages[2] != 1 && canAttack)
            {
                fadeImages[2] = 1;
                animamator.SetInteger("Atk", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerMovement.TargetPosition = transform.position;
            if (playerMovement.PlayerFinishedMovement && fadeImages[3] != 1 && canAttack)
            {
                fadeImages[3] = 1;
                animamator.SetInteger("Atk", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerMovement.TargetPosition = transform.position;
            if (playerMovement.PlayerFinishedMovement && fadeImages[4] != 1 && canAttack)
            {
                fadeImages[4] = 1;
                animamator.SetInteger("Atk", 5);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            playerMovement.TargetPosition = transform.position;
            if (playerMovement.PlayerFinishedMovement && fadeImages[5] != 1 && canAttack)
            {
                fadeImages[5] = 1;
                animamator.SetInteger("Atk", 6);
            }
        }
        else
        {
            animamator.SetInteger("Atk", 0);
        }


        //Faz com que o player siga o ponteiro do mouse quando a tecla space estiver pressionada
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 targetPosition = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            }

            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(targetPosition - transform.position), 
                15f * Time.deltaTime);
        }
    }

    void CheckToFade()
    {
        if(fadeImages[0] == 1)
        {
            if(FadeAndWait(fillWaitImage1, 1.0f))
            {
                fadeImages[0] = 0;
            }
        }

        if (fadeImages[1] == 1)
        {
            if (FadeAndWait(fillWaitImage2, 0.7f))
            {
                fadeImages[1] = 0;
            }
        }

        if (fadeImages[2] == 1)
        {
            if (FadeAndWait(fillWaitImage3, 0.1f))
            {
                fadeImages[2] = 0;
            }
        }

        if (fadeImages[3] == 1)
        {
            if (FadeAndWait(fillWaitImage4, 0.2f))
            {
                fadeImages[3] = 0;
            }
        }

        if (fadeImages[4] == 1)
        {
            if (FadeAndWait(fillWaitImage5, 0.3f))
            {
                fadeImages[4] = 0;
            }
        }

        if (fadeImages[5] == 1)
        {
            if (FadeAndWait(fillWaitImage6, 0.8f))
            {
                fadeImages[5] = 0;
            }
        }
    }

    //Metodo para fazer o cooldown das imagens de skill
    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        bool faded = false;

        if (fadeImage == null)
            return faded;

        /*
         * Verifica se a imagem esta ativa ou nao(checkbox ticado ou nao, 
         * ao lado do nome do objeto ma cena), por default nenhum deles vem ativado
         */
        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if(fadeImage.fillAmount <= 0.0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }

        return faded;
    }
}
