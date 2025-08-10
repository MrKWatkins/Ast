# Pipeline&lt;TBaseNode&gt;.Run Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Run(TBaseNode)](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0)) | Runs the pipeline on the specified root node. |
| [Run(TBaseNode, String)](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)) | Runs the pipeline on the specified root node. |

## Run(TBaseNode) {id="mrkwatkins-ast-processing-pipeline-1-run(-0)"}

Runs the pipeline on the specified root node.

```c#
public bool Run(TBaseNode root);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipeline-1-run(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TBaseNode | The root node to run the pipeline on. |

## Returns {id="returns-mrkwatkins-ast-processing-pipeline-1-run(-0)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if all stages ran successfully, `false` otherwise.
## Run(TBaseNode, String) {id="mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)"}

Runs the pipeline on the specified root node.

```c#
public bool Run(TBaseNode root, out string lastStageRun);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TBaseNode | The root node to run the pipeline on. |
| lastStageRun | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The name of the last stage that was run. If `false` is returned then this will be the name of the stage that stopped further stages from continuing. |

## Returns {id="returns-mrkwatkins-ast-processing-pipeline-1-run(-0-system-string@)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if all stages ran successfully, `false` otherwise.
