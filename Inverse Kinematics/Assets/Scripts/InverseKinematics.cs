using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour{
    public Transform target;
    public float length = 2f;
    public List<Transform> joints;
    
    private void FixedUpdate(){
        PositionLimbs();
    }

    private void PositionLimbs(){
        float totalLength = length * joints.Count - 1;
        float distanceToTarget = (target.position - joints[0].position).magnitude;
        if (distanceToTarget >= totalLength){
            UnreachableTargetPosition();
        }
        else{
            ReachableTargetPosition();
        }
    }

    // Fully elongates the limb system if the target is out of reach
    private void UnreachableTargetPosition(){
        
            Vector3 targetDirection = (target.position - joints[0].position).normalized;
            Transform root = joints[0];
            for (int i = 1; i < joints.Count; i++){
                joints[i].position = root.position + targetDirection * (length * i);
            }
    }

    // Positions limbs is target is in reach
    private void ReachableTargetPosition(){
        // first stage
        
        // set end position at target
        
        // work backwards to the beginning
        // find line between most recently updated point and next point backwards
        
        
        // second stage
    }

}