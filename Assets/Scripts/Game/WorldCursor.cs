using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldCursor : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private bool showMouse;
    [SerializeField] private CursorLockMode lockMouse = CursorLockMode.Confined;
    
    private void Awake()
    {
        Cursor.visible = showMouse;
        Cursor.lockState = lockMouse;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!_camera)
        {
            _camera = Camera.main;
            if (!_camera) return;
        }
        
        Vector3 mousePos = Pointer.current.position.ReadValue();

        mousePos.z = -_camera.transform.position.z;
        transform.position = _camera.ScreenToWorldPoint(mousePos);

        transform.position = _camera!.ScreenToWorldPoint(mousePos);
    }
}
