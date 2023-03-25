using UnityEngine;
using System;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime
{
    [AddComponentMenu("Behavior Designer/Variable Synchronizer")]
    public class VariableSynchronizer : MonoBehaviour
    {
        public enum SynchronizationType { BehaviorDesigner, Property, Animator, PlayMaker, uFrame };
        public enum AnimatorParameterType { Bool, Float, Integer }

        [Serializable]
        public class SynchronizedVariable
        {
            public SynchronizedVariable(SynchronizationType synchronizationType, bool setVariable, Behavior behavior, string variableName, bool global, Component targetComponent, string targetName, bool targetGlobal)
            {
                this.synchronizationType = synchronizationType;
                this.setVariable = setVariable;
                this.behavior = behavior;
                this.variableName = variableName;
                this.global = global;
                this.targetComponent = targetComponent;
                this.targetName = targetName;
                this.targetGlobal = targetGlobal;
            }

            public SynchronizationType synchronizationType;
            public bool setVariable;

            public Behavior behavior;
            public string variableName;
            public bool global;

            public Component targetComponent;
            public string targetName;
            public bool targetGlobal;

            // Behavior Designer
            public SharedVariable targetSharedVariable;

            // properties
            public Action<object> setDelegate;
            public Func<object> getDelegate;

            // animator
            public Animator animator;
            public AnimatorParameterType animatorParameterType; 
            public int targetID;

            // PlayMaker
            public Action<VariableSynchronizer.SynchronizedVariable> thirdPartyTick;
            public Enum variableType;
            public object thirdPartyVariable;

            public SharedVariable sharedVariable;
        }

        [SerializeField]
        private UpdateIntervalType updateInterval = BehaviorDesigner.Runtime.UpdateIntervalType.EveryFrame;
        public UpdateIntervalType UpdateInterval { get { return updateInterval; } set { updateInterval = value; UpdateIntervalChanged(); } }
        [SerializeField]
        private float updateIntervalSeconds = 0;
        public float UpdateIntervalSeconds { get { return updateIntervalSeconds; } set { updateIntervalSeconds = value; UpdateIntervalChanged(); } }
        private WaitForSeconds updateWait = null;

        [SerializeField]
        private List<SynchronizedVariable> synchronizedVariables = new List<SynchronizedVariable>();
        public List<SynchronizedVariable> SynchronizedVariables { get { return synchronizedVariables; } set { synchronizedVariables = value; enabled = true; } }

        private void UpdateIntervalChanged()
        {
            StopCoroutine("CoroutineUpdate");
            if (updateInterval == UpdateIntervalType.EveryFrame) {
                enabled = true;
            } else if (updateInterval == UpdateIntervalType.SpecifySeconds) {
                if (Application.isPlaying) {
                    updateWait = new WaitForSeconds(updateIntervalSeconds);
                    StartCoroutine("CoroutineUpdate");
                }
                enabled = false;
            } else { // manual
                enabled = false;
            }
        }

        public void Awake()
        {
            for (int i = synchronizedVariables.Count - 1; i > -1; --i) {
                var synchronizedVariable = synchronizedVariables[i];
                if (synchronizedVariable.global) {
                    synchronizedVariable.sharedVariable = GlobalVariables.Instance.GetVariable(synchronizedVariable.variableName);
                } else {
                    synchronizedVariable.sharedVariable = synchronizedVariable.behavior.GetVariable(synchronizedVariable.variableName);
                }
                string errorDescription = "";
                if (synchronizedVariable.sharedVariable == null) {
                    errorDescription = "the SharedVariable can't be found";
                } else {
                    switch (synchronizedVariable.synchronizationType) {
                        case SynchronizationType.BehaviorDesigner:
                            var targetBehavior = synchronizedVariable.targetComponent as Behavior;
                            if (targetBehavior == null) {
                                errorDescription = "the target component is not of type Behavior Tree";
                            } else {
                                if (synchronizedVariable.targetGlobal) {
                                    synchronizedVariable.targetSharedVariable = GlobalVariables.Instance.GetVariable(synchronizedVariable.targetName);
                                } else {
                                    synchronizedVariable.targetSharedVariable = targetBehavior.GetVariable(synchronizedVariable.targetName);
                                }
                                if (synchronizedVariable.targetSharedVariable == null) {
                                    errorDescription = "the target SharedVariable cannot be found";
                                }
                            }
                            break;
                        case SynchronizationType.Property:
                            var propertyInfo = synchronizedVariable.targetComponent.GetType().GetProperty(synchronizedVariable.targetName);
                            if (propertyInfo == null) {
                                errorDescription = "the property " + synchronizedVariable.targetName + " doesn't exist";
                            } else {
                                // Setting the variable means we need to get the property
                                if (synchronizedVariable.setVariable) {
                                    var getMethod = propertyInfo.GetGetMethod();
                                    if (getMethod == null) {
                                        errorDescription = "the property has no get method";
                                    } else {
                                        synchronizedVariable.getDelegate = CreateGetDelegate(synchronizedVariable.targetComponent, getMethod);
                                    }
                                } else {
                                    var setMethod = propertyInfo.GetSetMethod();
                                    if (setMethod == null) {
                                        errorDescription = "the property has no set method";
                                    } else {
                                        synchronizedVariable.setDelegate = CreateSetDelegate(synchronizedVariable.targetComponent, setMethod);
                                    }
                                }
                            }
                            break;
                        case SynchronizationType.Animator:
                            synchronizedVariable.animator = synchronizedVariable.targetComponent as Animator;
                            if (synchronizedVariable.animator == null) {
                                errorDescription = "the component is not of type Animator";
                            } else {
                                synchronizedVariable.targetID = Animator.StringToHash(synchronizedVariable.targetName);
                                var valueType = synchronizedVariable.sharedVariable.GetType().GetProperty("Value").PropertyType;
                                if (valueType.Equals(typeof(bool))) {
                                    synchronizedVariable.animatorParameterType = AnimatorParameterType.Bool;
                                } else if (valueType.Equals(typeof(float))) {
                                    synchronizedVariable.animatorParameterType = AnimatorParameterType.Float;
                                } else if (valueType.Equals(typeof(int))) {
                                    synchronizedVariable.animatorParameterType = AnimatorParameterType.Integer;
                                } else {
                                    errorDescription = "there is no animator parameter type that can synchronize with " + valueType;
                                }
                            }
                            break;
                        case SynchronizationType.PlayMaker:
                            var playMakerSynchronizationType = TaskUtility.GetTypeWithinAssembly("BehaviorDesigner.Runtime.VariableSynchronizer_PlayMaker");
                            if (playMakerSynchronizationType != null) {
                                var startMethod = playMakerSynchronizationType.GetMethod("Start");
                                if (startMethod != null) {
                                    var result = (int)startMethod.Invoke(null, new object[] { synchronizedVariable });
                                    if (result == 1) {
                                        errorDescription = "the PlayMaker NamedVariable cannot be found";
                                    } else if (result == 2) {
                                        errorDescription = "the Behavior Designer SharedVariable is not the same type as the PlayMaker NamedVariable";
                                    } else {
                                        var tickMethod = playMakerSynchronizationType.GetMethod("Tick");
                                        if (tickMethod != null) {
#if !UNITY_EDITOR && NETFX_CORE
                                            synchronizedVariable.thirdPartyTick = (Action<VariableSynchronizer.SynchronizedVariable>)tickMethod.CreateDelegate(typeof(Action<VariableSynchronizer.SynchronizedVariable>), null);
#else
                                            synchronizedVariable.thirdPartyTick = (Action<VariableSynchronizer.SynchronizedVariable>)Delegate.CreateDelegate(typeof(Action<VariableSynchronizer.SynchronizedVariable>), tickMethod);
#endif
                                        }
                                    }
                                }
                            } else {
                                errorDescription = "has the PlayMaker classes been imported?";
                            }
                            break;
                        case SynchronizationType.uFrame:
                            var uFrameSynchronizationType = TaskUtility.GetTypeWithinAssembly("BehaviorDesigner.Runtime.VariableSynchronizer_uFrame");
                            if (uFrameSynchronizationType != null) {
                                var startMethod = uFrameSynchronizationType.GetMethod("Start");
                                if (startMethod != null) {
                                    var result = (int)startMethod.Invoke(null, new object[] { synchronizedVariable });
                                    if (result == 1) {
                                        errorDescription = "the uFrame property cannot be found";
                                    } else if (result == 2) {
                                        errorDescription = "the Behavior Designer SharedVariable is not the same type as the uFrame property";
                                    } else {
                                        var tickMethod = uFrameSynchronizationType.GetMethod("Tick");
                                        if (tickMethod != null) {
#if !UNITY_EDITOR && NETFX_CORE
                                            synchronizedVariable.thirdPartyTick = (Action<VariableSynchronizer.SynchronizedVariable>)tickMethod.CreateDelegate(typeof(Action<VariableSynchronizer.SynchronizedVariable>), null);
#else
                                            synchronizedVariable.thirdPartyTick = (Action<VariableSynchronizer.SynchronizedVariable>)Delegate.CreateDelegate(typeof(Action<VariableSynchronizer.SynchronizedVariable>), tickMethod);
#endif
                                        }
                                    }
                                }
                            } else {
                                errorDescription = "has the uFrame classes been imported?";
                            }
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(errorDescription)) {
                    Debug.LogError(string.Format("Unable to synchronize {0}: {1}", synchronizedVariable.sharedVariable.Name, errorDescription));
                    synchronizedVariables.RemoveAt(i);
                }
            }

            if (synchronizedVariables.Count == 0) {
                enabled = false;
                return;
            }

            UpdateIntervalChanged();
        }

        public void Update()
        {
            Tick();
        }

        private IEnumerator CoroutineUpdate()
        {
            while (true) {
                Tick();
                yield return updateWait;
            }
        }

        public void Tick()
        {
            for (int i = 0; i < synchronizedVariables.Count; ++i) {
                var synchronizedVariable = synchronizedVariables[i];
                switch (synchronizedVariable.synchronizationType) {
                    case SynchronizationType.BehaviorDesigner:
                        if (synchronizedVariable.setVariable) {
                            synchronizedVariable.sharedVariable.SetValue(synchronizedVariable.targetSharedVariable.GetValue());
                        } else {
                            synchronizedVariable.targetSharedVariable.SetValue(synchronizedVariable.sharedVariable.GetValue());
                        }
                        break;
                    case SynchronizationType.Property:
                        // Setting the variable means we need to get the property
                        if (synchronizedVariable.setVariable) {
                            synchronizedVariable.sharedVariable.SetValue(synchronizedVariable.getDelegate());
                        } else {
                            synchronizedVariable.setDelegate(synchronizedVariable.sharedVariable.GetValue());
                        }
                        break;
                    case SynchronizationType.Animator:
                        if (synchronizedVariable.setVariable) {
                            switch(synchronizedVariable.animatorParameterType) {
                                case AnimatorParameterType.Bool:
                                    synchronizedVariable.sharedVariable.SetValue(synchronizedVariable.animator.GetBool(synchronizedVariable.targetID));
                                    break;
                                case AnimatorParameterType.Float:
                                    synchronizedVariable.sharedVariable.SetValue(synchronizedVariable.animator.GetFloat(synchronizedVariable.targetID));
                                    break;
                                case AnimatorParameterType.Integer:
                                    synchronizedVariable.sharedVariable.SetValue(synchronizedVariable.animator.GetInteger(synchronizedVariable.targetID));
                                    break;
                            }
                        } else {
                            switch (synchronizedVariable.animatorParameterType) {
                                case AnimatorParameterType.Bool:
                                    synchronizedVariable.animator.SetBool(synchronizedVariable.targetID, (bool)synchronizedVariable.sharedVariable.GetValue());
                                    break;
                                case AnimatorParameterType.Float:
                                    synchronizedVariable.animator.SetFloat(synchronizedVariable.targetID, (float)synchronizedVariable.sharedVariable.GetValue());
                                    break;
                                case AnimatorParameterType.Integer:
                                    synchronizedVariable.animator.SetInteger(synchronizedVariable.targetID, (int)synchronizedVariable.sharedVariable.GetValue());
                                    break;
                            }
                        }
                        break;
                    case SynchronizationType.PlayMaker:
                    case SynchronizationType.uFrame:
                        synchronizedVariable.thirdPartyTick(synchronizedVariable);
                        break;
                }
            }
        }

        private static Func<object> CreateGetDelegate(object instance, MethodInfo method)
        {
            var target = Expression.Constant(instance);
            var call = Expression.Call(target, method);
            return Expression.Lambda<Func<object>>(Expression.TypeAs(call, typeof(object))).Compile();
        }

        private static Action<object> CreateSetDelegate(object instance, MethodInfo method)
        {
            var target = Expression.Constant(instance);
            var parameter = Expression.Parameter(typeof(object), "p");
            var converted = Expression.Convert(parameter, method.GetParameters()[0].ParameterType);
            var call = Expression.Call(target, method, converted);
            return Expression.Lambda<Action<object>>(call, parameter).Compile();
        }
    }
}