using System;
using UnityEngine;

public class GroundController : MonoBehaviour
{
   [SerializeField] private LayerMask groundLayer;
   [SerializeField] private Transform foot;
   [SerializeField] private float radius = 0.1f;
   [SerializeField] private float distance = 0.3f;
   private bool _isGrounded;
   RaycastHit2D _previousHit;
   public event Action<bool, bool> OnGroundStateChanged;
   
   private void FixedUpdate()
   {
       CheckGround();
   }

   private void CheckGround()
   {
       RaycastHit2D hit = Physics2D.CircleCast(foot.position, radius, Vector2.down, distance, groundLayer);
       bool newGroundState = hit.collider;
       _previousHit = hit;
       if (_isGrounded != newGroundState)
       {
           OnGroundStateChanged?.Invoke(_isGrounded, newGroundState);
           _isGrounded = newGroundState;
       }
   }

   public bool IsGrounded()
   {
       return _isGrounded;
   }

   public RaycastHit2D GetPreviousHit()
   {
       return _previousHit;
   }

   private void OnDrawGizmos()
   {
       if (foot == null) return;

       Vector3 startPos = foot.position;
       Vector3 endPos = startPos + Vector3.down * distance;

       // Perform preview cast with the same layer mask
       RaycastHit2D previewHit = Physics2D.CircleCast(startPos, radius, Vector2.down, distance, groundLayer);

       // Draw the circle sweep visualization
       DrawCircleSweep(startPos, endPos, radius, previewHit.collider != null);

       // Draw hit information if we hit something
       if (previewHit.collider != null)
       {
           // Hit point
           Gizmos.color = Color.green;
           Gizmos.DrawSphere(previewHit.point, 0.08f);

           // Draw circle at hit position
           Gizmos.color = Color.yellow;
           DrawCircle(previewHit.point, radius, Vector3.forward);

           // Normal line
           Gizmos.color = Color.blue;
           Gizmos.DrawLine(previewHit.point, previewHit.point + previewHit.normal * 0.5f);
           
           // Draw arrow head for normal
           DrawArrowHead(previewHit.point + previewHit.normal * 0.5f, previewHit.normal, 0.1f);
       }
       else
       {
           // Show miss indicator at end
           Gizmos.color = Color.red;
           Gizmos.DrawWireSphere(endPos, 0.08f);
       }

       // Draw grounded state indicator
       Gizmos.color = _isGrounded ? Color.green : Color.red;
       Gizmos.DrawWireCube(startPos + Vector3.right * (radius + 0.15f), Vector3.one * 0.1f);
   }

   // Helper method to draw the circle sweep
   private void DrawCircleSweep(Vector3 start, Vector3 end, float circleRadius, bool hit)
   {
       // Draw start circle
       Gizmos.color = hit ? Color.green : Color.yellow;
       DrawCircle(start, circleRadius, Vector3.forward);

       // Draw end circle
       Gizmos.color = hit ? Color.green : Color.red;
       DrawCircle(end, circleRadius, Vector3.forward);

       // Draw connecting lines
       Gizmos.color = hit ? new Color(0, 1, 0, 0.3f) : new Color(1, 1, 0, 0.3f);
       Gizmos.DrawLine(start + Vector3.right * circleRadius, end + Vector3.right * circleRadius);
       Gizmos.DrawLine(start + Vector3.left * circleRadius, end + Vector3.left * circleRadius);
   }

   // Helper method to draw a circle in 3D space
   private void DrawCircle(Vector3 center, float circleRadius, Vector3 normal, int segments = 20)
   {
       Vector3 forward = Vector3.Slerp(Vector3.up, Vector3.right, 0.5f);
       if (Vector3.Dot(normal, forward) > 0.9f)
           forward = Vector3.forward;

       Vector3 right = Vector3.Cross(normal, forward).normalized;
       forward = Vector3.Cross(right, normal).normalized;

       Vector3 prevPoint = center + right * circleRadius;
       for (int i = 1; i <= segments; i++)
       {
           float angle = (float)i / segments * Mathf.PI * 2f;
           Vector3 newPoint = center + (right * Mathf.Cos(angle) + forward * Mathf.Sin(angle)) * circleRadius;
           Gizmos.DrawLine(prevPoint, newPoint);
           prevPoint = newPoint;
       }
   }

   // Helper to draw arrow head for normal visualization
   private void DrawArrowHead(Vector3 tip, Vector3 direction, float size)
   {
       Vector3 right = Vector3.Cross(direction, Vector3.forward).normalized;
       if (right.magnitude < 0.1f)
           right = Vector3.Cross(direction, Vector3.up).normalized;

       Vector3 back = -direction.normalized * size;
       Gizmos.DrawLine(tip, tip + back + right * size * 0.5f);
       Gizmos.DrawLine(tip, tip + back - right * size * 0.5f);
   }
}