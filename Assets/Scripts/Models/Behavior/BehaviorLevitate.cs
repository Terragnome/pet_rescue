﻿using UnityEngine;

    public override string GetDebugName()
    {
        return "Levitate";
    }
    {
        mRigidbody = mEntity.GetComponent<Rigidbody>();
    }
    {
        mRigidbody.AddForce(Vector3.up * -sLevitationForce, ForceMode.Impulse);
    }