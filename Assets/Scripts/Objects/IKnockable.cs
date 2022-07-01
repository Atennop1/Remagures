using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockable
{
    public LayerMask LayerMask { get; }
    public void Knock(Rigidbody2D rigidbody, float knockTime);
}
