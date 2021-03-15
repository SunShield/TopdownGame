using ModestTree;
using UnityEngine;

namespace Zenject
{
    public class ZenAutoInjecter : MonoBehaviour
    {
        [SerializeField]
        ContainerSources _containerSource = ContainerSources.SearchHierarchy;

        bool _hasInjected;

        public ContainerSources ContainerSource
        {
            get { return _containerSource; }
            set { _containerSource = value; }
        }

		//Make sure they don't cause injection to happen twice
		//[Inject]
		//public void Construct()
		//{
		//	if (!_hasInjected)
		//	{
		//		throw Assert.CreateException(
		//			"ZenAutoInjecter was injected!  Do not use ZenAutoInjecter for objects that are instantiated through zenject or which exist in the initial scene hierarchy");
		//	}
		//}

		public void Awake()
        {
            // sometimes I need to have an Auto Injector on objects which can be already injected in different siruations
            // (for example, if an Effect is instantiated, it needs to be injected, but if the same effect is put on the entity from the start, it is already injected from entitie's Auto Injector)
            // the same applies to the entities.
            if (_hasInjected) return; 

            _hasInjected = true;
            LookupContainer().InjectGameObject(gameObject);
        }

        DiContainer LookupContainer()
        {
            if (_containerSource == ContainerSources.ProjectContext)
            {
                return ProjectContext.Instance.Container;
            }

            if (_containerSource == ContainerSources.SceneContext)
            {
                return GetContainerForCurrentScene();
            }

            Assert.IsEqual(_containerSource, ContainerSources.SearchHierarchy);

            var parentContext = transform.GetComponentInParent<Context>();

            if (parentContext != null)
            {
                return parentContext.Container;
            }

            return GetContainerForCurrentScene();
        }

        DiContainer GetContainerForCurrentScene()
        {
            return ProjectContext.Instance.Container.Resolve<SceneContextRegistry>()
                .GetContainerForScene(gameObject.scene);
        }

        public enum ContainerSources
        {
            SceneContext,
            ProjectContext,
            SearchHierarchy
        }
    }
}
