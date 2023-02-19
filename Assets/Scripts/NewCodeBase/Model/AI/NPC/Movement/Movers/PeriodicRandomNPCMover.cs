using Remagures.Root;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public class PeriodicRandomNPCMover : IRandomNPCMover, IUpdatable //TODO move this to walking state
    {
        [Header("Times")]
        [SerializeField] private float _minMoveTime;
        [SerializeField] private float _minWaitTime;
        [SerializeField] private float _maxMoveTime;
        [SerializeField] private float _maxWaitTime;

        private float _moveTimeSeconds;
        private float _waitTimeSeconds;
        
        public Vector2 MoveDirection => _randomNpcMover.MoveDirection;
        private readonly IRandomNPCMover _randomNpcMover;
        
        public void Move()
            => _randomNpcMover.Move();

        public void StopMove()
            => _randomNpcMover.StopMove();

        public void Update()
        {
            if (CurrentState is not (NPCState.Talk or NPCState.Wait))
            {
                if (CurrentState == NPCState.Walk)
                {
                    _moveTimeSeconds -= UnityEngine.Time.deltaTime;

                    if (_moveTimeSeconds <= 0)
                    {
                        _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
                    }
                    else if (!PlayerInRange)
                    {
                        Move();
                    }
                }
                else
                {
                    _waitTimeSeconds -= UnityEngine.Time.deltaTime;
                    if (!(_waitTimeSeconds <= 0)) 
                        return;

                    ChooseDifferentDirection();
                    _waitTimeSeconds = Random.Range(_minWaitTime, _maxWaitTime);
                }
            }
        }

        public void ChooseDifferentDirection()
        {
            throw new System.NotImplementedException();
        }
    }
}