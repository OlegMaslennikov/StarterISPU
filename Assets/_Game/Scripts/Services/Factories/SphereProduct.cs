// SphereProduct.cs - обновленная версия
using UnityEngine;

namespace _Game.Scripts.Services.Factories
{
    public class SphereProduct : MonoBehaviour, IProduct
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public void Initialize()
        {
            gameObject.name = "Sphere Product";
            
            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();
                
            if (_meshRenderer != null)
            {
                _meshRenderer.material.color = Color.red;
            }
            
            Debug.Log($"SphereProduct initialized at {transform.position}");
        }
        
        private void OnDestroy()
        {
            Debug.Log("SphereProduct prefab destroyed");
        }
    }
}