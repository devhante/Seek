using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seek.GameScene
{
    [Serializable]
    public class SynthesizeData
    {
        public string[] Materials { get; set; } = Array.Empty<string>();
        public string[] Results { get; set; } = Array.Empty<string>();
        public int Time { get; set; } = 0;
    }
}

