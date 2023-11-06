using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seek
{
    public class ResourceManager : MonoBehaviour
    {
        public Sprite GetCardSpriteByCardName(CardName cardName)
        {
            switch (cardName)
            {
                case CardName.Wood:
                    return Resources.Load<Sprite>($"Cards/Wood");
                case CardName.Plate:
                    return Resources.Load<Sprite>($"Cards/Plate");
                default:
                    return null;
            }
        }
    }
}