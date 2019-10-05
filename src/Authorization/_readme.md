# Custom authorization demo with RequiredPermission attribute.
Look at these first:

Tests.PermissionTests.PermissionTest()
```csharp
var content = new Content { Name = "Content1" };
content.AddPermission("Group1", PermissionType.Open);
content.AddPermission("Group1", PermissionType.RunApplication);
host.SaveContent(content);
// ...
host.Invoke("Client", "Class1", "DoIt", "Content1")
```

Client.Class1.DoIt()
```csharp
[RequiredPermissions("Group1", "Open, RunApplication")]
public string DoIt(Content content)
{
    ...
```