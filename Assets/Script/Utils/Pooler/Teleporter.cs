using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Transform teleportDestination;

    [SerializeField]
    private float offset = 1f;

    [SerializeField]
    private bool top;
    [SerializeField]
    private bool bot;
    [SerializeField]
    private bool left;
    [SerializeField]
    private bool right;

    [Tooltip("Game Objects with this tag will be destroyed.")]
    [SerializeField] private List<string> tags;

    void Start()
    {
        if (bot || left)
            offset *= -1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tags.Contains(other.tag))
        {
            Vector3 otherPos = other.transform.position;

            if (left || right)
            {
                other.transform.position = new Vector3(teleportDestination.position.x + offset, otherPos.y, otherPos.z);
            }
            else
            {
                other.transform.position = new Vector3(otherPos.x, teleportDestination.position.y + offset, otherPos.z);
            }
        }
    }
}
