using Remagures.AI.NPCs.Components;
using UnityEngine;

namespace Remagures.AI.NPCs
{
    public class RandomMovableBoundedNPC : MovableNPC
    {
        [Header("Times")]
        [SerializeField] private float _minMoveTime;
        [SerializeField] private float _minWaitTime;
        [SerializeField] private float _maxMoveTime;
        [SerializeField] private float _maxWaitTime;

        [Header("Bounds")]
        [SerializeField] private Collider2D _bounds;

        private float _moveTimeSeconds;
        private float _waitTimeSeconds;
        private Vector3 _directionVector;
    
        private readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");
    
        public override void Start()
        {
            base.Start();
            _waitTimeSeconds = Random.Range(_minWaitTime, _maxWaitTime);
            _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
            ChangeDirection();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            ChooseDifferentDirection();
        }

        protected override void TriggerEnter()
        {
            base.TriggerEnter();
            ChooseDifferentDirection();
        
            var temp = Vector3.MoveTowards(transform.position, Player.position, Speed * UnityEngine.Time.deltaTime);
            _directionVector = temp - transform.position;
            Animations.UpdateAnim(_directionVector);
        }
    
        private void FixedUpdate()
        {
            if (CurrentState is not (NPCState.Talk or NPCState.Wait))
            {
                if (CurrentState == NPCState.Walk)
                {
                    Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
                    _moveTimeSeconds -= UnityEngine.Time.deltaTime;
                    if (_moveTimeSeconds <= 0)
                    {
                        _moveTimeSeconds = Random.Range(_minMoveTime, _maxMoveTime);
                        ChangeState(NPCState.Stay);
                    }
                    else if (!PlayerInRange)
                    {
                        Move();
                    }
                }
                else
                {
                    Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, true);
                    _waitTimeSeconds -= UnityEngine.Time.deltaTime;
                    if (!(_waitTimeSeconds <= 0)) return;
                
                    ChooseDifferentDirection();
                    _waitTimeSeconds = Random.Range(_minWaitTime, _maxWaitTime);
                    ChangeState(NPCState.Walk);
                }
            }
            else
            {
                var temp = Vector3.MoveTowards(transform.position, Player.position, Speed * UnityEngine.Time.deltaTime);
                _directionVector = temp - transform.position;
                Animations.UpdateAnim(_directionVector);
            }
        }

        private void Move()
        {
            var movePoint = transform.position + Speed * UnityEngine.Time.deltaTime * _directionVector;
            if (_bounds.bounds.Contains(movePoint) && Physics2D.Raycast(transform.position, _directionVector, 2f))
                Rigidbody.MovePosition(movePoint);
            else ChooseDifferentDirection();
        }

        private void ChooseDifferentDirection()
        {
            var previousDirectionVector = _directionVector;
        
            while (previousDirectionVector == _directionVector)
                ChangeDirection();
        }

        private void ChangeDirection()
        {
            var direction = Random.Range(0, 4);
            _directionVector = direction switch
            {
                0 => Vector3.right,
                1 => Vector3.left,
                2 => Vector3.up,
                3 => Vector3.down,
                _ => _directionVector
            };
            Animations.UpdateAnim(_directionVector);
        }
    }
}
