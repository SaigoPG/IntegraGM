using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePlatform : MonoBehaviour
{
    private MoveWithFloor playerFloor;

    [SerializeField] private float shakeForce;
    [SerializeField] private float shakeTime;
    [SerializeField] private float awaitToDestroyTime;

    private Vector3 originalPlatformPosition;
    private GameObject platform;
    private int shakes = 0;
    private void Awake()
    {
        platform = transform.GetChild(0).gameObject;
        originalPlatformPosition = platform.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        playerFloor = other.gameObject.GetComponent<MoveWithFloor>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (playerFloor.groudName == gameObject.name) platformShake();
        print(other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        playerFloor = null;
    }

    private void platformShake()
    {
        
        if (shakes < 4)
        {
            shakes++;
            LeanTween.moveLocal(platform, new Vector3(
            originalPlatformPosition.x + Random.Range(-shakeForce, shakeForce),
            originalPlatformPosition.y,
            originalPlatformPosition.z + Random.Range(-shakeForce, shakeForce)
        ), shakeTime)
        .setEase(LeanTweenType.easeShake)
        .setOnComplete(() => {
            platformShake();
        });
        }
        else
        {
            LeanTween.moveLocal(platform, new Vector3(
            originalPlatformPosition.x + Random.Range(-shakeForce, shakeForce),
            originalPlatformPosition.y,
            originalPlatformPosition.z + Random.Range(-shakeForce, shakeForce)
        ), shakeTime)
        .setEase(LeanTweenType.easeShake)
        .setOnComplete(() => {
            RestorePosition();
        });
        }
    }

    private void RestorePosition()
    {
        LeanTween.moveLocal(platform, originalPlatformPosition, 0.1f).setOnComplete(() => {
            StartCoroutine(AwaitToDestroy());
        });
    }

    private IEnumerator AwaitToDestroy()
    {
        yield return new WaitForSeconds(awaitToDestroyTime);
        destroyCollisionComponent();


    }

    private void destroyCollisionComponent()
    {
        Collider[] collisionComponents = GetComponents<Collider>();
        for (int i = 0; i < collisionComponents.Length; i++)
        {
            Destroy(collisionComponents[i]);
            collisionComponents[i] = null;
        }
    }
}
