using System;
using UnityEngine;

#pragma warning disable 0649

namespace PepixSignals
{
    /// <summary>
    /// Strongly typed messages with no parameters
    /// </summary>
    public abstract class ASignal : ABaseSignal<Action>
    {
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch()
        {
            DispatchInternal(callback => callback.handler());

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash);
                }
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash);
                    });
                }
            }
        }
        
        public CustomYieldInstruction Wait() => this;
        public static implicit operator CustomYieldInstruction(ASignal signal) => SignalYieldInstruction.Wait(signal);

    }

    /// <summary>
    /// Strongly typed messages with 1 parameter
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    public abstract class ASignal<T>: ABaseSignal<Action<T>>
    {   
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch(T arg1)
        {
            DispatchInternal(callback => callback.handler(arg1));

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash, arg1);
                } 
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash, arg1);
                    });
                }
            }
        }
        
        public CustomYieldInstruction Wait() => this;
        public static implicit operator CustomYieldInstruction(ASignal<T> signal) => SignalYieldInstruction.Wait(signal);

    }

    /// <summary>
    /// Strongly typed messages with 2 parameters
    /// </summary>
    /// <typeparam name="T">First parameter type</typeparam>
    /// <typeparam name="U">Second parameter type</typeparam>
    public abstract class ASignal<T, U>: ABaseSignal<Action<T, U>>
    {
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch(T arg1, U arg2)
        {
            DispatchInternal(callback => callback.handler(arg1, arg2));

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash, arg1, arg2);
                }
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash, arg1, arg2);
                    });
                }
            }
        }

        public CustomYieldInstruction Wait() => this;
        public static implicit operator CustomYieldInstruction(ASignal<T, U> signal) => SignalYieldInstruction.Wait(signal);

    }

    /// <summary>
    /// Strongly typed messages with 3 parameter
    /// </summary>
    /// <typeparam name="T">First parameter type</typeparam>
    /// <typeparam name="U">Second parameter type</typeparam>
    /// <typeparam name="V">Third parameter type</typeparam>
    public abstract class ASignal<T, U, V>: ABaseSignal<Action<T, U, V>>
    {
        /// <summary>
        /// Dispatch this signal
        /// </summary>
        public void Dispatch(T arg1, U arg2, V arg3)
        {
            DispatchInternal(callback => callback.handler(arg1, arg2, arg3));

            if (hub != null)
            {
                if (dispatchToParent && hub.parent != null)
                {
                    hub.parent.DispatchToHash(hash, arg1, arg2, arg3);
                }
                
                if (hub.propagateToSubs && hub.subs is {Count: > 0})
                {
                    hub.subs.ForEach(sub =>
                    {
                        sub.DispatchToHash(hash, arg1, arg2, arg3);
                    });
                }
            }
        }
        
        public CustomYieldInstruction Wait() => this;
        public static implicit operator CustomYieldInstruction(ASignal<T, U, V> signal) => SignalYieldInstruction.Wait(signal);
    }
}