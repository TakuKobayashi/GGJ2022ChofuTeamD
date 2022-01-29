using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
    bool bottonDown = false;
    // Start is called before the first frame update
    void Start()
    {
        bottonDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (bottonDown == false)
            {
                GameController.Instance.SetResu( Resut.Lose );
                bottonDown = true;
            }
        }

    }
}
