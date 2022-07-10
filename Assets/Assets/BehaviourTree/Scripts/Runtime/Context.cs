using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // The context is a shared object every node has access to.
    // Commonly used components and subsytems should be stored here
    // It will be somewhat specfic to your game exactly what to add here.
    // Feel free to extend this class 
    public class Context 
    {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody2D rigidbody;

        public EnemyWithTarget enemyWithTarget;
        public AreaEnemy areaEnemy;
        public MeleeEnemy meleeEnemy;
        public PatrollingLog patrollingLog;
        public TurretEnemy turretEnemy;

        public static Context CreateFromGameObject(GameObject gameObject) 
        {
            Context context = new Context();

            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.GetComponent<Animator>();
            context.rigidbody = gameObject.GetComponent<Rigidbody2D>();

            context.enemyWithTarget = gameObject.GetComponent<EnemyWithTarget>();
            context.areaEnemy = gameObject.GetComponent<AreaEnemy>();
            context.meleeEnemy = gameObject.GetComponent<MeleeEnemy>();
            context.patrollingLog = gameObject.GetComponent<PatrollingLog>();
            context.turretEnemy = gameObject.GetComponent<TurretEnemy>();

            return context;
        }
    }
}