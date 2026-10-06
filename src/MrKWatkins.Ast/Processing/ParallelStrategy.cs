namespace MrKWatkins.Ast.Processing;

/// <summary>
/// How a parallel pipeline stage divides its work between threads.
/// </summary>
/// <remarks>
/// Whichever strategy is used the tree is walked lazily; it is never loaded into memory in its entirety. Processors in a parallel stage must not change
/// the structure of the tree as the walk is in progress whilst they run, and nodes are processed concurrently with their ancestors and descendents.
/// </remarks>
public enum ParallelStrategy
{
    /// <summary>
    /// The tree is walked once and each node is handed to a thread, which runs every processor in the stage on that node in turn. A node is only ever
    /// being processed by one thread at a time, so processors can safely write to the node they are given. This is the default, and suits stages with
    /// many cheap processors.
    /// </summary>
    PerNode,

    /// <summary>
    /// Each processor runs on its own thread and walks the whole tree itself. The tree is walked once per processor, and different processors can be
    /// processing the same node at the same time, so processors must only write to a node in a thread safe manner, e.g. by adding messages. Suits
    /// stages with a few expensive processors, or processors that read neighbouring nodes.
    /// </summary>
    PerProcessor
}