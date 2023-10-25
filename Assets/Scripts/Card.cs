using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private Transform childCardPosition;
    
    private Camera _mainCamera;
    private SpriteRenderer _spriteRenderer;
    private bool _isDragging;
    private Vector2 _moveAmount;
    private Vector2 _offset;
    private Vector2 _originPosition;
    private List<Card> _overlappedCards;
    private Card _parentCard;
    private Card _childCard;

    public void OnMouseDown()
    {
        transform.SetAsFirstSibling();
        if (_parentCard != null)
        {
            _parentCard._childCard = null;
            _parentCard = null;
        }
        _originPosition = transform.position;
        _isDragging = true;
        UpdateOffset();
        MoveUp();
    }

    public void OnMouseUp()
    {
        _isDragging = false;
        if (_overlappedCards.Count > 0)
        {
            Card result = _overlappedCards[0];
            float shortest = Vector3.Distance(transform.position, _overlappedCards[0].transform.position);
            foreach (Card card in _overlappedCards)
            {
                float distance = Vector3.Distance(transform.position, card.transform.position);
                if (distance < shortest)
                {
                    result = card;
                }
            }
            _parentCard = result;
            result._childCard = this;
        }
        _overlappedCards.Clear();
        if (_parentCard != null)
        {
            _overlappedCards.Add(_parentCard);
        }
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isDragging = false;
        _offset = Vector2.zero;
        _overlappedCards = new List<Card>();
        _childCard = null;
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
}