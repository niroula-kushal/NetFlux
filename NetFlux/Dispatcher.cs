using System;
using System.Collections.Generic;
using System.Linq;

namespace NetFlux
{
    public class Dispatcher
    {
        public readonly Dictionary<Type, IList<object>> actions = new Dictionary<Type, IList<object>>();

        public void Register<ACTION>(Action<ACTION> stuffToDo)
        {
            var stuffsToDo = GetListOfStuffs<ACTION>();
            stuffsToDo.Add(stuffToDo);
        }

        public void Dispatch<ACTION>(ACTION actionData)
        {
            var stuffsToDo = GetStuffsToDoForAction<ACTION>();
            stuffsToDo.ToList().ForEach(stuffToDo => stuffToDo(actionData));
        }

        private IEnumerable<Action<ACTION>> GetStuffsToDoForAction<ACTION>()
        {
            var type = typeof(ACTION);
            InitializeStuffsForActionIfEmpty<ACTION>();
            return actions[type].OfType<Action<ACTION>>();
        }

        private List<object> GetListOfStuffs<ACTION>()
        {
            var type = typeof(ACTION);
            InitializeStuffsForActionIfEmpty<ACTION>();
            return actions[type] as List<object>;
        }

        private void InitializeStuffsForActionIfEmpty<ACTION>()
        {
            var type = typeof(ACTION);
            if (!actions.ContainsKey(type))
            {
                actions.Add(type, new List<object>());
            }
        }
    }
}