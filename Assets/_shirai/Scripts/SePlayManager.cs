using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SePlayManager : MonoBehaviour
{
    public enum SE
    {
        attack,
        defeat_se,
        item_get,
        victory_se,
        walking_se,

    };


    static void PlaySeSound(SE _se)
    {
        SoundManager.Instance.PlaySe(_se.ToString());
    }

}
