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
    [SerializeField] private float maxHealth = 100;
    private float health;
    [SerializeField] private AudioSource audioSource;
    public void SetlookDirection(bool faceRight)
    
    {
        
      transform.localScale = new Vector3(faceRight ? -1 : 1, 1, 1);
      
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
}