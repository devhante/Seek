using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Revenged.Player
{
    
}
public class DragAndDrop : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector2 _moveAmount;
    private Vector2 _offset;

    private BoxCollider2D _boxCollider2D;
    private ContactFilter2D _contactFilter2D;
    private Collider2D[] _overlappedColliders;

    public void OnMouseDown()
    {
        _isDragging = true;
        _offset = transform.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseUp()
    {
        _isDragging = false;
    }

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _contactFilter2D = new ContactFilter2D();
        _overlappedColliders = new Collider2D[10];
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        _isDragging = false;
        _offset = Vector2.zero;
    }

    private void Update()
    {
        if (_isDragging)
        {
            _moveAmount = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            _moveAmount += _offset;
            transform.Translate(_moveAmount);

            if (_boxCollider2D.OverlapCollider(_contactFilter2D, _overlappedColliders) > 0)
            {
                foreach (var coll in _overlappedColliders)
                {
                    Debug.Log(coll);
                }
            }
        }
    }
}
