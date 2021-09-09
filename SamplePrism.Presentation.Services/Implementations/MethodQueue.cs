using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using SamplePrism.Domain.Models.Users;

namespace SamplePrism.Presentation.Services.Implementations
{
    public class MethodQueue : IMethodQueue
    {
        private readonly IApplicationState _applicationState;

        public MethodQueue(IApplicationState applicationState)
        {
            _applicationState = applicationState;
        }

        private static readonly Dictionary<string, Action> MethodList = new Dictionary<string, Action>();

        public void Queue(string key, Action action)
        {
            if (!MethodList.ContainsKey(key))
                MethodList.Add(key, action);
            else MethodList[key] = action;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RunQueue()
        {
            if (MethodList.Count == 0 || _applicationState.CurrentLoggedInUser == User.Nobody || _applicationState.IsLocked) return;
            lock (MethodList)
            {
                MethodList.Values.ToList().ForEach(x => x.Invoke());
                MethodList.Clear();
            }
        }
    }
}
