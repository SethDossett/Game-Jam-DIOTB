using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Event/General")]
public class GeneralEventSO : ScriptableObject
{
    public UnityAction OnRaiseEvent;

    public void RaiseEvent() => OnRaiseEvent?.Invoke();
}
