using System;
using System.Collections.Generic;
using System.Text;
using ExampleConsole.Actions;
using NetFlux;
using NetFlux.Interfaces;

namespace ExampleConsole.Stores
{

    class AppStore : IStore
    {
        public const string EVENT_NAME_CHANGED = "NAME_CHANGED";

        public string Name { get; protected set; }
        public Dispatcher Dispatcher { get; }

        public static EventHandler NameChanged;

        public void OnNameChange(NameChangeAction nameChange)
        {
            this.Name = nameChange.Name;
            NameChanged?.Invoke(this, EventArgs.Empty);
        }

        public AppStore(Dispatcher dispatcher)
        {
            this.Dispatcher = dispatcher;
        }

        public void Register()
        {
            Dispatcher.Register<NameChangeAction>(OnNameChange);
        }

        public void Subscribe(string eventToSubscribeTo, EventHandler subscriber)
        {
            switch (eventToSubscribeTo)
            {
                case EVENT_NAME_CHANGED:
                    NameChanged += subscriber;
                    break;
                default:
                    return;
            }
        }

    }
}
