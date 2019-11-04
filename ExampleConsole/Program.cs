using System;
using ExampleConsole.Actions;
using ExampleConsole.Stores;
using NetFlux;

namespace ExampleConsole
{
    static class Program
    {
        static AppStore appStore;
        static Dispatcher dispatcher;
        static void Main(string[] args)
        {
            dispatcher = new Dispatcher();
            appStore = new AppStore(dispatcher);
            appStore.Register();
            appStore.Subscribe(AppStore.EVENT_NAME_CHANGED, OnNameChange);
            appStore.Subscribe(AppStore.EVENT_NAME_CHANGED, (sender, e) =>
            {
                Console.WriteLine("Using Annonymous function");
            });
            dispatcher.Dispatch<NameChangeAction>(new NameChangeAction("Hola"));
        }

        static void OnNameChange(object sender, EventArgs eventArgs)
        {
            Console.WriteLine(appStore.Name);
        }
    }
}