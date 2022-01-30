using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private string[] ResultString = { "èüóò", "îsñk", "à¯Ç´ï™ÇØ" };

    [SerializeField]
    private Sprite[] ResultSp ;

    [SerializeField]
    private Text tex;

    [SerializeField]
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        switch (GameController.Instance.resut)
        {
            case Resut.Win:
                tex.text = ResultString[(int)Resut.Win];
                SePlayManager.PlaySeSound(SePlayManager.SE.victory_se);
                img.sprite = ResultSp[(int)Resut.Win];
                break;
            case Resut.Lose:
                tex.text = ResultString[(int)Resut.Lose];
                SePlayManager.PlaySeSound(SePlayManager.SE.defeat_se);
                img.sprite = ResultSp[(int)Resut.Lose];
                break;
            default:
            case Resut.draw:
                SePlayManager.PlaySeSound(SePlayManager.SE.victory_se);
                tex.text = ResultString[(int)Resut.draw];
                break;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
