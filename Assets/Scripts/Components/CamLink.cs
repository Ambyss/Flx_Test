using Cinemachine;
using UnityEngine;

namespace Components.Views
{
    public class CamLink : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _vCamMain;

        public CinemachineVirtualCamera VCamMain => _vCamMain;
    }
}