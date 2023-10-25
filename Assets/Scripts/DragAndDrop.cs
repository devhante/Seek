using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging;
    private Vector2 _moveAmount;
    private Vector2 _offset;
    private Vector2 _originPosition;

    public void OnMouseDown()
    {
        _originPosition = transform.position;
        _isDragging = true;
        UpdateOffset();
    }

    public void OnMouseUp()
    {
        _isDragging = false;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Card"))
        {
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Card"))
        {
            
        }
    }

    private void UpdateOffset()
    {
        _offset = transform.position - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
}
