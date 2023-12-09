using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Seek
{
    [Serializable]
    public class SaveData
    {
        public bool Saved { get; set; }
        public int Progress { get; set; }
        public string Time { get; set; }
    }
}