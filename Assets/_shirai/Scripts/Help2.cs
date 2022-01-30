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
    public Mobcast.Coffee.Transition.UITransition _uITransition2;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void helpButtonOpne()
    {
        if (_uITransition2.isShow)
        {
            _uITransition2.Hide();
        }
        _uITransition.Show();
    }
    public void helpButtonOpne2()
    {
        if (_uITransition.isShow)
        {
            _uITransition.Hide();
        }
        _uITransition2.Show();
    }
    public void helpButton()
    {
        _uITransition.Hide();
    }
    public void helpButton2()
    {
        index++;
        if (helps.Length <= index)
		{
            _uITransition2.Hide();
            index = 0;
            return;
        }
        helpImg.sprite = helps[index];
    }
}
