using UnityEngine;

[RequireComponent(typeof(CodeAudioEmitter))]
public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private string interactSoundName;
    protected CodeAudioEmitter soundEmitter;
    [HideInInspector] public bool active { get; protected set; } = true;

    private void Awake()
    {
        soundEmitter = GetComponent<CodeAudioEmitter>();
        _Awake();
    }

    private void Start()
    {
        _Start();
    }

    private void Update()
    {
        _Update();
    }

    private void FixedUpdate()
    {
        _FixedUpdate();
    }

    protected abstract void _Awake();
    protected abstract void _Start();
    protected abstract void _Update();
    protected abstract void _FixedUpdate();

    public virtual void Interact()
    {
        if (!active) return;
        if (soundEmitter != null) soundEmitter.emitSound(interactSoundName);
    }
}
