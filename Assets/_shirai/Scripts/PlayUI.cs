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

    [SerializeField]
    private Mobcast.Coffee.Transition.UIAnimation p1;
    [SerializeField]
    private Mobcast.Coffee.Transition.UIAnimation p2;
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
            p1.enabled = true;
            p2.enabled = false;
        }
        else
        {
            imgbg.color = colors[1];
            p1.enabled = false;
            p2.enabled = true;
        }

    }
}
