using System;
using System.Collections;
using System.Collections.Generic;
using Seek;
using UnityEngine;

namespace Seek.GameScene
{
    public class ClickCardController : MonoBehaviour
    {
        private Camera _mainCamera;
        private Card draggingCard;

        private void Awake()
        {
            _mainCamera = Camera.main;
            draggingCard = null;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D[] hits =
                    Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(Input.mousePosition));
                Card result = null;
                int maxSortingOrder = -1;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!hits[i].transform.TryGetComponent(out Card card)) continue;
                    int sortingOrder = card.GetSortingOrder();
                    if (sortingOrder > maxSortingOrder)
                    {
                        result = card;
                        maxSortingOrder = sortingOrder;
                    }
                }

                if (result)
                {
                    draggingCard = result;
                    draggingCard.SetMouseDown();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (draggingCard)
                {
                    draggingCard.SetMouseUp();
                    draggingCard = null;
                }
            }
        }
    }
}
