using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem : MonoBehaviour
{
    public float distancia = 3f;

    private Vector3 destino;

    [Range(0.0f, 0.5f)]
    public float velocidade = 0.015f;

    [Range(0.0f, 5f)]
    public float velocidadeRotacao = 2f;


    private void Start()
    {
        destino = transform.position;
    }

    private void Update()
    {
        AtualizarPosicaoDestino();

        Movimentar();
        Rotacionar();
    }

    private void Rotacionar()
    {
        var rotacaoDestino = Quaternion.LookRotation(destino);
        var rotacao = Quaternion.Slerp(transform.rotation, rotacaoDestino, velocidadeRotacao * Time.deltaTime);

        rotacao.eulerAngles = new Vector3(0f, rotacao.eulerAngles.y, 0f);

        transform.rotation = rotacao;
    }

    private void Movimentar()
    {
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    private void AtualizarPosicaoDestino()
    {
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distancia))
            {
                destino = hit.point;
                destino.y = transform.position.y;
            }
        }
    }
}