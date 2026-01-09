using System;
using Unity.VisualScripting;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    [SerializeField] private Transform foot;
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private LayerMask groundLayers;

    private bool _isGrounded;
    private RaycastHit2D _previousHit;

    public event Action<bool, bool> OnGroundStateChanged;

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.CircleCast(foot.position, radius, Vector2.down, distance, groundLayers);
        bool newGroundState = hit.collider;

        _previousHit = hit;

        if (_isGrounded != newGroundState)
        {
            OnGroundStateChanged?.Invoke(_isGrounded, newGroundState);
            _isGrounded = newGroundState;
        }
    }

    public bool IsGrounded() => _isGrounded;

    public RaycastHit2D GetPreviousHit() => _previousHit;


    // -----------------------------//
    //        GIZMO VISUALIZER      //
    // -----------------------------//
    private void OnDrawGizmos()
    {
        if (foot == null)
            return;

        // Base colors
        Color castColor = _isGrounded ? Color.green : Color.yellow;
        Color hitColor = _isGrounded ? Color.green : Color.red;

        // Draw the starting circle
        Gizmos.color = castColor;
        Gizmos.DrawWireSphere(foot.position, radius);

        // Draw the cast direction line
        Vector3 endPoint = foot.position + Vector3.down * distance;
        Gizmos.DrawLine(foot.position, endPoint);

        // If we hit something, draw hit point + normal
        if (_previousHit.collider != null)
        {
            Gizmos.color = hitColor;

            // Hit point sphere
            Gizmos.DrawSphere(_previousHit.point, 0.05f);

            // Normal direction
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                _previousHit.point,
                _previousHit.point + _previousHit.normal * 0.3f
            );
        }
    }
}

