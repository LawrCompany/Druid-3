using System.Collections.Generic;
using Helper;
using Model;
using UnityEngine;


namespace Controller
{
    public class PoolController
    {
        //todo сделать пуллменеджер синглтоном и при создании выделять для него все нужные в игре Dictionary<string, LinkedList<BaseObjectScene>>
        //todo сделать автокоррекцию размера пула (допустим вид патронов Bullet1 - максимум 50, если надо большо то создаются, но после возврата в пул урезать до 50)
        private Dictionary<string, LinkedList<BaseObjectScene>> _poolsDictionary;
        private Transform _deactivatedObjectsParent;

        public void Init(Transform pooledObjectsContainer)
        {
            Dbg.Log($"PoolController.Init; _poolsDictionary Create new Dictionary<string, LinkedList<BaseObjectScene>>()");
            _deactivatedObjectsParent = pooledObjectsContainer;
            _poolsDictionary = new Dictionary<string, LinkedList<BaseObjectScene>>();
        }

        public BaseObjectScene GetFromPool(BaseObjectScene prefab)
        {
            //Debug.Log($"_poolsDictionary: {_poolsDictionary.Count}");
            if (!_poolsDictionary.ContainsKey(prefab.name))
            {
                Dbg.Log($"Create new LinkedList<BaseObjectScene>");
                _poolsDictionary[prefab.name] = new LinkedList<BaseObjectScene>();
            }

            BaseObjectScene result;

            if (_poolsDictionary[prefab.name].Count > 0)
            {
                Dbg.Log($"_poolsDictionary[{prefab.name}].Count = {_poolsDictionary.Count}");
                result = _poolsDictionary[prefab.name].First.Value;
                _poolsDictionary[prefab.name].RemoveFirst();
                result.SetDefault();
                result.SetActive(true);
                //result.SetActivateChildren(result.gameObject, true);
                Dbg.Log($"return {result.name}");
                return result;
            }

            Dbg.Log($"Create new BaseObjectScene.Instantiate(prefab): {prefab.name}");
            result = BaseObjectScene.Instantiate(prefab);
            result.name = prefab.name;

            return result;
        }

        public void PutToPool(BaseObjectScene target)
        {
            _poolsDictionary[target.name].AddFirst(target);
            target.transform.parent = _deactivatedObjectsParent;
            target.SetActive(false);
            Dbg.Log($"PoolController.PutToPool; pool.Count: {_poolsDictionary[target.name].Count}");
        }
    }
}