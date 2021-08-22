using UnityEngine;
[RequireComponent(typeof(Collider))]
public class InteractableItem : MonoBehaviour
{
    public ObjectType type;
}

public enum ObjectType { Potion, Knife }