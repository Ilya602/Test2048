using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Config
{
    public class GameConfig : MonoBehaviour
    {
        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 1000;
        }
    }
}