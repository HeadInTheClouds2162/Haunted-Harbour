using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private PlayerInput action;
    private IInputReceiver[] _receiver;
    [SerializeField] private AudioResource hurtSoundEffect;
    [SerializeField] private AudioResource deathSoundEffect;
    [SerializeField] private float _health = 100;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private bool lookDirectionStartRight = true;
    [SerializeField] private Transform flipTransform;

    
    private bool isFacingright; 
    
    public void SetlookDirection(bool faceRight)
    {
        if (isFacingright == faceRight) return;
        isFacingright = faceRight;

        Vector3 vec = flipTransform.localScale;
        flipTransform.localScale = new Vector3((faceRight ^ lookDirectionStartRight) ? -Mathf.Abs(vec.x) : Mathf.Abs(vec.x), vec.y, vec.z);
        
    }

    private void Awake()
    {
        _receiver = GetComponentsInChildren < IInputReceiver >();
        foreach (IInputReceiver receiver in _receiver)
        {
            receiver.BindControls(action);
        }
        isFacingright = lookDirectionStartRight;
    }

    private void OnDrawGizmosSelected()
    {
        if (flipTransform != null) return;
        flipTransform = GetComponentInChildren<SpriteRenderer>().transform;
        if (flipTransform == null) return;
        Debug.LogWarning($"Auto assigned the flip transform to {flipTransform.name}", gameObject);
    }


    private void OnDestroy()
    {
        foreach (IInputReceiver receiver in _receiver) 
            receiver.UnbindControls(action);
    }


    public void TakeDamage(float damage, Vector2 direction, Vector2 position)
    {
        audioSource.resource = hurtSoundEffect;
        audioSource.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            _health -= 10;
            audioSource.resource = hurtSoundEffect;
            audioSource.Play();

        }
    }

}