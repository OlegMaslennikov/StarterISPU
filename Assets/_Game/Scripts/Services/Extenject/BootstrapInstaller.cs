using UnityEngine;
using Zenject;

public class BootstrapInstaller : MonoInstaller
{
    [SerializeField] private PlayerStartDataSO _playerStartData;
    
    public override void InstallBindings()
    {
        // BindInstance - регистрирует уже существующий объект в контейнере
        // AsSingle - создаст один экземпляр для всего приложения (синглтон)
        Container.BindInstance(_playerStartData).AsSingle();
        
        // Bind<T> - регистрирует тип, Zenject сам создаст экземпляр через конструктор
        // NonLazy - объект создастся сразу при старте, а не при первом использовании
        Container.Bind<GameData>().AsSingle().NonLazy();
        Container.Bind<GameDataController>().AsSingle().NonLazy();
        
        Container.Bind<SaveLoadManager>().AsSingle().NonLazy();
        Container.Bind<StateManager>().AsSingle().NonLazy();
        Container.Bind<SceneChanger>().AsSingle().NonLazy();
       
        // Bind<IInitializable> - специальный интерфейс Zenject
        // To<GameInitializer> - реализация интерфейса
        // Initialize() вызовется автоматически после всех инъекций
        Container.Bind<IInitializable>().To<GameInitializer>().AsSingle().NonLazy();
    }
}

/*

Суть Dependency Injection (DI)
Dependency Injection — это паттерн проектирования, 
который решает проблему управления зависимостями между классами. 
Вместо того чтобы класс сам создавал нужные ему объекты через new, 
он получает их извне — "инъекцию". Это делает код слабосвязанным, 
тестируемым и гибким.  
Основные преимущества: 
-легко заменять реализации (например, для тестов), 
-контролировать время жизни объектов (синглтоны, временные объекты), 
-централизованно управлять всеми зависимостями проекта.

Zenject в Unity
Zenject — это мощный DI-фреймворк для Unity, учитывающий особенности игрового движка: 
работу с MonoBehaviour, асинхронную загрузку сцен, ScriptableObject и т.д. 
Вместо ручного "склеивания" компонентов через инспектор или поиска по сцене, 
вы описываете все зависимости в специальных классах-инсталлерах (MonoInstaller). 
Контейнер Zenject сам собирает объекты, учитывая их жизненный цикл: 
-AsSingle() для синглтонов, 
-AsTransient() для временных объектов, 
-NonLazy() для немедленного создания. 
Особенно ценна поддержка сложных сценариев: 
инъекция в MonoBehaviour без доступа к конструктору (через [Inject] метод), 
автоматический вызов Initialize() после всех инъекций, привязка интерфейсов к реализациям. 
Это позволяет строить чистую архитектуру даже в больших проектах, 
где объекты создаются не только на сцене, но и динамически (префабы, пулы, фабрики).


Краткая шпаргалка по Zenject:
Основные методы привязки:
Bind<T>() - привязка по типу
BindInstance(obj) - привязка готового объекта
To<T>() - указать реализацию интерфейса

Жизненные циклы (Scope):
AsSingle() - один экземпляр на весь проект
AsTransient() - новый экземпляр при каждой инъекции (по умолчанию)
AsCached() - как синглтон, но без гарантии единственности

Полезные модификаторы:
NonLazy() - создать объект немедленно
WhenInjectedInto<T>() - применять только для конкретного класса
WithId("name") - несколько привязок одного типа
Читайте документацию на GitHub https://github.com/modesttree/Zenject
*/