using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class Mirror : MonoBehaviour {

    public VRIK ik;
    public Transform mirroredAvatarRoot;
    private Transform[] children;
    private Transform[] targetChildren;

    private void Start()
    {
        children = mirroredAvatarRoot.GetComponentsInChildren<Transform>();
        targetChildren = ik.transform.GetComponentsInChildren<Transform>();

        mirroredAvatarRoot.localScale = new Vector3(-1f, 1f, 1f);

        ik.solver.OnPostUpdate += OnPostUpdate;
    }

    void OnPostUpdate()
    {
        // Mirror root position
        Vector3 mPos = transform.InverseTransformPoint(ik.transform.position);
        mPos.z = -mPos.z;
        mirroredAvatarRoot.position = transform.TransformPoint(mPos);

        // Mirror root rotation
        Quaternion mRot = Quaternion.Inverse(transform.rotation) * ik.transform.rotation;
        Vector3 mForward = mRot * Vector3.forward;
        Vector3 mUp = mRot * Vector3.up;
        mForward.z = -mForward.z;
        mUp.z = -mUp.z;
        mirroredAvatarRoot.rotation = transform.rotation * Quaternion.LookRotation(mForward, mUp);

        // Match pose (actual mirroring of the pose is done by mirroredAvatarRoot.localScale = new Vector3(-1f, 1f, 1f));
        for (int i = 1; i < children.Length; i++)
        {
            children[i].localPosition = targetChildren[i].localPosition;
            children[i].localRotation = targetChildren[i].localRotation;
        }
    }
}
