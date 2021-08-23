using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{

	//Esta es la forma correcta de mostrar variables privadas en el inspector. 
	//No se deben hacer public variables que no queremos sean accesibles desde otras clases-
	[SerializeField]
	private string sceneToLoad;

	[SerializeField]
	private TextMeshProUGUI percentText;

	[SerializeField]
	private Image progressImage;

	// En cuanto se active el objeto, se inciar� el cambio de escena
	void Start()
	{
		//Iniciamos una corrutina, que es un m�todo que se ejecuta 
		//en una l�nea de tiempo diferente al flujo principal del programa
		StartCoroutine(LoadScene());
	}

	//Corrutina
	IEnumerator LoadScene()
	{
		AsyncOperation loading;

		//Iniciamos la carga as�ncrona de la escena y guardamos el proceso en 'loading'
		loading = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

		//Bloqueamos el salto autom�tico entre escenas para asegurarnos el control durante el proceso
		loading.allowSceneActivation = false;

		//Cuando la escena llega al 90% de carga, se produce el cambio de escena
		while (loading.progress < 0.9f)
		{

			//Actualizamos el % de carga de una forma optima 
			//(concatenar con + tiene un alto coste en el rendimiento)
			percentText.text = string.Format("{0}%", loading.progress * 100);

			//Actualizamos la barra de carga
			progressImage.fillAmount = loading.progress;

			//Esperamos un frame
			yield return null;
		}

		//Mostramos la carga como finalizada
		percentText.text = "100%";
		progressImage.fillAmount = 1;

		//Activamos el salto de escena.
		loading.allowSceneActivation = true;


	}

}
