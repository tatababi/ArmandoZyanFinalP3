using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public string objectName = "Object";
    [TextArea(3, 10)]
    public string[] dialogueLines; 

    public bool isPickupItem = false; 
    public UnityEvent OnPickup;

    public bool  isNPC = false;
    public UnityEvent OnNPCInteract;
}
   