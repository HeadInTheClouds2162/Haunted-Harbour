using UnityEngine;
using UnityEngine.InputSystem;

public class Lillian_InteractionController : MonoBehaviour
{
    [Header("Cast Settings")]
    [SerializeField] private float distance = 5f;
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private LayerMask deletableLayers;
    [SerializeField] private Transform hand;

    [Header("Debug")]
    [SerializeField] private bool castFire;
    [SerializeField] private bool castOver = true;

    private float timer;
    private const float castDuration = 0.1f;

    private Vector2 castStart;
    private Vector2 castEnd;

    private void Start()
    {
        BindControls(GetComponent<PlayerInput>());
    }

    private void Update()
    {
        if (!castFire) return;

        timer += Time.deltaTime;

        if (timer >= castDuration)
        {
            castFire = false;
            castOver = true;
        }
    }

    public void BindControls(PlayerInput reference)
    {
        reference.actions["Interact"].performed += TryInteract;
    }

    public void UnbindControls(PlayerInput reference)
    {
        reference.actions["Interact"].performed -= TryInteract;
    }

    private void TryInteract(InputAction.CallbackContext _)
    {
        if (!CanInteract() || hand == null)
            return;

        DeleteStuff();

        timer = 0f;
        castFire = true;
        castOver = false;

        castStart = hand.position;
        castEnd = castStart + (Vector2)hand.right * distance;
    }

    private bool CanInteract()
    {
        return true; // put cooldown / stamina checks here later
    }

    private void DeleteStuff()
    {
        Vector2 origin = hand.position;
        Vector2 direction = hand.right;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            origin,
            radius,
            direction,
            distance,
            deletableLayers
        );

        foreach (var hit in hits)
        {
            if (!hit.collider) continue;

            Destroy(hit.collider.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        if (hand == null) return;

        Vector3 origin = hand.position;
        Vector3 end = origin + hand.right * distance;

        if (castFire)
            Gizmos.color = Color.red;
        else if (castOver)
            return;

        Gizmos.DrawWireSphere(origin, radius);
        Gizmos.DrawWireSphere(end, radius);
        Gizmos.DrawLine(origin, end);
    }
}