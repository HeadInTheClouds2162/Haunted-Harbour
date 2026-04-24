using UnityEngine;

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private PlayerInput action;
    private IInputReceiver[] _receiver;
    [SerializeField] private AudioResource hurtSoundEffect;
    [SerializeField] private AudioResource deathSoundEffect;
    [SerializeField] private float _health = 100;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    public void SetlookDirection(bool faceRight)
    
    {
        
      transform.localScale = new Vector3((float)(faceRight ? -0.5 : 0.5), (float)0.5, (float)0.5);
      
    }
    
    private void Awake()
    {
        _receiver = GetComponentsInChildren < IInputReceiver >();
        foreach (IInputReceiver receiver in _receiver)
        {
            receiver.BindControls(action);
            
        }
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