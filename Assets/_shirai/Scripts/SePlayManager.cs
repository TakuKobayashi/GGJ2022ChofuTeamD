using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class SePlayManager
{
    public enum SE
    {
        attack,
        defeat_se,
        item_get,
        victory_se,
        walking_se,

    };


    static public void PlaySeSound(SE _se)
    {
        if(SoundManager.Instance)
            SoundManager.Instance.PlaySe(_se.ToString());
    }

}
