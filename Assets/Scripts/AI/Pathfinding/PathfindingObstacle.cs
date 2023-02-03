using System.Collections.Generic;
using UnityEngine;

namespace Remagures.AI
{
    public class PathfindingObstacle : MonoBehaviour
    {
        [SerializeField] private bool _right;
        [SerializeField] private bool _down;

        [Space]
        [SerializeField] private Vector2 _size;
        [SerializeField] private Vector2 _offset;

        [Space]
        [SerializeField] private Grid2D _grid;

        private readonly List<Node2D> _nodes = new();

        public void Start()
        {
            if (_grid == null)
                return;

            for (var x = (int)-(_size.x / 2); x < _size.x / 2; x++)
            {
                for (var y = (int)-(_size.y / 2); y < _size.y / 2; y++)
                {
                    var xId = _grid.NodeFromWorldPoint(transform.position + (Vector3)_offset).GridX + (_right ? -x : x);
                    var yId = _grid.NodeFromWorldPoint(transform.position + (Vector3)_offset).GridY + (_down ? y : -y);

                    _nodes.Add(_grid.Grid[xId, yId]);
                    _grid.Grid[xId, yId].SetObstacle(true);
                }
            }
        }
    
        public void OnDisable()
        {
            foreach (var node in _nodes)
                node.SetObstacle(false);
        }
    }
}
