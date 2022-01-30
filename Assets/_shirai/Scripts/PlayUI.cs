using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField]
    private Image imgbg;

    [SerializeField]
    private Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.isMyTurn())
        {
            imgbg.color = colors[0];
        }
        else
        {
            imgbg.color = colors[1];
        }

    }
}
