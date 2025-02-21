using Components.Views;
using Extensions;
using PepixSignals;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class CannonAimController : ITickable
    {
        [Inject] private CannonView _view;
        [Inject] private CannonAimSettings _aimSettings;

        public readonly TheSignal OnDirectionChanged = new();
        
        private float _horizontalMovement;
        private float _verticalMovement;
        private bool _isMoved;

        public void Tick()
        {
            if (Input.GetKey(KeyCode.A))
                _horizontalMovement = -1;
            else if (Input.GetKey(KeyCode.D))
                _horizontalMovement = 1;
            
            if (Input.GetKey(KeyCode.W))
                _verticalMovement = -1;
            else if (Input.GetKey(KeyCode.S))
                _verticalMovement = 1;

            ProceedHorizontalMovement();
            ProceedVerticalMovement();
            CheckIfMoved();
        }

        private void CheckIfMoved()
        {
            if (_isMoved)
            {
                OnDirectionChanged.Dispatch();
            }
            _isMoved = false;
        }

        private void ProceedHorizontalMovement()
        {
            if (_horizontalMovement == 0) return;
            var newRotationHorizontal = _view.Foundation.localEulerAngles.y +
                               _horizontalMovement * _aimSettings.HorizontalSpeed * Time.deltaTime;
            if (newRotationHorizontal > 180)
                newRotationHorizontal -= 360;
            newRotationHorizontal = Mathf.Clamp(newRotationHorizontal, _aimSettings.HorizontalRotationBorders.x,
                _aimSettings.HorizontalRotationBorders.y);
                
            _view.Foundation.localEulerAngles = _view.Foundation.localEulerAngles.WithY(newRotationHorizontal);

            _horizontalMovement = 0;
            _isMoved = true;
        }

        private void ProceedVerticalMovement()
        {
            if (_verticalMovement == 0) return;
            var newRotationVertical = _view.Barrel.localEulerAngles.z +
                               _verticalMovement * _aimSettings.VerticalSpeed * Time.deltaTime;
            if (newRotationVertical > 180)
                newRotationVertical -= 360;
            newRotationVertical = Mathf.Clamp(newRotationVertical, _aimSettings.VerticalRotationBorders.x,
                _aimSettings.VerticalRotationBorders.y);
            _view.Barrel.localEulerAngles = _view.Barrel.localEulerAngles.WithZ(newRotationVertical);

            _verticalMovement = 0;
            _isMoved = true;
        }
    }
}