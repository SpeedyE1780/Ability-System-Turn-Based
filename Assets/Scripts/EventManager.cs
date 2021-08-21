using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void UpdateStamina(int stamina);
    public static UpdateStamina updateStamina;

    public delegate void UpdateQueueTab(bool empty);
    public static UpdateQueueTab updateQueue;
}