# PipelineStageBuilder&lt;TSelf, TStage, TBaseNode, TProcessor, TShouldContinue&gt;.WithAlwaysContinue Method
## Definition

Specifies that processing should always continue after this stage. By default, processing will not continue if there are any errors in the tree.

```c#
public abstract TSelf WithAlwaysContinue();
```

## Returns

TSelf

The fluent builder.
