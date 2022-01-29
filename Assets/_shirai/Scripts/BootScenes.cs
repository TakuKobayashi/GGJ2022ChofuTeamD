
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
	public class BootScenes : MonoBehaviour
	{

		[SerializeField] bool BootLoad = true;
		[SerializeField] string[] loadScenes;
		

		private IEnumerator Start()
		{
			if (BootLoad)
			{
				foreach (var scene in loadScenes)
				{
					yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
				}
			}
		}
	}
}