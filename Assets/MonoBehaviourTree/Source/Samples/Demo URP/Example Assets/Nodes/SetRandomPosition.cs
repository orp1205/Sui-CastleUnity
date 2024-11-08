﻿using MBT;
using UnityEngine;

namespace MBTExample
{
    [AddComponentMenu("")]
    [MBTNode("Example/Set Random Position", 500)]
    public class SetRandomPosition : Leaf
    {
        public Bounds bounds;
        public Vector3Reference blackboardVariable = new Vector3Reference(VarRefMode.DisableConstant);
        public TransformReference centerReference = new TransformReference(VarRefMode.DisableConstant);

        public override NodeResult Execute()
        {
            bounds.center = centerReference.Value.position;
            // Random values per component inside bounds
            blackboardVariable.Value = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
            return NodeResult.success;
        }
    }
}
