using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAtacar : EstadoBase
{
    private EnemigoME enemigoME;

    private Salud1 saludJugador;

    private float temporizadorAtaque;

    public EstadoAtacar(EnemigoME maquinaEstados) : base(maquinaEstados)
    {
        enemigoME = (EnemigoME)maquinaEstados;
    }

    public override void Entrar()
    {
        base.Entrar();
        Debug.Log("Entró a Estado Atacar");

        // Obtener referencia al script de salud del jugador
        saludJugador = enemigoME.TransformObjetivo.GetComponent<Salud1>();

        temporizadorAtaque = 0f;
    }

    public override void UpdateLogica()
    {
        base.UpdateLogica();
        VoltearAVerObjetivo();

        // Si el jugador se aleja, volver a perseguir
        if (enemigoME.DistanciaAlObjetivo > enemigoME.NavMeshAgent.stoppingDistance)
        {
            enemigoME.CambiarEstado(typeof(EstadoPerseguir));
            return;
        }

        // Temporizador de ataque
        temporizadorAtaque += Time.deltaTime;

        if (temporizadorAtaque >= enemigoME.TiempoEntreAtaques)
        {
            Atacar();
            temporizadorAtaque = 0f;
        }
    }

    private void Atacar()
    {
        Debug.Log("Atacar");

        if (saludJugador != null && !saludJugador.EstaMuerto())
        {
            saludJugador.PerderSalud(enemigoME.DanioAtaque);
        }
    }

    private void VoltearAVerObjetivo()
    {
        Vector3 direccion = (enemigoME.TransformObjetivo.position - enemigoME.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
        enemigoME.transform.rotation = Quaternion.Slerp(
            enemigoME.transform.rotation,
            lookRotation,
            Time.deltaTime * enemigoME.VelVoltearAVer
        );
    }
}