namespace DataModel.GameResources
{
    // string id for debug purposes, can be easily replaced to other type, like int, byte, etc
    public readonly struct ResourceId
    {
        public readonly string Id;

        public ResourceId(string id)
        {
            Id = id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ResourceId otherResource)
            {
                return Id.Equals(otherResource.Id);
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
