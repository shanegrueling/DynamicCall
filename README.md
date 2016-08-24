# DynamicCall
A little library to call methods from runtime from a serialized parameter array.
# How to use
```csharp
var dc = new DynamicCall();

var objectYouWantToMakeAvailable = new ClassYouWantToMakeAvailable();
var methodYouWantToMakeAvailable = objectYouWantToMakeAvailable.GetType().GetMethodInfo("MethodYouWantToMakeAvailable");

dc.Add(objectYouWantToMakeAvailable, methodYouWantToMakeAvailable);

dc.Call("ClassYouWantToMakeAvailable", "MethodYouWantToMakeAvailable", new SerializerOFYourChoice());
```
