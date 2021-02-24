using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematics : MonoBehaviour{
    public Transform target;
    public float length = 2f;

    public List<Transform> joints;
    public int iterations = 1;
    
    private Vector3 lastTargetPosition;

    private void Start(){
        lastTargetPosition = target.position;
    }

    private void FixedUpdate(){
        if ((target.position - lastTargetPosition).magnitude > 0.01f){
            PositionLimbs();
        }


        lastTargetPosition = target.position;
    }

    private void PositionLimbs(){
        if ((target.position - joints[joints.Count - 1].position).magnitude < 0.01f) return;

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
        int iterated = 0;

        while (iterated < iterations){
            Vector3 rootPos = joints[0].position;
            // FIRST STAGE
            //set end position at target
            joints[joints.Count - 1].position = target.position;
            //work backwards to the beginning
            for (int i = joints.Count - 2; i >= 0; i--){
                //find line between most recently updated point and next point BACKWARD
                Vector3 line = (joints[i + 1].position - joints[i].position).normalized;
                //move along line by length distance
                joints[i].position = joints[i + 1].position + (line * length);
            }

            // SECOND STAGE
            // Move P1 (root) to original position
            joints[0].position = rootPos;
            //work forward to the target
            for (int i = 1; i < joints.Count; i++){
                //find line between most recently updated point and next point FORWARD
                Vector3 line = (joints[i].position - joints[i - 1].position).normalized;
                //move along line by length distance
                joints[i].position = joints[i - 1].position + line * length;
            }

            iterated++;
        }
    }
}