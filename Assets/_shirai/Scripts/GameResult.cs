using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameResult : MonoBehaviour
{
    [SerializeField] string[] loadScenes;

    public float time = 0;
    public float timer = 2;

    // Start is called before the first frame update
    void Start()
    {

    }
    private bool bottonDown = false;
    // Update is called once per frame
    void Update()
    {
        if (timer < time)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                if (bottonDown == false)
                    StartCoroutine("Go");
            }
        }
        time += Time.deltaTime;

    }


    private IEnumerator Go()
		{
            bottonDown = true;

                foreach (var scene in loadScenes)
				{
					yield return SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
				}
          SceneManager.UnloadScene("result");

        }
}
