using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : SingletonBehaviour<GameController>
{
    public Square SelectingSquare = null;
	private Resut _resut = Resut.none;

	public Resut resut
	{ get { return _resut;  } }


	public void SetResu(Resut re)
	{
		if(SoundManager.Instance)
		SoundManager.Instance.StopBgm();

		_resut = re;
		StartCoroutine("Go");
	}

	

	private IEnumerator Go()
	{
		yield return new WaitForSeconds(1.0f);

		SceneManager.LoadScene("result");

	}
}
