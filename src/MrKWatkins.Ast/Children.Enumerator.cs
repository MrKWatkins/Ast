using System.Collections;

namespace MrKWatkins.Ast;

public sealed partial class Children<TNode>
    where TNode : Node<TNode>
{
    /// <inheritdoc />
    /// <remarks>
    /// Enumerating over children tries to accommodate changes to the collection whilst enumerating. However not all changes can be
    /// accommodated and an <see cref="InvalidOperationException" /> will be thrown if enumeration cannot continue.
    /// </remarks>
    public IEnumerator<TNode> GetEnumerator() => new Enumerator(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    internal struct Enumerator(Children<TNode> children) : IEnumerator<TNode>
    {
        private int index = -1;
        private int countBeforeLastIteration = 0;

        public bool MoveNext()
        {
            if (index == -1 || countBeforeLastIteration == children.Count)
            {
                index++;
            }
            else
            {
                // Children has been mutated. Try and work out what is going on.
                var indexOfChild = children.IndexOf(Current);
                if (indexOfChild == -1)
                {
                    // child has been removed from the tree. If the count has changed by one assume that's the only change.
                    // (e.g. Ignore child and another node being removed, and yet another added) Leave index as is so we
                    // continue enumeration on it's (previously) next sibling.
                    if (children.Count != countBeforeLastIteration - 1)
                    {
                        throw new InvalidOperationException(
                            $"Node {Current} has been removed from parent during enumeration at the same time as other mutations. Cannot determine sensible place to continue enumeration.");
                    }
                }
                else
                {
                    // Node is still in the tree. Set index to after it's current position and continue on as normal.
                    index = indexOfChild + 1;
                }
            }

            countBeforeLastIteration = children.Count;

            if (index < children.Count)
            {
                Current = children[index];
                return true;
            }

            return false;
        }

        public void Reset() => index = -1;

        public TNode Current { get; private set; } = null!;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}