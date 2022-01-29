using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameTransfer : MonoBehaviour
{
    [SerializeField] string[] loadScenes;
    SoundManager.Handle bgm_handle;


    // Start is called before the first frame update
    void Start()
    {
        //Set BGM volume
        SoundManager.Instance.Volume.bgm = 0.05f;
        //Play BGM
        bgm_handle = SoundManager.Instance.PlayBgm("OP");

    }
    private bool bottonDown = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if(bottonDown==false)
                StartCoroutine("Go");
        }
    }


    	private IEnumerator Go()
		{
            bottonDown = true;

                foreach (var scene in loadScenes)
				{
					yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
				}
        SoundManager.Instance.StopBgm();
          SceneManager.UnloadScene("GameTitle");

        }
}
