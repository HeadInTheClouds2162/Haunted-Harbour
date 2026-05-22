using System;
using UnityEngine;

public class Parallaxxxxx : MonoBehaviour
{
   [SerializeField] private Vector2 speed;
   private Vector3 InitialStartPosition;
   private Vector3 InitialCameraPosition;
   private Camera cam;
   
   
   private void Start()
   {
      
         cam = Camera.main;
         InitialStartPosition = transform.position;
         InitialCameraPosition = cam.transform.position;
      
         
   }

   private void LateUpdate()
   {
       Vector3 camDelta = cam.transform.position - InitialCameraPosition;
       Vector3 newPLocation = new Vector3(InitialStartPosition.x + camDelta.x * speed.x, InitialStartPosition.y + camDelta.y * speed.y);
       transform.position = newPLocation;
   }
   
   
   
}
