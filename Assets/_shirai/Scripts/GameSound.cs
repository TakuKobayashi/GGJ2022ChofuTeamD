using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    SoundManager.Handle bgm_handle;
    public enum bgm
    {
        BGM01,
        BGM02,
        BGM03,
        BGM04,
        BGM05,
        Max
    };
    [Range(0.0f,1.0f)]
    public float seVol = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        if (SoundManager.Instance == null)
            return;
        //Set BGM volume
        SoundManager.Instance.Volume.bgm = seVol;

        var i = (bgm)Random.Range(0, (int)bgm.Max);
        //Play BGM
        bgm_handle = SoundManager.Instance.PlayBgm(i.ToString());

    }


}
