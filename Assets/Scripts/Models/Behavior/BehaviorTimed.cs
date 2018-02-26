﻿using UnityEngine;
using System.Collections;

public abstract class BehaviorTimed : Behavior
{
    private float mTimeout;
        : base(entity)
    {
        mTimeout = timeout;
    }

    public override bool IsDone()
    {
        return mTimer > mTimeout;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        mTimer += Time.deltaTime;
    }
}