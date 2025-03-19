using UnityEngine;
using Unity.Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private CinemachineImpulseSource impulseSource; 
    void Start()
    {
        instance = this;
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        
    }

    public void CameraShakeShow()
    {
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
