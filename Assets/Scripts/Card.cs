using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seek
{
    public enum CardName
    {
        None,
        Wood,
        Plate
    }
    
    public class Card : MonoBehaviour
    {
        [SerializeField] private Transform childCardPosition;

        private Camera _mainCamera;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider;
        private bool _isDragging;
        private Vector2 _moveAmount;
        private Vector2 _offset;
        private List<Card> _overlappedCards;
        private CardManager _cardManager;
        private Card _parentCard;
        private Card _childCard;
        private ResourceManager _resourceManager;

        private CardName _cardName;

        public List<Card> ChildCards { get; set; }
        public CardName CardName
        {
            get => _cardName;
            set
            {
                _cardName = value;
                _spriteRenderer.sprite = _resourceManager.GetCardSpriteByCardName(_cardName);
            }
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
            ChildCards = new List<Card>();
            _cardManager = FindObjectOfType<CardManager>();
            _resourceManager = FindObjectOfType<ResourceManager>();
            CardName = CardName.None;
        }
        
        private void Update()
        {
            if (_isDragging)
            {
                _moveAmount = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                _moveAmount += _offset;
                transform.Translate(_moveAmount);
            }

            if (_childCard)
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
        
        public void SetMouseDown()
        {
            DetachParent();
            _isDragging = true;
            SetTrigger(true);
            UpdateOffset();
            MoveUp();
        }

        public void SetMouseUp()
        {
            _isDragging = false;
            AttachParent();
            SetTrigger(false);
        }
        
        public void DetachParent()
        {
            if (_parentCard)
            {
                _parentCard._childCard = null;
                _parentCard.SetIgnoreCollision(false);
                _parentCard.SetChildCards(null, new List<Card>());
                _parentCard.SetIgnoreCollision(true);
                _parentCard = null;
            }
        }

        private void AttachParent()
        {
            Card card = GetNearestOverlappedCard();
            if (card && !card._childCard)
            {
                _parentCard = card;
                card._childCard = this;
            }
            _overlappedCards.Clear();
            if (_parentCard)
            {
                _parentCard.SetChildCards(this, ChildCards);
                _parentCard.SetIgnoreCollision(true);
            }
        }
        
        private Card GetNearestOverlappedCard()
        {
            if (_overlappedCards.Count == 0) return null;
            
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

            return result;
        }

        private void SetTrigger(bool value)
        {
            _collider.isTrigger = value;
            if (_childCard)
                _childCard.SetTrigger(value);
        }

        private void SetIgnoreCollision(bool value)
        {
            foreach (Card card in ChildCards)
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

        private void MoveUp()
        {
            _spriteRenderer.sortingOrder = _cardManager.GetMaxSortingOrder() + 1;

            if (_childCard)
            {
                _childCard.MoveUp();
            }
        }

        private void SetChildCards(Card card, List<Card> cards)
        {
            ChildCards.Clear();
            if (card)
                ChildCards.Add(card);
            foreach (Card item in cards)
            {
                ChildCards.Add(item);
            }

            if (_parentCard)
                _parentCard.SetChildCards(this, ChildCards);
        }

        public int GetSortingOrder()
        {
            return _spriteRenderer.sortingOrder;
        }
    }
}