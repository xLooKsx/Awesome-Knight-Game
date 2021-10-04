using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorTexture;
    private CursorMode mode = CursorMode.ForceSoftware;
    private Vector2 hotSpot = Vector2.zero;

    public GameObject mousePoint;
    private GameObject instatiatedMouse;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, mode);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //Captura quando o botao do mouse sobe, logo apos um click
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Verifica se houve alguma colisao
            if(Physics.Raycast(ray, out hit))
            {
                //Case tenha uma colisao ele verifica se essa colisao com um gameObject do tipo terrain
                if(hit.collider is TerrainCollider)
                {
                    Vector3 temp = hit.point;
                    //Apenas sobe um pouco o eixo Y
                    temp.y = 0.25f;

                    /*
                     * Instantiate cria um clone de um GameObject os paremetros sao:
                     * 1 -> O object que vc deseja copiar(mousePoint)
                     * 2 -> O vetor que vc desja inserir esse objeto
                     * 3 -> a Rotacao dele
                     * 
                     * Nesse caso o Quaternion.identity serve para pegar a rotacao padrao
                     * ("This quaternion corresponds to "no rotation" - the object is perfectly aligned 
                     *  with the world or parent axes.")
                     *  
                     *  OBS: Usando o Instantiate o retorno desse clone é um Object e nao um GameObject
                     */
                    //Instantiate(mousePoint, temp, Quaternion.identity);

                    //If statement faz com que haja apenas uma instancia do ponteiro do mouse na tela
                    if(instatiatedMouse != null)
                    {
                        Destroy(instatiatedMouse);
                    }

                    /*
                     * clona um novo ponteiro do mouse e atribui ao mouse atual
                     * 'as GameObject' instacia esse novo clone como um GameObject
                     * e insere esse novo mouse a nova posicao clicada
                     */
                    instatiatedMouse = Instantiate(mousePoint) as GameObject;
                    instatiatedMouse.transform.position = temp;
                }
            }
        }
    }

}
