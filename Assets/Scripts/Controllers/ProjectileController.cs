using System.Collections.Generic;
using Components.Views;
using Data;
using Extensions;
using MEC;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class ProjectileController
    {
        [Inject] private ProjectileView _view;
        [Inject] private ProjectileSettings _settings;
        [Inject] private VFXFactory _vfxFactory;
        [Inject] private ImpactMarkFactory _impactMarkFactory;
        
        private Vector3 _flyVelocity;
        private CoroutineHandle _flyCoroutineHandle;
        private Transform _viewTrs;
        private Vector3[] _prevVerticesPos;
        private Ray _ray;
        private RaycastHit[] _hitResults = new RaycastHit[1];
        private int _numOfCollisions;
        private Vector3[] _vertices;
        private Vector3[] _globalVerticesPos;
        private Vector3 _rotationAxis;

        [Inject]
        private void Construct()
        {
            GenerateModel();
            _viewTrs = _view.transform;
        }
        
        private void GenerateModel()
        {
            Mesh mesh = new Mesh();

            _vertices = new[] {
                new Vector3(-0.5f, -0.5f, -0.5f),
                new Vector3( 0.5f, -0.5f, -0.5f),
                new Vector3( 0.5f,  0.5f, -0.5f),
                new Vector3(-0.5f,  0.5f, -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3( 0.5f, -0.5f,  0.5f),
                new Vector3( 0.5f,  0.5f,  0.5f),
                new Vector3(-0.5f,  0.5f,  0.5f) 
            };
            _globalVerticesPos = new Vector3[_vertices.Length];
            _prevVerticesPos = new Vector3[_vertices.Length];

            for (int i = 0; i < _vertices.Length; i++)
            {
                _vertices[i] = _vertices[i] *= Random.Range(.5f, 1.5f);
            }
            
            int[] triangles = {
                0, 2, 1, 0, 3, 2,
                5, 6, 4, 6, 7, 4,
                4, 7, 0, 7, 3, 0,
                1, 2, 5, 2, 6, 5,
                3, 7, 2, 7, 6, 2,
                4, 0, 5, 0, 1, 5
            };

            Vector2[] uvs = {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
            };

            mesh.vertices = _vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            _view.MeshFilter.mesh = mesh;
        }
        
        public void Launch(Vector3 dir)
        {
            _rotationAxis = Random.onUnitSphere;
            _numOfCollisions = 0;
            _flyVelocity = dir;
            CachePointsGlobalPos();
            _flyCoroutineHandle = Timing.RunCoroutine(_Flying());
        }

        IEnumerator<float> _Flying()
        {
            while (true)
            {
                yield return Timing.WaitForOneFrame;
                _viewTrs.Rotate(_rotationAxis, _settings.RotationSpeed * Time.deltaTime);
                ProceedPosition();
                CheckForCollision();
            }
        }

        private void ProceedPosition()
        {
            for (int i = 0; i < _globalVerticesPos.Length; i++)
            {
                _prevVerticesPos[i] = _globalVerticesPos[i];
            }
            _flyVelocity += Vector3.up * _settings.Gravity * Time.deltaTime;
            _viewTrs.position += _flyVelocity * Time.deltaTime;
            CachePointsGlobalPos();
        }

        private void CachePointsGlobalPos()
        {
            for (int i = 0; i < _globalVerticesPos.Length; i++)
            {
                _globalVerticesPos[i] = _vertices[i];
            }
            _view.transform.TransformPoints(_globalVerticesPos);
        }

        private void CheckForCollision()
        {
            for (int i = 0; i < _globalVerticesPos.Length; i++)
            {
                var prevPos = _prevVerticesPos[i];
                _ray.origin = prevPos;
                var curPos = _globalVerticesPos[i];
                _ray.direction = curPos - prevPos;
                var distance = (curPos - prevPos).magnitude;
                var hitCount = Physics.RaycastNonAlloc(_ray, _hitResults, distance, _settings.CollisionMask);
                if (hitCount > 0)
                {
                    if (++_numOfCollisions >= _settings.NumOfPossibleCollisions)
                        Destroy();
                    RecalculateVelocity();
                    break;
                }
            }
        }
        
        private void RecalculateVelocity()
        {
            var result = _hitResults[0];
            var hitPoint = result.point;
            var hitNormal = result.normal;
            var magnitude = _flyVelocity.magnitude;
            var reflectionVelocity = (_ray.direction - 2 * Vector3.Dot(_ray.direction, hitNormal) * hitNormal).normalized * magnitude * _settings.Bounciness;
            _flyVelocity = reflectionVelocity;
            _viewTrs.position += _flyVelocity * .5f * Time.deltaTime;
            
            CreateHitFx(hitPoint, hitNormal);
        }

        private void CreateHitFx(Vector3 hitPoint, Vector3 hitNormal)
        {
            var fx = _vfxFactory.Create(ParticleType.Hit);
            fx.transform.position = hitPoint;

            var impactMark = _impactMarkFactory.Create();
            impactMark.transform.position = hitPoint + hitNormal * .01f;
            impactMark.transform.rotation = Quaternion.LookRotation(-hitNormal);
        }

        private void Destroy()
        {
            var fx = _vfxFactory.Create(ParticleType.Death);
            fx.transform.position = _viewTrs.position;
            Timing.KillCoroutines(_flyCoroutineHandle);
            _view.Destroy();
        }
    }
}