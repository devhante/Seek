using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seek
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private Transform childCardPosition;

        private Camera _mainCamera;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private bool _isDragging;
        private Vector2 _moveAmount;
        private Vector2 _offset;
        private Vector2 _originPosition;
        private List<Card> _overlappedCards;
        [SerializeField] private Card _parentCard;
        [SerializeField] private Card _childCard;
        [SerializeField] private List<Card> _childCards;

        public void SetMouseDown()
        {
            Debug.Log(gameObject.name);
            transform.SetAsFirstSibling();
            if (_parentCard != null)
            {
                _parentCard._childCard = null;
                _parentCard.SetIgnoreCollision(false);
                _parentCard.SetChildCards(null, new List<Card>());
                _parentCard.SetIgnoreCollision(true);
                _parentCard = null;
            }

            _originPosition = transform.position;
            _isDragging = true;
            SetTrigger(true);
            UpdateOffset();
            MoveUp();
        }

        public void SetMouseUp()
        {
            _isDragging = false;
            if (_overlappedCards.Count > 0)
            {
                Card result = _overlappedCards[0];
                float shortest = Vector3.Distance(transform.position, _overlappedCards[0].transform.position);
                foreach (Card card in _overlappedCards)
                {
                    if (card._childCard)
                        continue;

                    float distance = Vector3.Distance(transform.position, card.transform.position);
                    if (distance < shortest)
                    {
                        result = card;
                    }
                }

                if (!result._childCard)
                {
                    _parentCard = result;
                    result._childCard = this;
                }
            }

            _overlappedCards.Clear();
            if (_parentCard)
            {
                _parentCard.SetChildCards(this, _childCards);
                _parentCard.SetIgnoreCollision(true);
            }

            SetTrigger(false);
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
            _isDragging = false;
            _offset = Vector2.zero;
            _overlappedCards = new List<Card>();
            _childCard = null;
            _childCards = new List<Card>();
        }

        private void Update()
        {
            if (_isDragging)
            {
                _moveAmount = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                _moveAmount += _offset;
                transform.Translate(_moveAmount);
            }

            if (_childCard != null)
            {
                _childCard.transform.position = childCardPosition.position;
            }
        }

        private void FixedUpdate()
        {
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(_mainCamera.ScreenPointToRay(Input.mousePosition));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isDragging && other.CompareTag("Card"))
            {
                _overlappedCards.Add(other.GetComponent<Card>());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_isDragging && other.CompareTag("Card"))
            {
                _overlappedCards.Remove(other.GetComponent<Card>());
            }
        }

        public void SetTrigger(bool value)
        {
            _collider.isTrigger = value;
            if (_childCard)
                _childCard.SetTrigger(value);
        }

        public void SetIgnoreCollision(bool value)
        {
            foreach (Card card in _childCards)
            {
                Physics2D.IgnoreCollision(_collider, card._collider, value);
            }

            if (_parentCard)
                _parentCard.SetIgnoreCollision(value);
        }

        private void UpdateOffset()
        {
            _offset = transform.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public void MoveUp()
        {
            int result = _spriteRenderer.sortingOrder;


            foreach (var sr in transform.parent.GetComponentsInChildren<SpriteRenderer>())
            {
                result = Mathf.Max(result, sr.sortingOrder);
            }

            _spriteRenderer.sortingOrder = result + 1;

            if (_childCard != null)
            {
                _childCard.MoveUp();
            }
        }

        public void SetChildCards(Card card, List<Card> cards)
        {
            _childCards.Clear();
            if (card)
                _childCards.Add(card);
            foreach (Card item in cards)
            {
                _childCards.Add(item);
            }

            if (_parentCard)
                _parentCard.SetChildCards(this, _childCards);
        }

        public int GetSortingOrder()
        {
            return _spriteRenderer.sortingOrder;
        }
    }
}