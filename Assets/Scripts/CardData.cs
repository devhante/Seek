using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seek
{
    [Serializable]
    public class CardData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public bool Bag { get; set; }
    }
}
