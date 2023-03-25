using System;

namespace BehaviorDesigner.Runtime.Tasks
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class LinkedTaskAttribute : Attribute
    {
        // Intentionally left blank
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class InspectTaskAttribute : Attribute
    {
        // Intentionally left blank
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public abstract class ObjectDrawerAttribute : Attribute
    {
        // Intentionally left blank
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class RequiredFieldAttribute : Attribute
    {
        // Intentionally left blank
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SharedRequiredAttribute : Attribute
    {
        // Intentionally left blank
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class TooltipAttribute : Attribute
    {
        public string Tooltip { get { return mTooltip; } }
        public readonly string mTooltip;
        public TooltipAttribute(string tooltip) { mTooltip = tooltip; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TaskIconAttribute : Attribute
    {
        public string IconPath { get { return mIconPath; } }
        public readonly string mIconPath;
        public TaskIconAttribute(string iconPath) { mIconPath = iconPath; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HelpURLAttribute : Attribute
    {
        public string URL { get { return mURL; } }
        private readonly string mURL;
        public HelpURLAttribute(string url) { mURL = url; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TaskCategoryAttribute : Attribute
    {
        public string Category { get { return mCategory; } }
        public readonly string mCategory;
        public TaskCategoryAttribute(string category) { mCategory = category; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TaskDescriptionAttribute : Attribute
    {
        public string Description { get { return mDescription; } }
        public readonly string mDescription;
        public TaskDescriptionAttribute(string description) { mDescription = description; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TaskNameAttribute : Attribute
    {
        public string Name { get { return mName; } }
        public readonly string mName;
        public TaskNameAttribute(string name) { mName = name; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class RequiredComponentAttribute : Attribute
    {
        public Type ComponentType { get { return mComponentType; } }
        public readonly Type mComponentType;
        public RequiredComponentAttribute(Type componentType) { mComponentType = componentType; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class SkipErrorCheckAttribute : Attribute
    {
        // Intentionally left blank
    }
}