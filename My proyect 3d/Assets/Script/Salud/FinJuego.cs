using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinJuego : MonoBehaviour
{
	[SerializeField] private int indiceMenuPrincipal = 0;
	[SerializeField] private int segCargarMenu = 1;

	public void VolverMenuPrincipal()
	{
    	StartCoroutine(CargarMenu());
	}

	private IEnumerator CargarMenu()
	{
    	yield return new WaitForSeconds(segCargarMenu);

    	SceneManager.LoadScene(indiceMenuPrincipal);
	}
}
