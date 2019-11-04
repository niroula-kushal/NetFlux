# NetFlux
Inspired by Facebook's Flux architecture. This is not a complete implementation of the Flux architecture. I have essentially ignored the View aspect in this one. Go to https://facebook.github.io/flux/docs/in-depth-overview for indepth information about Flux architecture.

### Usage
First, we need a dispatcher. It dispatches action to appropriate stores.

```csharp
// using NetFlux;
Dispatcher dispatcher = new Dispatcher();
```

After that, we need to instantiate our stores. Lets assume, we have a  AppStore

```csharp
AppStore appStore = new AppStore(dispatcher);
```

Our store then needs to register it self to the dispatcher. There must be a Register method in our store. Internally, this method registers the store to the dispatcher on various actions.

```csharp
public void Register()
{
    Dispatcher.Register<NameChangeAction>(OnNameChange);
}
```

Above, NameChangeAction is simply a class. OnNameChange is a method in the store class, that takes an instance of NameChangeAction as parameter, or the action as parameter.

```csharp
public void OnNameChange(NameChangeAction nameChange)
{
    this.Name = nameChange.Name;
    NameChanged?.Invoke(this, EventArgs.Empty);
}
```

At this point, Our store is already registered with the dispatcher. Whenever an action is dispatched by the Dispatcher, appropriate methods in our stores will be called.

Next, we need to subscribe to the changes in our store. For example, we may want to change our view when the store value changes.

We subscribe to the changes in the store using the subscribe method.

```csharp
appStore.Subscribe(AppStore.EVENT_NAME_CHANGED, OnNameChange);
//....
static void OnNameChange(object sender, EventArgs eventArgs)
{
    Console.WriteLine(appStore.Name);
}
```

Above, we subscribe to the Name Change event and run the OnNameChange method. We can also do it with anonymous methods.

```csharp
appStore.Subscribe(AppStore.EVENT_NAME_CHANGED, (sender, e) =>
{
    Console.WriteLine("Using Annonymous function");
});
```

In a store, the Subscribe method may look like this.

```csharp
public static EventHandler NameChanged;

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
```


We can dispatch actions to dispatcher using the Dispatch method.

```csharp
dispatcher.Dispatch<NameChangeAction>(new NameChangeAction("Hola"));
```

Here, we are dispatching NameChangeAction action with the payload as parameter. Any store registered to this action will get the data in payload. The store will then update its data and then fire off appropriate event. Any subscriber that is listening to the event is then notified of the change.

This way, we have a simple One Way data flow.
