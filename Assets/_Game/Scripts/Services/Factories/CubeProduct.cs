// CubeProduct.cs - обновленная версия
using UnityEngine;

namespace _Game.Scripts.Services.Factories
{
    public class CubeProduct : MonoBehaviour, IProduct
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public void Initialize()
        {
            gameObject.name = "Cube Product";
            
            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();
                
            if (_meshRenderer != null)
            {
                _meshRenderer.material.color = Color.blue;
            }
            
            Debug.Log($"CubeProduct initialized at {transform.position}");
        }
        
        private void OnDestroy()
        {
            Debug.Log("CubeProduct prefab destroyed");
        }
    }
}