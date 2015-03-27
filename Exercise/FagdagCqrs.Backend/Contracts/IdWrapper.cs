using System;

namespace RestApi.Contracts
{
    public class IdWrapper
    {
        public IdWrapper()
        {
        }

        public IdWrapper(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}