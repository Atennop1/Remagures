namespace BehaviorDesigner.Runtime.Tasks
{
    // The decorator class is a wrapper task that can only have one child. The decorator task will modify the behavior of the child task in some way. For example,
    // the decorator class may keep running the child task until it returns with a status of success or it may invert the return status of the child.
    public class Decorator : ParentTask
    {
        public override int MaxChildren()
        {
            return 1;
        }
    }
}