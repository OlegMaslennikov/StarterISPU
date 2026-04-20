using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.Serialization;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private AssetReference uiLevelPanelPrefab;
    [SerializeField] private AssetReference uiSaveLoadPrefab;
    [SerializeField] private string uiCatsPrefabName; 
    private DiContainer _container;
    
    async void Start()
    {
        await LoadAllUI();
    }
    
    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }
    
    async Task LoadAllUI()
    {
        Debug.Log("Начинаем загрузку...");
        
        GameObject levelPanel = await LoadAndInstantiate(uiLevelPanelPrefab);
        levelPanel.transform.SetParent(transform);
        
        
        GameObject saveLoadPanel = await LoadAndInstantiate(uiSaveLoadPrefab);
        saveLoadPanel.transform.SetParent(levelPanel.transform);
        
        GameObject catsPanel = await LoadFromAddress(uiCatsPrefabName);
        catsPanel.transform.SetParent(levelPanel.transform);
        
        Debug.Log("Все UI элементы загружены!");
    }
    
    async Task<GameObject> LoadAndInstantiate(AssetReference assetRef)
    {
        await Task.Delay(500);
        var handle = Addressables.LoadAssetAsync<GameObject>(assetRef);
        await handle.Task; 
        return _container.InstantiatePrefab(handle.Result);
    }
    
    async Task<GameObject> LoadFromAddress(string address)
    {
        await Task.Delay(500);
        var handle = Addressables.LoadAssetAsync<GameObject>(address);
        await handle.Task;
        return _container.InstantiatePrefab(handle.Result);
    }
}