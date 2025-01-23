using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace Code.Entity
{
    public abstract class Entity : MonoBehaviour
    {
        private Dictionary<Type, IEntityComponent> _components;
        
        private void Awake()
        {
            _components = new Dictionary<Type, IEntityComponent>();

            InitializeComponents();
        }

        protected virtual void InitializeComponents()
        {
            GetComponentsInChildren<IEntityComponent>().ToList().ForEach(compo => _components.Add(compo.GetType(), compo));
            _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        public T GetCompo<T>(bool isDerived = false) where T : class
        {
            if (_components.TryGetValue(typeof(T), out IEntityComponent component)) return (T)component;

            if (isDerived == false) return default(T);

            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if (findType != null) return (T)_components[findType];

            return default(T);
        }
    }
}