using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Cannon aim settings")]
public class CannonAimSettings : ScriptableObject
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeed;
    [SerializeField] private Vector2 _horizontalRotationBorders;
    [SerializeField] private Vector2 _verticalRotationBorders;
    [SerializeField] private float _fireFeedbackValue;
    [SerializeField] private float _fireFeedbackTime;
    [SerializeField] private float _firePeriod;
    [Space] 
    [SerializeField, BoxGroup("Trajectory")] private float _trajectoryDrawTimestep;
    [SerializeField, BoxGroup("Trajectory")] private int _trajectoryDrawPoints;

    public float HorizontalSpeed => _horizontalSpeed;

    public float VerticalSpeed => _verticalSpeed;

    public Vector2 HorizontalRotationBorders => _horizontalRotationBorders;

    public Vector2 VerticalRotationBorders => _verticalRotationBorders;

    public float FireFeedbackValue => _fireFeedbackValue;

    public float FireFeedbackTime => _fireFeedbackTime;

    public float TrajectoryDrawTimestep => _trajectoryDrawTimestep;

    public int TrajectoryDrawPoints => _trajectoryDrawPoints;

    public float FirePeriod => _firePeriod;
}