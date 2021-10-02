using System;

namespace LibraryClass.Helpers
{
    public class ClassAttribute : Attribute
    {

        public ClassAttribute(string title = "", bool visible = true, int width = 0)
        {
            Title = title;
            Visible = visible;
            Width = width;

        }

        public string Title { get; private set; }

        public bool Visible { get; private set; }

        public int Width { get; private set; }
    }
}
