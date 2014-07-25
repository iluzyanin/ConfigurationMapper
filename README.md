ConfigurationMapper
===================

ConfigurationMapper performs binding of app settings, connection strings and configuration sections from app.config (or web.config) to strongly typed .Net object. To bind each property in configuration object, one of the binding attributes is used: AppSetting, ConnectionString, ConfigurationSection.
Usage:
------
Use of AppSetting attribute.
```csharp
public class ProjectConfiguration
{
  [AppSetting]
  public string ProjectSetting1 { get; set; }
  [AppSetting(IsRequired = true, DefaultValue= 2)]
  public int ProjectSetting2 { get; set; }
}
...
var config = ConfigurationMapper<ProjectConfiguration>.Map();
// config object now contains binded properties.
```
Use of ConnectionString attribute.
```csharp
public class ProjectConfiguration
{
  [ConnectionString]
  public string ProjectSetting1 { get; set; }
}
...
var config = ConfigurationMapper<ProjectConfiguration>.Map();
```
You can also use SqlConnectionStringBuilder type properties to bind to:
```csharp
public class ProjectConfiguration
{
  [ConnectionString]
  public SqlConnectionStringBuilder ProjectSetting1 { get; set; }
}
...
var config = ConfigurationMapper<ProjectConfiguration>.Map();
```
