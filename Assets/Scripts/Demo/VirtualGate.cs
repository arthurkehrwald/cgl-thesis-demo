using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VirtualGate : MonoBehaviour
{
    public float Openness
    {
        get => _openness;
        set
        {
            _openness = Mathf.Clamp01(value);
            currentRotation = Quaternion.Slerp(closedRotation, openRotation, _openness);
        }
    }
    private float _openness = 0f;

    [SerializeField]
    private GateTriggerZone triggerZone;
    private new Rigidbody rigidbody;

    [SerializeField]
    private float openTimeSeconds = 3f;
    [SerializeField]
    private float closeTimeSeconds = 6f;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Quaternion currentRotation;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        closedRotation = transform.rotation;
        openRotation = Quaternion.AngleAxis(-90f, transform.up);
        currentRotation = closedRotation;
    }

    private void FixedUpdate()
    {
        if (triggerZone.IsBodyInZone)
        {
            if (Openness < 1f)
                Openness += 1 / openTimeSeconds * Time.fixedDeltaTime;
        }
        else
        {
            if (Openness > 0f)
                Openness -= 1 / closeTimeSeconds * Time.fixedDeltaTime;
        }
        rigidbody.rotation = currentRotation;
    }
}
