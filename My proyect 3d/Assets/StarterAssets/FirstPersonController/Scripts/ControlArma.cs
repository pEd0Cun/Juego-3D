using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlArma : MonoBehaviour
{
    [SerializeField] private Arma arma;

    public void OnDisparar(InputValue value)
    {
        Debug.Log("Input System detectó disparo");

        arma.ProcesarEntrada(value.isPressed);
    }
}