using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoME : MaquinaEstados
{
    [Header("Objetivo")]
    [SerializeField] private GameObject objetivo;

    [Header("Detección")]
    [SerializeField] private float rangoDeteccion = 5f;
    [SerializeField] private float tiempoAgresion = 5f;

    [Header("Movimiento")]
    [SerializeField] private float velVoltearAVer = 5f;

    [Header("Ataque")]
    [SerializeField] private float danioAtaque = 1f;
    [SerializeField] private float tiempoEntreAtaques = 1.5f;

    // Propiedades públicas
    public float RangoDeteccion => rangoDeteccion;
    public float VelVoltearAVer => velVoltearAVer;
    public float TiempoAgresion => tiempoAgresion;

    public float DanioAtaque => danioAtaque;
    public float TiempoEntreAtaques => tiempoEntreAtaques;

    public NavMeshAgent NavMeshAgent { get; private set; }
    public Transform TransformObjetivo { get; private set; }
    public float DistanciaAlObjetivo { get; private set; } = Mathf.Infinity;

    [NonSerialized] public float ContTiempoAgresion;

    private void Awake()
    {
        var estados = new Dictionary<Type, EstadoBase>()
        {
            {typeof(EstadoReposo), new EstadoReposo(this)},
            {typeof(EstadoPerseguir), new EstadoPerseguir(this)},
            {typeof(EstadoAtacar), new EstadoAtacar(this)},
        };

        DefinirEstados(estados);

        NavMeshAgent = GetComponent<NavMeshAgent>();

        if (objetivo != null)
        {
            TransformObjetivo = objetivo.transform;
        }
        else
        {
            Debug.LogError("No se asignó el objetivo (jugador) en el enemigo");
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (TransformObjetivo != null)
        {
            DistanciaAlObjetivo = Vector3.Distance(
                TransformObjetivo.position,
                transform.position
            );
        }
    }

    protected override Type ObtenerEstadoInicial()
    {
        return typeof(EstadoReposo);
    }

    public bool RevisarDistancia()
    {
        return (DistanciaAlObjetivo <= RangoDeteccion);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}