using UnityEngine;

namespace PepixSignals
{
    public class SignalYieldInstruction
    {
        public static CustomYieldInstruction Wait(ASignal signal)
        {
            var dispatched = false;
            signal.On(() => dispatched = true).OffWhen(() => dispatched);
            return new WaitUntil(() => dispatched);
        }
        
        public static CustomYieldInstruction Wait<T>(ASignal<T> signal)
        {
            var dispatched = false;
            signal.On(_ => dispatched = true).OffWhen(() => dispatched);
            return new WaitUntil(() => dispatched);
        }
        
        public static CustomYieldInstruction Wait<T, U>(ASignal<T, U> signal)
        {
            var dispatched = false;
            signal.On((_, _) => dispatched = true).OffWhen(() => dispatched);
            return new WaitUntil(() => dispatched);
        }
        
        public static CustomYieldInstruction Wait<T, U, V>(ASignal<T, U, V> signal)
        {
            var dispatched = false;
            signal.On((_, _, _) => dispatched = true).OffWhen(() => dispatched);
            return new WaitUntil(() => dispatched);
        }
    }
}