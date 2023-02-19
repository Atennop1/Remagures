using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Remagures.Model.AI.Pathfinding
{
    public sealed class GridObstaclesApplier
    {
        private readonly Tilemap[] _obstacleMaps;
        private readonly GridData _gridData;

        public GridObstaclesApplier(Tilemap[] obstacleMaps, GridData gridData)
        {
            _obstacleMaps = obstacleMaps ?? throw new ArgumentNullException(nameof(obstacleMaps));
            _gridData = gridData;
        }

        public List<Vector2> Apply(GridNode[,] nodes)
        {
            var coordinates = new List<Vector2>();

            for (var x = 0; x < _gridData.SizeX; x++)
            {
                for (var y = 0; y < _gridData.SizeY; y++)
                {
                    foreach (var map in _obstacleMaps)
                    {
                        if (map.HasTile(map.WorldToCell(nodes[x, y].WorldPosition)))
                            coordinates.Add(new Vector2(x, y));
                    }
                }
            }

            return coordinates;
        }
    }
}