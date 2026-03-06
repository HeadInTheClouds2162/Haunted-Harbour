using UnityEngine;

using UnityEngine;

public class SeaHag : Enemy
{
    public Transform player;

    protected override void Move()
    {
        if (player == null) return;
        Vector2 toSeahag = (transform.position - player.position).normalized;
        Vector2 playerForward = new Vector2(player.localScale.x, 0).normalized;
        float dot = Vector2.Dot(playerForward, toSeahag);

        if (dot > 0f)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 moveDir = (player.position - transform.position).normalized;
        rigidbody2D.AddForce(moveDir * speed, ForceMode2D.Force);
        if (rigidbody2D.linearVelocity.magnitude > maxSpeed)
        {
            rigidbody2D.linearVelocity = rigidbody2D.linearVelocity.normalized * maxSpeed;
        }
    }
}
