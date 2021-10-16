using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Handles player input to make the feather go up and down.
/// </summary>
public class FeatherMovement : MonoBehaviour
{
    [SerializeField] private float _force;

    private void Awake()
    {

    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            // Holding press.
            Debug.Log("HOLDING");


        }
    }

    private void FixedUpdate()
    {
        
    }
}
