ConfigurationMapper
===================

ConfigurationMapper performs binding of application settings, connection strings and configuration sections from app.config (or web.config) file to strongly typed .Net object.
Usage:
------
Configuration manager has a set of attributes (`AppSettingAttribute`, `ConnectionStringAttribute` and `ConfigSectionAttribute`) which is used to associate object properties with configuration values.

###`AppSetting` attribute:###
AppSetting attribute has the following parameters:
* string `Key` - used in app setting lookup process. If key is null or empty, property name is used.
* bool `IsRequired` - flag indicating whether `PropertyNotFoundException` should be thrown in case application setting with specified key was not found in configuration and there is no default value for it.
* string `DefaultValue` - default value for property in case it wasn't found in configuration.
* string `CultureName` - name of culture used in properties mapping. Default is current thread's culture.
* string `ArrayDelimiter` - used for array properties, default is "," (comma). For non-array properties this option is ignored.

####Sample:####

Consider we have the following appSettings in our configuration file:
```xml
<appSettings>
    <add key="ProjectSetting1" value="Hello!"/>
    <add key="Equals" value="True"/>        
</appSettings>
```
These settings can be mapped as follows:
```csharp
public class ProjectConfiguration
{
  [AppSetting]
  public string ProjectSetting1 { get; set; }
      
  [AppSetting(IsRequired = true, DefaultValue= 2)]
  public int ProjectSetting2 { get; set; }
      
  [AppSetting(Key = "Equals")]
  public bool ProjectSetting3 { get; set; }
}
...
var config = ConfigurationMapper<ProjectConfiguration>.Map();
Console.WriteLine(config.ProjectSetting1); // "Hello!"
Console.WriteLine(config.ProjectSetting2); // "2". NOTE: This property was not found in config but has default value, so it is mapped successfully.
Console.WriteLine(config.ProjectSetting3); // "true"
```
###`ConnectionString` attribute:###
ConnectionString attribute has the following parameters:
* string `Name` - used in connection string lookup process. If Name is null or empty, property name is used.
* bool `IsRequired` - flag indicating whether `PropertyNotFoundException` should be thrown in case connection string with specified name was not found in configuration and there is no default value for it.
* string `DefaultValue` - default value for property in case it wasn't found in configuration.

####Sample:####

Consider we have the following connectionStrings in our configuration file:
```xml
<connectionStrings>
    <add name="PlainCS" connectionString="Data Source=(local);Initial Catalog=TestDB1;Integrated Security=True" providerName="System.Data.SqlClient" />
    <add name="Equals" connectionString="Data Source=(local);Initial Catalog=TestDB2;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```
These connection strings can be mapped as follows:
```csharp
public class ProjectConfiguration
{
  [ConnectionString]
  public string PlainCS { get; set; }
  
  [ConnectionString(Name = "Equals")]
  public string OverridenName { get; set; }
}
...
var config = ConfigurationMapper<ProjectConfiguration>.Map();
Console.WriteLine(config.PlainCS); // "Data Source=(local);Initial Catalog=TestDB1;Integrated Security=True".
Console.WriteLine(config.OverridenName); // "Data Source=(local);Initial Catalog=TestDB2;Integrated Security=True".
```
You can also map connection string to  [SqlConnectionStringBuilder](http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqlconnectionstringbuilder(v=vs.110).aspx) type. For example, you have the following connectionStrings in our configuration file:
```xml
<connectionStrings>
    <add name="ProjectSetting1" connectionString="Data Source=(local);Initial Catalog=TestDB3;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```
Then map this connection string as follows:
```csharp
public class ProjectConfiguration
{
    [ConnectionString]
    public SqlConnectionStringBuilder ProjectSetting1 { get; set; }
}
...
var config = ConfigurationMapper<ProjectConfiguration>.Map();
Console.WriteLine(config.ProjectSetting1.InitialCatalog); // "TestDB3".
```
