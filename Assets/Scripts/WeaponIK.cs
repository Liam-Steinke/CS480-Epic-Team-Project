using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to help enemies aim their weapon at the player.
public class WeaponIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;
    public Transform bone;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPosition = targetTransform.position;
        AimAtTarget(bone, targetPosition);
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition) {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        
        bone.rotation = aimTowards * bone.rotation;
    }

    // Settter for target variable in case not set in editor
    public void SetTarget(Transform newTarget)
    {
        targetTransform = newTarget;
    }
}
