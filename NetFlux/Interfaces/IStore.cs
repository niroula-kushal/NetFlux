using System;
using System.Collections.Generic;
using System.Text;

namespace NetFlux.Interfaces
{
    public interface IStore
    {
        /// <summary>
        /// Register to dispatchers
        /// </summary>
        public void Register();

        /// <summary>
        /// Subscribe to a change in the store
        /// </summary>
        /// <param name="eventToSubscribeTo"></param>
        /// <param name="handler"></param>
        public void Subscribe(string eventToSubscribeTo, EventHandler handler)
        {

        }

        /// <summary>
        /// Subscribe to a change in store
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventToSubscribeTo"></param>
        /// <param name="handler"></param>
        public void Subscribe<T>(string eventToSubscribeTo, EventHandler<T> handler)
        {

        }
    }
}
