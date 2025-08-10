# Pipeline&lt;TContext, TBaseNode&gt;.Run Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Run(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Pipeline-2.Run.md#mrkwatkins-ast-processing-pipeline-2-run(-0-1)) | Runs the pipeline on the specified root node. |
| [Run(TContext, TBaseNode, String)](MrKWatkins.Ast.Processing.Pipeline-2.Run.md#mrkwatkins-ast-processing-pipeline-2-run(-0-1-system-string@)) | Runs the pipeline on the specified root node. |

## Run(TContext, TBaseNode) {id="mrkwatkins-ast-processing-pipeline-2-run(-0-1)"}

Runs the pipeline on the specified root node.

```c#
public bool Run(TContext? context, TBaseNode root);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipeline-2-run(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run the pipeline on. |

## Returns {id="returns-mrkwatkins-ast-processing-pipeline-2-run(-0-1)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if all stages ran successfully, `false` otherwise.
## Run(TContext, TBaseNode, String) {id="mrkwatkins-ast-processing-pipeline-2-run(-0-1-system-string@)"}

Runs the pipeline on the specified root node.

```c#
public bool Run(TContext? context, TBaseNode root, out string lastStageRun);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipeline-2-run(-0-1-system-string@)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root node to run the pipeline on. |
| lastStageRun | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the last stage that was run. If `false` is returned then this will be the name of the stage that stopped further stages from continuing. |

## Returns {id="returns-mrkwatkins-ast-processing-pipeline-2-run(-0-1-system-string@)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if all stages ran successfully, `false` otherwise.
