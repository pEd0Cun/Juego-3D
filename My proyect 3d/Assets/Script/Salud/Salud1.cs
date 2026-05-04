using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Salud1 : MonoBehaviour
{
	[SerializeField] private float saludMax = 3f;
	[SerializeField] private bool destruirAlMorir = true;
	[SerializeField] private float tiempoEnDestruirse = 0f;
	[SerializeField] private UnityEvent<float> alPerderSalud;
	[SerializeField] private UnityEvent alMorir;

	private float saludActual;
	private Animator animator;
	private bool estaMuerto = false;

	public event Action alActualizarSalud;

	private void Awake()
	{
    	animator = GetComponent<Animator>();
    	saludActual = saludMax;
	}

	private void Start()
	{
    	alActualizarSalud?.Invoke();
	}

	public bool EstaMuerto()
	{
    	return estaMuerto;
	}

	public float ObtenerFraccion()
	{
    	return saludActual / saludMax;
	}

	public float ObtenerSalud()
	{
    	return saludActual;
	}

	public void AjustarSalud(float salud)
	{
    	saludActual = salud;
    	alActualizarSalud?.Invoke();
	}

	public void PerderSalud(float saludPerdida)
	{
    	//animator.ResetTrigger("perderSalud");
    	saludActual = Mathf.Max(saludActual - saludPerdida, 0);
    	alPerderSalud?.Invoke(saludPerdida);
    	alActualizarSalud?.Invoke();
    	if (saludActual == 0)
    	{
        	Morir();
    	}
    	else
    	{
        	//animator.SetTrigger("perderSalud");
    	}
	}
	private void Morir()
	{
    	if (estaMuerto) return;
   	 
    	alMorir?.Invoke();
    	estaMuerto = true;
    	//animator.SetTrigger("morir");
    	if (destruirAlMorir)
    	{
        	Destroy(gameObject, tiempoEnDestruirse);
    	}
	}
	public void RecuperarSalud(float cantidad)
	{
		if (estaMuerto) return;
 
		saludActual = Mathf.Min(saludActual + cantidad, saludMax);
		alActualizarSalud?.Invoke();
	}
}

