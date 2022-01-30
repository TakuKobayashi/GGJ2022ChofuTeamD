using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Help2 : MonoBehaviour
{
    public Sprite[] helps;
    public Image helpImg;
    public int index=0;
    public Mobcast.Coffee.Transition.UITransition _uITransition;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void helpButton()
    {
        index++;
        if (helps.Length <= index)
		{
            _uITransition.Hide();
            index = 0;
            return;
        }
        helpImg.sprite = helps[index];
    }
}
