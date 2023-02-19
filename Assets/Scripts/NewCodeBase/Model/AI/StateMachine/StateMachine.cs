using System;
using System.Collections.Generic;
using System.Linq;

namespace Remagures.Model.AI.StateMachine
{
    public class StateMachine
    {
        private IState _currentState;
   
        private readonly Dictionary<Type, List<Transition>> _transitions = new();
        private readonly List<Transition> _universalTransitions = new();

        private readonly List<Transition> _emptyTransitions = new(0);
        private List<Transition> _currentTransitions = new();

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
                SetState(transition.To);
      
            _currentState?.Update();
        }

        public void SetState(IState state)
        {
            if (IsStateBannedInCurrentContext(state))
                throw new ArgumentException( $"Can't set {state} because it's banned in the given context");
            
            if (IsStateAlreadySet(state))
                throw new ArgumentException( $"Can't set {state} because it's already set");

            _currentState?.OnExit();
            _currentState = state;

            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            _currentTransitions ??= _emptyTransitions;
      
            _currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }
      
            transitions.Add(new Transition(to, predicate, null));
        }
        
        public void AddUniversalTransition(IState to, Func<bool> predicate)
            => _universalTransitions.Add(new Transition(to, predicate, null));

        public void AddExceptionsToTransition(IState from, params IState[] exceptions)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            var exceptionsTypes = exceptions.Select(exception => exception.GetType()).ToList();
            transitions.Add(new Transition(null, null, exceptionsTypes));
        }

        public void AddExceptionToUniversalTransition(IState to, params IState[] exceptions)
        {
            var exceptionsTypes = exceptions.Select(exception => exception.GetType()).ToList();
            _universalTransitions.Add(new Transition(to, null, exceptionsTypes));
        }
        
        public bool IsStateBannedInCurrentContext(IState state)
        {
            if (_currentState == null) return false;
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            return _currentTransitions != null && _currentTransitions.Any(transition => transition.Exceptions != null && transition.Exceptions.Contains(state.GetType()));
        }

        public bool IsStateAlreadySet(IState state) 
            => state == _currentState;

        private Transition GetTransition()
        {
            foreach (var transition in _universalTransitions.Where(transition => transition.Condition()))
                if (_currentState != transition.To)
                    return transition;

            return _currentTransitions?.FirstOrDefault(transition => transition.Condition());
        }
    }
}
