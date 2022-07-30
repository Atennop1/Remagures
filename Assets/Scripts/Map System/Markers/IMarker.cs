using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarker
{
    public IMarkerVisitor Visitor { get; }

    public void Init(MapView view);
    public void SetupComponents();

    public bool ContainsInMap(Map map);
    public bool ContainsInMapTree(Map map);

    public bool TryDisplay(Transform parent, Vector3 scale, Map map);
}
