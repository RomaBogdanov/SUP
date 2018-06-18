using AndoverLib;

namespace AndoverPersonsManager
{
    public class ContainerCore
    {
        private readonly Container _container;

        public ContainerCore(Container container)
        {
            _container = container;

            Name = _container.UiName;
        }

        public Container Container
        {
            get
            {
                return _container;
            }
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
