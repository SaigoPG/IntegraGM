using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class InteractableObjectTest : InteractableObject
{
    [SerializeField] private Color[] rendererObjectColors;
    private Renderer rendererObject;
    private int currentColorPosition = 0;

    protected override void _Awake() { }

    protected override void _Start()
    {
        rendererObject = GetComponent<Renderer>();
        if (rendererObjectColors.Length > 0)
        {
            rendererObject.material.color = rendererObjectColors[0];
        }

    }

    protected override void _Update() { }

    protected override void _FixedUpdate() { }
    
    public override void Interact()
    {
        base.Interact();
        currentColorPosition = (currentColorPosition + 1) % rendererObjectColors.Length;
        rendererObject.material.color = rendererObjectColors[currentColorPosition];
    }

}
