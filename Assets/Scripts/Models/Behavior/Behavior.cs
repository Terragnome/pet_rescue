using System.Collections;using System.Collections.Generic;using UnityEngine;public abstract class Behavior{    protected Entity mEntity;    public Behavior(Entity entity)    {        mEntity = entity;    }

    #region Abstract Methods    public abstract string GetDebugName();    public abstract bool IsDone();
    #endregion
    #region Virtual Methods    // Only one movement behavior can be active at a time.    // The most recently requested movement behavior takes    // precedence.    public virtual bool IsMovement()
    {
        return false;
    }    public virtual void Start()    { }    public virtual void Stop()    { }	public virtual void Update()    { }    public virtual void FixedUpdate()    { }    #endregion}