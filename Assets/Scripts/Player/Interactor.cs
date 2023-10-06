using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider2D[] _colliders = new Collider2D[3];
    [SerializeField] private int _numFound;
    private bool previousWasTouching = false;

    private void Update()
    {
        _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null)
            {
                bool isTouching = Input.touchCount > 0;

                if ((isTouching && !previousWasTouching) || Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame) interactable.Interact(this);
                previousWasTouching = isTouching;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}