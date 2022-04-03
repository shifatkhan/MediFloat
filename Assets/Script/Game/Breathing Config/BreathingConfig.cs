using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BreathConfig", menuName = "ScriptableObjects/BreathingConfig")]
public class BreathingConfig : ScriptableObject
{
    public string Name = "Default 4-7-8";
    public float InTime = 4;
    public float Hold = 7;
    public float OutTime = 8;
}
