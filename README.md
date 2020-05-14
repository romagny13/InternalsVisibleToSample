# InternalsVisibleToSample
 
 > Show how to test private types and private methods


Example:

Add to **AssemblyInfo** of "MyLibrary"

```cs
[assembly:InternalsVisibleTo("MyLibrary.Tests")]
```

.. Allows to access to private types of assembly from the Test project


Use PrivateObject with .Net Framework or Reflection with .Net Core to test **private methods**