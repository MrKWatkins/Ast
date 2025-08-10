# PipelineException Class
## Definition

Exception thrown by a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) when one or more [Exceptions](https://learn.microsoft.com/en-gb/dotnet/api/System.Exception) occur during processing. [InnerException](https://learn.microsoft.com/en-gb/dotnet/api/System.Exception.InnerException) will contain specifics of the [Exception](https://learn.microsoft.com/en-gb/dotnet/api/System.Exception), and will be an [AggregateException](https://learn.microsoft.com/en-gb/dotnet/api/System.AggregateException) if multiple exceptions occurred.

```c#
public sealed class PipelineException : Exception, ISerializable
```

## Properties

| Name | Description |
| ---- | ----------- |
| [Message](MrKWatkins.Ast.Processing.PipelineException.Message.md) |  |
| [Stage](MrKWatkins.Ast.Processing.PipelineException.Stage.md) | The name of the stage the exception occurred in. |

