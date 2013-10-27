using ProtoBuf;

namespace Transport.Protocol
{
    [ProtoContract]
    internal class OperationSetOperationsPublisherRequest : OperationRequest<OperationResponse>
    {
        [ProtoMember(1)]
        public string Address { get; set; }
        [ProtoMember(2)]
        public string Topic { get; set; }
    }
}