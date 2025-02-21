using System;
using PepixSignals;
using UnityEngine;

namespace Components.Views
{
    public class VFXItem : MonoBehaviour
    {
        public TheSignal OnStopped = new();

        private void OnParticleSystemStopped()
        {
            OnStopped.Dispatch();
        }
    }
}