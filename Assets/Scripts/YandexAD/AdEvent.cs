using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdEvent : MonoBehaviour
{
    public StartGame startGame;

    public void OpenVullScreenAd()
    {
        startGame.enabled = false;
    }

    public void CloseVullScreenAd()
    {
        startGame.enabled = true;
    }
}
