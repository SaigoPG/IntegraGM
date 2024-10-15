using UnityEngine;

public class InteractableObjectTest : InteractableObject
{
    [SerializeField] private Color[] rendererObjectColors;

    private Renderer rendererObject;
    private int currentColorPosition = 0;

    private void Start()
    {
        rendererObject = GetComponent<Renderer>();
        if (rendererObjectColors.Length > 0)
        {
            rendererObject.material.color = rendererObjectColors[0];
        }
    }

    public override void Interact()
    {
        soundEmitter.emitSound("InteractableTestSound");
        currentColorPosition = (currentColorPosition + 1) % rendererObjectColors.Length;
        rendererObject.material.color = rendererObjectColors[currentColorPosition];
    }

}
