using UnityEngine;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private PlayerInput action;
    private IInputReceiver[] _receiver;
    
    [SerializeField] private AudioResource hurtSoundEffect;
    [SerializeField] private AudioResource deathSoundEffect;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float maxHealth = 100;
    private float health;

    private void Awake()
    {
        _receiver = GetComponentsInChildren<IInputReceiver>();
        foreach (IInputReceiver receiver in _receiver)
        {
            receiver.BindControls(action);
        }

        health = maxHealth;
    }

    private void OnDestroy()
    {
        foreach (IInputReceiver receiver in _receiver)
            receiver.UnbindControls(action);
    }

    public void SetLookDirection(bool faceRight)
    {
        transform.localScale = new Vector3(faceRight ? -1 : 1, 1, 1);
    }

    public void TakeDamage(float damage, Vector2 direction, Vector2 position)
    {
        audioSource.resource = hurtSoundEffect;
        audioSource.Play();
    }
}