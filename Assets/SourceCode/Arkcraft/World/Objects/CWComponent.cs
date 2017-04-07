namespace Arkcraft.World.Objects
{
    public class CWComponent
    {
        protected ACObject cwobject;

        internal void AddedToObject(ACObject cwobject)
        {
            this.cwobject = cwobject;

            OnAddedToObject(cwobject);
        }

        internal void RemovedFromObject()
        {
            this.cwobject = null;

            OnRemovedFromObject();
        }

        protected virtual void OnRemovedFromObject()
        {
        }

        protected virtual void OnAddedToObject(ACObject cwobject)
        {
        }

        public virtual void Update(float deltaTime)
        {
        }
    }
}
