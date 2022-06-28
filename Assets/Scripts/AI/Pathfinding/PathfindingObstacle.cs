using System.Collections.Generic;
using UnityEngine;

public class PathfindingObstacle : MonoBehaviour
{
    [SerializeField] private bool _right;
    [SerializeField] private bool _down;

    [Space]
    [SerializeField] private Vector2 _size;
    [SerializeField] private Vector2 _offset;

    [Space]
    [SerializeField] private Grid2D _grid;

    private List<Node2D> _nodes = new List<Node2D>();

    public void Start()
    {
        if (_grid == null)
            return;
        
        int xId = 0;
        int yId = 0;

        for (int x = (int)-(_size.x / 2); x < _size.x / 2; x++)
        {
            for (int y = (int)-(_size.y / 2); y < _size.y / 2; y++)
            {
                xId = _grid.NodeFromWorldPoint(transform.position + (Vector3)_offset).GridX + (_right ? -x : x);
                yId = _grid.NodeFromWorldPoint(transform.position + (Vector3)_offset).GridY + (_down ? y : -y);

                _nodes.Add(_grid.Grid[xId, yId]);
                _grid.Grid[xId, yId].SetObstacle(true);
            }
        }
    }
    
    public void OnDisable()
    {
        foreach (Node2D node in _nodes)
            node.SetObstacle(false);
    }
}
